using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace DataAccess.EF
{
    public class EFDataAccessAsTran : IDisposable
    {
        protected DbContext _context;
        /// <summary>
        /// 用于对基础数据库执行创建/删除/存在性检查操作
        /// </summary>
        protected Database _db;
        public EFDataAccessAsTran(DbContext dbContext)
        {
            _context = dbContext;
            _db = _context.Database;
        }
        public EFDataAccessAsTran(string connString)
        {
            _context = new DbContext(connString);
            _db = _context.Database;
        }
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public ResultInfo<object> AddEntitysAsTran<TEntity>(IEnumerable<TEntity> entityList)
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
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }
        public ResultInfo<object> AddEntityAsTran<TEntity>(TEntity entity) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                this.AddEntitysAsTran(new List<TEntity>() { entity });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.BindAllException(ex);
            }
            return result;
        }

        public ResultInfo<object> DeleteEntitysAsTran<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var set = _context.Set<TEntity>();
                entityList.ToList().ForEach(entity =>
                {
                    set.Remove(entity);
                });
            }
            catch (Exception ex)
            {
                result.BindAllException(ex);
                result.Success = false;
            }
            return result;
        }

        public ResultInfo<object> DeleteEntitysAsTran<TEntity, TKey>(IEnumerable<TEntity> entityList, TKey keyPropteryName) where TEntity : class,new()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
                var keyPName = keyPropteryName.ToString();
                var set = _context.Set<TEntity>();

                entityList.ToList().ForEach(entity =>
                {
                    var tmp = set.AsEnumerable().Where(x => x.GetType().GetProperty(keyPName).GetValue(x, null).ToString() == entity.GetType().GetProperty(keyPName).GetValue(entity, null).ToString()).FirstOrDefault();
                    if (tmp == null)
                    {
                        throw new Exception("未找到要删除的对象!");
                    }
                    set.Remove(tmp);
                });
            }
            catch (Exception ex)
            {
                result.BindAllException(ex);
                result.Success = false;
            }
            return result;
        }

        public ResultInfo<object> DeleteEntityAsTran<TEntity>(TEntity entity) where TEntity : class,new()
        {
            var count = this.DeleteEntitysAsTran(new List<TEntity>() { entity });
            return count;
        }

        public ResultInfo<object> CommitTransaction()
        {
            ResultInfo<object> result = new ResultInfo<object>();
            try
            {
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
    }
}
