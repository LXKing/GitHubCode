using System;
using System.Collections.Generic;
using System.Linq;
using XCI.Extension;

namespace XCI.Helper
{
    /// <summary>
    /// 列表辅助操作类
    /// </summary>
    public static class ListHelper
    {

        /// <summary>
        /// 复制列表,返回一个新的列表对象
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        public static IList<T> CopyList<T>(IList<T> list)
        {
            return CopyList(list, null);
        }


        /// <summary>
        /// 复制列表,返回一个新的列表对象
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="func">循环时对每个元素执行的动作</param>
        public static IList<T> CopyList<T>(IList<T> list, Func<T, bool> func)
        {
            if (list.IsNotEmpty())
            {
                IList<T> copyList = new List<T>();
                foreach (T item in list)
                {
                    if (func != null)
                    {
                        if (func(item))
                        {
                            copyList.Add(item);
                        }
                    }
                    else
                    {
                        copyList.Add(item);
                    }
                }
                return copyList;
            }
            return list;
        }


        /// <summary>
        /// 同步列表
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">源列表</param>
        /// <param name="needSynchronizationList">待同步的集合</param>
        /// <param name="func">对象比较器</param>
        public static void Synchronization<T>(IList<T> list, IList<T> needSynchronizationList, Func<T, string> func)
        {
            foreach (T item in list)
            {
                T item1 = item;
                var obj = needSynchronizationList.FirstOrDefault(p => func(p).Equals(func(item1)));
                if (obj == null)
                {
                    needSynchronizationList.Add(item);
                }
            }
        }
    }
}