using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using XCI.Helper;

namespace XCI.Core
{
    /// <summary>
    /// 表示可通过索引访问的对象的强类型列表。提供用于对列表进行搜索、排序和操作的方法
    /// </summary>
    /// <typeparam name="T">列表中元素的类型</typeparam>
    [XmlRoot("XCIList")]
    [Serializable]
    public class XCIList<T> : List<T>, IList<T>
    {
        public XCIList()
        {

        }

        public XCIList(IEnumerable<T> collection)
            : base(collection)
        {

        }

        /// <summary>
        /// 确定某元素是否在列表中
        /// </summary>
        /// <param name="predicate">查找器</param>
        /// <returns>如果在列表中找到 item，则为 true，否则为 false</returns>
        public bool Contains(Func<T, bool> predicate)
        {
            for (int i = 0; i < this.Count; i++)
            {
                T item = this[i];
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从列表中移除特定对象的第一个匹配项
        /// </summary>
        /// <param name="predicate">查找器</param>
        public void Remove(Func<T, bool> predicate)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                T item = this[i];
                if (predicate(item))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                this.RemoveAt(index);
            }
        }

        /// <summary>
        /// 搜索指定的对象，并返回整个列表中第一个匹配项的从零开始的索引
        /// </summary>
        /// <param name="predicate">查找器</param>
        /// <returns>如果在整个列表中找到 item 的第一个匹配项，则为该项的从零开始的索引；否则为-1</returns>
        public int IndexOf(Func<T, bool> predicate)
        {
            for (int i = 0; i < this.Count; i++)
            {
                T item = this[i];
                if (predicate(item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 索指定的对象，并返回整个列表中第一个匹配项的元素
        /// </summary>
        /// <param name="predicate">查找器</param>
        /// <returns>返回整个列表中第一个匹配项的元素</returns>
        public T First(Func<T, bool> predicate)
        {
            for (int i = 0; i < this.Count; i++)
            {
                T item = this[i];
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        public T First()
        {
            return this[0];
        }

        /// <summary>
        /// 修改实体对象
        /// </summary>
        /// <param name="item">实体对象</param>
        public void Edit(T item)
        {
            int index = IndexOf(item);
            this[index] = item;
        }

        public void AddOrUpdate(T item)
        {
            var index = IndexOf(item);
            if (index == -1)
            {
                this.Add(item);
            }
            else
            {
                this[index] = item;
            }
        }

        public void AddOrUpdate(Func<T, bool> predicate, T item)
        {
            var index = IndexOf(predicate);
            if (index == -1)
            {
                this.Add(item);
            }
            else
            {
                this[index] = item;
            }
        }

        public XCIList<T> Copy()
        {
            XCIList<T> list = new XCIList<T>();
            foreach (var item in this)
            {
                list.Add(item);
            }
            return list;
        }

        public XCIList<T> Copy(Func<T, bool> predicate)
        {
            XCIList<T> list = new XCIList<T>();
            foreach (var item in this)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public XCIList<T> Where(Func<T, bool> predicate)
        {
            XCIList<T> list = new XCIList<T>();
            foreach (var item in this)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 保存数据到Xml
        /// </summary>
        /// <param name="path">文件路径</param>
        public void SaveDataAsXml(string path)
        {
            XmlHelper.XmlSerializePath(path, this);
        }

        /// <summary>
        /// 读取数据从Xml
        /// </summary>
        /// <param name="path">文件路径</param>
        public void LoadDataFromXml(string path)
        {
            var data = XmlHelper.XmlDeserializePath<XCIList<T>>(path);
            if (data != null)
            {
                this.AddRange(data);
            }
        }

        /// <summary>
        /// 读取数据从Xml
        /// </summary>
        /// <param name="dataString">数据内容</param>
        public void LoadDataFromXmlString(string dataString)
        {
            var data = XmlHelper.XmlDeserialize<XCIList<T>>(dataString);
            if (data != null)
            {
                this.AddRange(data);
            }
        }

        /// <summary>
        /// 保存数据到二进制文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public void SaveDataAsByte(string path)
        {
            var datas = StreamHelper.Serialize(this);
            string txt = Convert.ToBase64String(datas, Base64FormattingOptions.InsertLineBreaks);
            File.WriteAllText(path, txt);
        }

        /// <summary>
        /// 读取数据从二进制文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public void LoadDataFromByte(string path)
        {
            FileHelper.CreateDirectoryByPath(path);
            if (File.Exists(path))
            {
                var txt = File.ReadAllText(path);
                LoadDataFromByteString(txt);
            }
        }

        /// <summary>
        /// 读取数据从二进制文件
        /// </summary>
        /// <param name="dataString">数据内容</param>
        public void LoadDataFromByteString(string dataString)
        {
            var datas = Convert.FromBase64String(dataString);
            var data = StreamHelper.Deserialize(datas);
            if (data != null)
            {
                this.AddRange((XCIList<T>)data);
            }
        }
    }
}