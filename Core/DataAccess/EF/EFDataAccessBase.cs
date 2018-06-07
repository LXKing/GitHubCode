using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace DataAccess.EF
{
    public class EFDataAccessBase:IEFDataAccess
    {
        protected DbContext _context;
        /// <summary>
        /// 用于对基础数据库执行创建/删除/存在性检查操作
        /// </summary>
        protected Database _db;
        public EFDataAccessBase(DbContext dbContext,DatabaseLogger dbLogger=null)
        {
            _context = dbContext;
            _db = _context.Database;
            if(dbLogger.NotNull_DA())
            {
                DbInterception.Remove(dbLogger);
                DbInterception.Add(dbLogger);
            }
        }

        public EFDataAccessBase(string connString, DatabaseLogger dbLogger = null)
        {
            _context = new DbContext(connString);
            _db = _context.Database;
            if (dbLogger.NotNull_DA())
            {
                DbInterception.Remove(dbLogger);
                DbInterception.Add(dbLogger);
            }
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            if (_db != null && _db.Connection.State!=ConnectionState.Closed)
            {
                _db.Connection.Close();
                _db.Connection.Dispose();
            }
        }

        #region Entity增删查改

        /// <summary>
        /// 增加多个实体
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entityList">要增加的实体集合</param>
        /// <returns></returns>
        public ResultInfo<object> AddEntitys<TEntity>(IEnumerable<TEntity> entityList)
            where TEntity : class, new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var set = _context.Set<TEntity>();
                entityList.ToList().ForEach(entity =>
                {
                    set.Add(entity);
                });
                var count = _context.SaveChanges();
                result.Success = (count > 0);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 增加单个实体
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要增加的实体</param>
        /// <returns></returns>
        public ResultInfo<object> AddEntity<TEntity>(TEntity entity) where TEntity : class,new()
        {
            return this.AddEntitys(new List<TEntity>() { entity });
        }
        ///// <summary>
        ///// 更新从context中查询出来实体,并返回影响的行数(Data属性)
        ///// </summary>
        ///// <returns></returns>
        public ResultInfo<object> UpdateEntitys()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                int count = _context.SaveChanges();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 批量更新对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entitysList"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("该方法请弃用，请使用新方法:UpdateEntitys<TEntity, TKey>(IEnumerable<TEntity> entitysList,string ssdl='Model')")]
        public ResultInfo<object> UpdateEntitys<TEntity, TKey>(IEnumerable<TEntity> entitysList, TKey key) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                List<TEntity> entitys = new List<TEntity>();
                entitysList.ToList().ForEach(x =>
                {
                    var key1 = key.ToString();
                    var tmp = _context.Set<TEntity>().AsEnumerable().Where(y => y.GetType().GetProperty(key1).GetValue(y, null) == x.GetType().GetProperty(key1).GetValue(x, null)).FirstOrDefault();
                    if (tmp != null)
                    {
                        entitys.Add(x.Apply_DA(tmp, true));
                    }
                });
                if (entitys.Count < entitysList.Count())
                {
                    throw new Exception("更新的集合未能全部找到!");
                }
                else
                {
                    result = this.UpdateEntitys();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.BindAllException(ex);
            }

            return result;
        }
        /// <summary>
        /// 批量更新(新方法)
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entitysList">实体集合</param>
        /// <param name="csdl">Edmx文件模型名称</param>
        /// <returns></returns>
        public ResultInfo<object> UpdateEntitys_New<TEntity>(IEnumerable<TEntity> entitysList,string csdl="Model") where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                List<TEntity> entitys = new List<TEntity>();
                entitysList.ToList().ForEach(x =>
                {
                    //var parms = this.Get_EntityKeyMembers(x, csdl).Select(z => z.Value).ToArray();
                    var json = new System.Text.StringBuilder("");
                    this.Get_EntityKeyMembers(x, csdl).ToList().ForEach(y =>
                    {
                        var v = y.Value;
                        json.AppendFormat("{0} : {1} ({2})", y.Key, y.Value, y.ValueType.FullName);
                    });
                    var tmp = _context.Entry(x);//var tmp = this._context.Set<TEntity>().Find(parms);
                    if(tmp.IsNull_DA())
                    {
                        throw new Exception(string.Format("未找到对象:({0})，可能已经被删除!", json.ToString()));
                    }
                    tmp.State = EntityState.Modified;
                });
                result = this.UpdateEntitys();
            }
            catch(System.InvalidOperationException ex)
            {
                result.Success = false;
                result.Message = "请保证使用AsNoTracking()查询,保证返回的实体不被缓存;" + ex.Message;
                result.BindAllException(ex);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 批量更新(新方法)
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="csdl">Edmx文件模型名称</param>
        /// <returns></returns>
        public ResultInfo<object> UpdateEntity_New<TEntity>(TEntity entity, string csdl = "Model") where TEntity : class,new()
        {
            return UpdateEntitys_New<TEntity>(new List<TEntity>() { entity },csdl);
        }
        [Obsolete("方法存在问题，已过时，请使用新方法")]
        /// <summary>
        /// 更新单个对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ResultInfo<object> UpdateEntity<TEntity, TKey>(TEntity entity, TKey key) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var list = new List<TEntity>();
                list.Add(entity);
                result = UpdateEntitys(list, key);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.BindAllException(ex);
            }

            return result;
        }
        /// <summary>
        /// 删除实体,并返回影响的行数(Data属性);
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns>并返回影响的行数</returns>
        public ResultInfo<object> DeleteEntitys<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var set = _context.Set<TEntity>();
                entityList.ToList().ForEach(entity =>
                {
                    set.Remove(entity);
                });
                int count = _context.SaveChanges();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.BindAllException(ex);
                result.Success = false;
            }
            return result;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entityList"></param>
        /// <param name="keyPropteryName"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteEntitys<TEntity,TKey>(IEnumerable<TEntity> entityList,TKey keyPropteryName) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var keyPName=keyPropteryName.ToString();
                var set = _context.Set<TEntity>();
                entityList.ToList().ForEach(entity =>
                {
                    var tmp = set.AsEnumerable().Where(x => x.GetType().GetProperty(keyPName).GetValue(x, null).ToString() == entity.GetType().GetProperty(keyPName).GetValue(entity, null).ToString()).FirstOrDefault();
                    if(tmp==null)
                    {
                        throw new Exception("未找到要删除的对象!");
                    }
                    set.Remove(tmp);
                });
                int count = _context.SaveChanges();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.BindAllException(ex);
                result.Success = false;
            }
            return result;
        }
        /// <summary>
        /// 删除单个对象,并返回影响的行数(Data属性);
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteEntity<TEntity>(TEntity entity) where TEntity : class,new()
        {
            var result = this.DeleteEntitys(new List<TEntity>() { entity });
            return result;
        }
        /// <summary>
        /// 批量删除(新方法)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityList"></param>
        /// <param name="csdl"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteEntitys_New<TEntity>(IEnumerable<TEntity> entityList, string csdl = "Model") where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                entityList.ToList().ForEach(x => {
                    var entity = _context.Entry(x);
                    entity.State = EntityState.Deleted;
                });
                int count = _context.SaveChanges();
                result.Data = count;
            }
            catch (Exception ex)
            {
                result.BindAllException(ex);
                result.Success = false;
            }
            return result;
        }
        /// <summary>
        /// 批量删除(新方法)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="csdl"></param>
        /// <returns></returns>
        public ResultInfo<object> DeleteEntity_New<TEntity>(TEntity entity, string csdl = "Model") where TEntity : class,new()
        {
            return this.DeleteEntitys_New(new List<TEntity>() { entity },csdl);
        }
        /// <summary>
        /// 获取该类型的实所有实体集合
        /// </summary>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> QueryEntitys<TEntity>(Expression<Func<TEntity, bool>> where = null) where TEntity : class,new()
        {
            try
            {
                IEnumerable<TEntity> set;
                if (where == null)
                    set = _context.Set<TEntity>().AsQueryable();
                else
                    set = _context.Set<TEntity>().Where(where);
                return set;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// SQL查询实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQueryEntitys<TEntity>(string sql,IEnumerable<DbParameter> parms=null) where TEntity : class,new()
        {
            try
            {
                var set = _context.Database.SqlQuery<TEntity>(sql, parms == null ? new object[] { } : parms.ToArray());
                return set;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查询某个类型实体的所有主键
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="ssdl"></param>
        /// <returns></returns>
        public IEnumerable<EntityKeyMemberEx> Get_EntityKeyMembers<TEntity>(TEntity entity, string ssdl = "Model")
        {
            List<EntityKeyMemberEx> keyMemberList = new List<EntityKeyMemberEx>();
            var type=typeof(TEntity);
            var xmlDoc = new System.Xml.XmlDocument();
            var ssdlFile = ssdl + ".csdl";
            var resNames = type.Assembly.GetManifestResourceNames();

            var exist =  resNames.Contains(ssdlFile);
            if(!exist)
                throw new Exception(string.Format("未能加载到资源{0}", ssdlFile));

            var csdlMs = type.Assembly.GetManifestResourceStream(ssdlFile);
            if (csdlMs.IsNull_DA())
                throw new Exception(string.Format("未能加载到资源{0}", ssdlFile));

            xmlDoc.Load(csdlMs);
            var childs = xmlDoc.ChildNodes[1].ChildNodes.Cast<XmlNode>().ToList();//.Where(x => x.LocalName == "EntityType" && x.Name == type.Name).FirstOrDefault();
            var EntityType = childs.Where(x => x.Attributes["Name"].Value == type.Name).FirstOrDefault();
            var key = EntityType.ChildNodes.Cast<XmlNode>().ToList().Where(x => x.LocalName == "Key").FirstOrDefault();
            var keys = key.ChildNodes.Cast<XmlNode>().Select(x => x.Attributes["Name"].Value).ToList();
            keys.ForEach(x => {
                var value = type.GetProperty(x).GetValue(entity, null);
                var valuetype=value.GetType();
                keyMemberList.Add(new EntityKeyMemberEx(x, value, valuetype));
            });
            return keyMemberList;
        }
        #endregion
    }
    public class EntityKeyMemberEx : EntityKeyMember
    {
        public Type ValueType { get; set; }
        public EntityKeyMemberEx(string keyName, object keyValue, Type valueType):base(keyName,keyValue)
        {
            ValueType = valueType;
        }
    }
}
