using System.Linq;
using XCI.Core;

namespace XCI.Component
{
    public abstract class DataDictionaryBase : IDataDictionary
    {
        protected readonly XCIList<DataDictionaryEntity> DictionaryData = new XCIList<DataDictionaryEntity>();

        /// <summary>
        /// 获取数据字典总数
        /// </summary>
        public int Count
        {
            get { return DictionaryData.Count; }
        }

        /// <summary>
        /// 获取或设置指定数据字典名称的字典列表
        /// </summary>
        /// <param name="key">数据字典名称</param>
        /// <returns>字典列表</returns>
        public XCIList<DataDictionaryEntity> this[string key]
        {
            get { return Get(key); }
            set { AddOrUpdate(value); }
        }

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <param name="itemValue">项值</param>
        public void AddOrUpdate(string name, string itemName, string itemValue)
        {
            AddOrUpdate(name, itemName, itemValue, null);
        }

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <param name="itemValue">项值</param>
        /// <param name="comment">备注</param>
        public void AddOrUpdate(string name, string itemName, string itemValue, string comment)
        {
            DataDictionaryEntity entity = new DataDictionaryEntity();
            entity.Code = name;
            entity.ItemName = itemName;
            entity.ItemValue = itemValue;
            entity.Comment = comment;
            AddOrUpdate(entity);
        }

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="entity">字典对象</param>
        public void AddOrUpdate(DataDictionaryEntity entity)
        {
            var index = DictionaryData.IndexOf(p => p.Code.Equals(entity.Code) && p.ItemName.Equals(entity.ItemName));
            if (index == -1)
            {
                if (Insert(entity))
                {
                    DictionaryData.Add(entity);
                }
            }
            else
            {
                DataDictionaryEntity _entity = DictionaryData[index];
                _entity.ItemName = entity.ItemName;
                _entity.ItemValue = entity.ItemValue;
                if (!string.IsNullOrEmpty(entity.Comment))
                {
                    entity.Comment = entity.Comment;
                }
                Update(_entity);
            }
        }

        /// <summary>
        /// 添加或者更新数据字典
        /// </summary>
        /// <param name="data">字典列表</param>
        public void AddOrUpdate(XCIList<DataDictionaryEntity> data)
        {
            for (int index = 0; index < data.Count; index++)
            {
                var item = data[index];
                AddOrUpdate(item);
            }
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns>成功返回true</returns>
        public void Remove(string name)
        {
            if (Delete(name))
            {
                DictionaryData.Remove(p => p.Code.Equals(name));
            }
        }

        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>成功返回true</returns>
        public void Remove(string name, string itemName)
        {
            if (Delete(name,itemName))
            {
                DictionaryData.Remove(p => p.Code.Equals(name) && p.ItemName.Equals(itemName));
            }
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        public XCIList<DataDictionaryEntity> Get(string name)
        {
            XCIList<DataDictionaryEntity> list = new XCIList<DataDictionaryEntity>();
            var tempList = DictionaryData.Where(p => p.Code.Equals(name));
            list.AddRange(tempList);
            return list;
        }

        /// <summary>
        /// 获取数据字典 如果没有指定名称的字典 则返回默认指定的项名称和项值
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="defaultItemName">项名称</param>
        /// <param name="defaultItemValue">项值</param>
        public XCIList<DataDictionaryEntity> Get(string name, string defaultItemName, string defaultItemValue)
        {
            if (!Contains(name))
            {
                DataDictionaryEntity entity = new DataDictionaryEntity();
                entity.Code = name;
                entity.ItemName = defaultItemName;
                entity.ItemValue = defaultItemValue;
                return new XCIList<DataDictionaryEntity> { entity };
            }
            return Get(name);
        }

        /// <summary>
        /// 获取数据字典 如果没有则添加数据字典
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <param name="defaultItemName">项名称</param>
        /// <param name="defaultItemValue">项值</param>
        public XCIList<DataDictionaryEntity> GetOrAdd(string name, string defaultItemName, string defaultItemValue)
        {
            if (!Contains(name))
            {
                AddOrUpdate(name, defaultItemName, defaultItemValue);
            }
            return Get(name);
        }

        /// <summary>
        /// 是否存在数据字典
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <returns>存在返回true</returns>
        public bool Contains(string name)
        {
            return DictionaryData.Any(p => p.Code.Equals(name));
        }

        /// <summary>
        /// 是否存在数据字典项
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>存在返回true</returns>
        public bool Contains(string name, string itemName)
        {
            return DictionaryData.Any(p => p.Code.Equals(name) && p.ItemName.Equals(itemName));
        }

        /// <summary>
        /// 获取数据字典位置
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典索引号</returns>
        public int IndexOf(string name)
        {
            return DictionaryData.IndexOf(p => p.Code.Equals(name));
        }

        /// <summary>
        /// 获取数据字典项位置
        /// </summary>
        /// <param name="name">数据字典名称</param>
        /// <param name="itemName">项名称</param>
        /// <returns>数据字典索引号</returns>
        public int IndexOf(string name, string itemName)
        {
            return DictionaryData.IndexOf(p => p.Code.Equals(name) && p.ItemName.Equals(itemName));
        }

        /// <summary>
        /// 清空数据字典
        /// </summary>
        public void Clear()
        {
            DeleteAll();
            DictionaryData.Clear();
        }

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        public XCIList<DataDictionaryEntity> GetList()
        {
            return DictionaryData;
        }

        /// <summary>
        /// 获取名称列表
        /// </summary>
        public XCIList<string> GetNameList()
        {
            XCIList<string> nameList = new XCIList<string>();
            foreach (var item in DictionaryData)
            {
                nameList.AddOrUpdate(item.Code);
            }
            return nameList;
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
        /// 插入数据字典对象
        /// </summary>
        /// <param name="entity">数据字典实体</param>
        protected virtual bool Insert(DataDictionaryEntity entity)
        {
            return true;
        }

        /// <summary>
        /// 更新数据字典对象
        /// </summary>
        /// <param name="entity">数据字典实体</param>
        protected virtual bool Update(DataDictionaryEntity entity)
        {
            return true;
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="code">编码</param>
        protected virtual bool Delete(string code)
        {
            return true;
        }

        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="itemName">名称</param>
        protected virtual bool Delete(string code, string itemName)
        {
            return true;
        }

        /// <summary>
        /// 删除全部数据字典对象
        /// </summary>
        protected virtual void DeleteAll()
        {

        }
    }
}