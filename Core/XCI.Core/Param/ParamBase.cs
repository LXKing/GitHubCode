using System;
using System.Linq;
using XCI.Core;
namespace XCI.Component
{
    public abstract class ParamBase : IParam
    {
        protected readonly XCIList<ParamEntity> ParamData = new XCIList<ParamEntity>();

        /// <summary>
        /// 获取参数总数
        /// </summary>
        public int Count
        {
            get { return ParamData.Count; }
        }

        /// <summary>
        /// 获取或设置指定参数名称的参数值
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        public string this[string key]
        {
            get { return Get(key); }
            set { AddOrUpdate(key, value); }
        }

        /// <summary>
        /// 添加或者更新参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        public void AddOrUpdate(string key, string value)
        {
            AddOrUpdate(key, value, null, null);
        }

        /// <summary>
        /// 添加或者更新参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="comment">描述</param>
        /// <param name="category">分类</param>
        public void AddOrUpdate(string key, string value, string comment, string category)
        {
            var index = ParamData.IndexOf(p => p.Key.Equals(key));
            if (index == -1)
            {
                ParamEntity entity = new ParamEntity();
                entity.Key = key;
                entity.Value = value;
                entity.Comment = comment;
                entity.Category = category;
                if(Insert(entity))
                {
                    ParamData.Add(entity);
                }
            }
            else
            {
                ParamEntity entity = ParamData[index];
                entity.Key = key;
                entity.Value = value;
                if (!string.IsNullOrEmpty(comment))
                {
                    entity.Comment = comment;
                }
                if (!string.IsNullOrEmpty(category))
                {
                    entity.Category = category;
                }
                Update(entity);
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>成功返回true</returns>
        public void Remove(string key)
        {
            var entity = ParamData.First(p => p.Key.Equals(key));
            if (entity != null)
            {
                Delete(entity);
                ParamData.Remove(entity);
            }
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数值</returns>
        public string Get(string key)
        {
            return Get(key, string.Empty);
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="defaultValue">获取失败时的默认值</param>
        /// <returns>参数值</returns>
        public string Get(string key, string defaultValue)
        {
            var index = ParamData.IndexOf(p => p.Key.Equals(key));
            if (index == -1)
            {
                return defaultValue;
            }
            return ParamData[index].Value;
        }

        /// <summary>
        /// 获取参数 如果没有则添加参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="defaultValue">参数默认值</param>
        /// <returns>参数值</returns>
        public string GetOrAdd(string key, string defaultValue)
        {
            var index = ParamData.IndexOf(p => p.Key.Equals(key));
            if (index == -1)
            {
                AddOrUpdate(key, defaultValue);
                return defaultValue;
            }
            return ParamData[index].Value;
        }

        /// <summary>
        /// 是否存在参数
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>存在返回true</returns>
        public bool Contains(string key)
        {
            return ParamData.Contains(p => p.Key.Equals(key));
        }

        /// <summary>
        /// 获取参数位置
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>参数索引号</returns>
        public int IndexOf(string key)
        {
            return ParamData.IndexOf(p => p.Key.Equals(key));
        }

        /// <summary>
        /// 清空参数
        /// </summary>
        public void Clear()
        {
            DeleteAll();
            ParamData.Clear();
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns>参数列表</returns>
        public XCIList<ParamEntity> GetParamList()
        {
            return ParamData;
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        public XCIList<string> GetCategory()
        {
            XCIList<string> categoryList = new XCIList<string>();
            foreach (var item in ParamData)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    categoryList.AddOrUpdate(item.Category);
                }
            }
            return categoryList;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// 加载
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// 插入参数对象
        /// </summary>
        /// <param name="entity">参数实体</param>
        protected virtual bool Insert(ParamEntity entity)
        {
            return true;
        }

        /// <summary>
        /// 更新参数对象
        /// </summary>
        /// <param name="entity">参数实体</param>
        protected virtual bool Update(ParamEntity entity)
        {
            return true;
        }

        /// <summary>
        /// 删除参数对象
        /// </summary>
        /// <param name="entity">参数实体</param>
        protected virtual bool Delete(ParamEntity entity)
        {
            return true;
        }

        /// <summary>
        /// 删除全部参数对象
        /// </summary>
        protected virtual void DeleteAll()
        {

        }
    }
}