using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 创建工厂对象操作帮助类
    /// </summary>
    /// <typeparam name="TKey">Key数据类型</typeparam>
    /// <typeparam name="T">返回值数据类型</typeparam>
    public static class FactoryHelper<TKey, T>
    {
        /// <summary>
        /// 对象创建字典
        /// </summary>
        private static readonly IDictionary<TKey, Func<T>> creators = new Dictionary<TKey, Func<T>>();
        
        /// <summary>
        /// 默认创建函数
        /// </summary>
        private static Func<T> defaultCreator { get; set; }
        

        /// <summary>
        /// 注册创建对象
        /// </summary>
        /// <param name="key">创建Key</param>
        /// <param name="result">Result of key.</param>
        public static void Register(TKey key, T result)
        {
            creators[key] = new Func<T>(() => result);
        }        


        /// <summary>
        /// 注册创建对象
        /// </summary>
        /// <param name="key">创建Key</param>
        /// <param name="creator">Corresponding creation function.</param>
        public static void Register(TKey key, Func<T> creator)
        {
            creators[key] = creator;
        }


        /// <summary>
        /// 注册默认返回对象
        /// </summary>
        /// <param name="result">默认返回对象</param>
        public static void RegisterDefault(T result)
        {
            defaultCreator = new Func<T>(() => result);
        }
        
        
        /// <summary>
        /// 注册默认创建对象函数
        /// </summary>
        /// <param name="creator">创建对象函数</param>
        public static void RegisterDefault(Func<T> creator)
        {
            defaultCreator = creator;
        }


        /// <summary>
        /// 是否包含指定Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasKey(TKey key)
        {
            return creators.ContainsKey(key);
        }


        /// <summary>
        /// 使用指定Key创建对象实例
        /// </summary>
        /// <param name="key">创建Key</param>
        public static T Create(TKey key)
        {
            if (!creators.ContainsKey(key))
                return default(T);

            return creators[key]();
        }


        /// <summary>
        /// 创建默认对象
        /// </summary>
        public static T Create()
        {
            return defaultCreator();
        }
    }
}
