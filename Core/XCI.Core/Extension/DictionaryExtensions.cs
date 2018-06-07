using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace XCI.Extension
{
    /// <summary>
    /// 字典扩展操作
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 判断字典不为空并且集合大于零
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source"></param>
        /// <returns>字典不为空并且集合大于零返回true</returns>
        public static bool HasItem<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            return source != null && source.Count > 0;
        }

        /// <summary>
        /// 连续添加
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <returns>返回字典本身</returns>
        public static IDictionary<TKey, TValue> AddEx<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            source.Add(key, value);
            return source;
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source != null)
            {
                if (!source.ContainsKey(key))
                {
                    source.Add(key, value);
                }
                else
                {
                    source[key] = value;
                }
            }
        }


        /// <summary>
        /// 获取指定key键值 如果不存在返回默认值
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            if (source != null && source.ContainsKey(key))
            {
                return source[key];
            }
            return default(TValue);
        }


        /// <summary>
        /// 安全添加
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <returns>添加成功返回true</returns>
        public static bool SafeAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source != null && !source.ContainsKey(key))
            {
                source.Add(key, value);
                return true;
            }
            return false;
        }


        /// <summary>
        /// 安全更新
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <returns>更新成功返回true</returns>
        public static bool SafeUpdate<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source != null && source.ContainsKey(key))
            {
                source[key] = value;
                return true;
            }
            return false;
        }


        /// <summary>
        /// 安全删除
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型</typeparam>
        /// <typeparam name="TValue">字典中的值的类型</typeparam>
        /// <param name="source">指定的字典</param>
        /// <param name="key">Key值</param>
        /// <returns>删除成功返回true</returns>
        public static bool SafeRemove<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            if (source != null && source.ContainsKey(key))
            {
                source.Remove(key);
                return true;
            }
            return false;
        }

    }
}
