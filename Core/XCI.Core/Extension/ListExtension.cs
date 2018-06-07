using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCI.Helper;

namespace XCI.Extension
{
    /// <summary>
    /// 列表扩展操作
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// 判断列表是否不为空并且列表中元素数目大于零
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        public static bool HasItem<T>(this IList<T> list)
        {
            return (list != null && list.Count > 0);
        }

        /// <summary>
        /// 列表排序
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="comparison">比较器</param>
        public static IList<T> Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            ((List<T>)list).Sort(comparison);
            return list;
        }


        /// <summary>
        /// 添加或者更新(不存在就添加 否则更新)
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="func">查找器</param>
        /// <param name="entity">对象</param>
        public static void AddOrUpdate<T>(this IList<T> list, Func<T, bool> func, T entity)
        {
            var index = list.IndexOf(func);
            if (index >= 0)
            {
                list[index] = entity;
            }
            else
            {
                list.Add(entity);
            }
        }


        /// <summary>
        /// 获取指定项在集合中的索引
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="func">比较器</param>
        /// <returns>如果不存在返回-1</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> func)
        {
            int index = 0;
            foreach (T item in list)
            {
                if (func(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// 获取指定项在集合中的索引
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="func">比较器</param>
        /// <returns>如果不存在返回-1</returns>
        public static int IndexOf<T>(this ICollection list, Func<T, bool> func)
        {
            int index = 0;
            foreach (T item in list)
            {
                if (func(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
        

        /// <summary>
        /// 把集合添加到当前集合
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="array">要添加的集合</param>
        public static IList<T> AddRange<T>(this IList<T> list, ICollection<T> array)
        {
            if (list == null || array == null)
            {
                return list;
            }

            foreach (T item in array)
            {
                list.Add(item);
            }

            return list;
        }


        /// <summary>
        /// 循环执行指定的动作
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="array">列表</param>
        /// <param name="action">动作</param>
        public static void ForEach<T>(this ICollection<T> array, Action<T> action)
        {
            foreach (var item in array)
            {
                action(item);
            }
        }


        /// <summary>
        /// 检测集合中是否有空值
        /// </summary>
        /// <typeparam name="T">列表中元素的类型</typeparam>
        /// <param name="list">列表</param>
        public static bool HasAnyNull<T>(this ICollection<T> list)
        {
            return IsTrueForAny(list, t => t == null);
        }


        /// <summary>
        /// 通过指定的动作检查集合 只要有任意一个为true就返回true
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="func">动作</param>
        /// <remarksreturns>只要有任意一个为true 就返回true</remarksreturns>
        public static bool IsTrueForAny<T>(this ICollection<T> list, Func<T, bool> func)
        {
            foreach (T item in list)
            {
                bool result = func(item);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 通过指定的动作检查集合 只要有任意一个为false就返回false
        /// </summary>
        /// <param name="array">列表</param>
        /// <param name="func">动作</param>
        /// <returns>只要有任意一个为false 就返回false</returns>
        public static bool IsTrueForAll<T>(this ICollection<T> array, Func<T, bool> func)
        {
            foreach (T item in array)
            {
                bool result = func(item);
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 转为字典
        /// </summary>
        /// <param name="array">列表</param>
        public static IDictionary<T, T> ToDictionary<T>(this ICollection<T> array)
        {
            IDictionary<T, T> dict = new Dictionary<T, T>();
            foreach (T item in array)
            {
                dict[item] = item;
            }
            return dict;
        }

    }
}
