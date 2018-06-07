using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// ���������������������
    /// </summary>
    /// <typeparam name="TKey">Key��������</typeparam>
    /// <typeparam name="T">����ֵ��������</typeparam>
    public static class FactoryHelper<TKey, T>
    {
        /// <summary>
        /// ���󴴽��ֵ�
        /// </summary>
        private static readonly IDictionary<TKey, Func<T>> creators = new Dictionary<TKey, Func<T>>();
        
        /// <summary>
        /// Ĭ�ϴ�������
        /// </summary>
        private static Func<T> defaultCreator { get; set; }
        

        /// <summary>
        /// ע�ᴴ������
        /// </summary>
        /// <param name="key">����Key</param>
        /// <param name="result">Result of key.</param>
        public static void Register(TKey key, T result)
        {
            creators[key] = new Func<T>(() => result);
        }        


        /// <summary>
        /// ע�ᴴ������
        /// </summary>
        /// <param name="key">����Key</param>
        /// <param name="creator">Corresponding creation function.</param>
        public static void Register(TKey key, Func<T> creator)
        {
            creators[key] = creator;
        }


        /// <summary>
        /// ע��Ĭ�Ϸ��ض���
        /// </summary>
        /// <param name="result">Ĭ�Ϸ��ض���</param>
        public static void RegisterDefault(T result)
        {
            defaultCreator = new Func<T>(() => result);
        }
        
        
        /// <summary>
        /// ע��Ĭ�ϴ���������
        /// </summary>
        /// <param name="creator">����������</param>
        public static void RegisterDefault(Func<T> creator)
        {
            defaultCreator = creator;
        }


        /// <summary>
        /// �Ƿ����ָ��Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasKey(TKey key)
        {
            return creators.ContainsKey(key);
        }


        /// <summary>
        /// ʹ��ָ��Key��������ʵ��
        /// </summary>
        /// <param name="key">����Key</param>
        public static T Create(TKey key)
        {
            if (!creators.ContainsKey(key))
                return default(T);

            return creators[key]();
        }


        /// <summary>
        /// ����Ĭ�϶���
        /// </summary>
        public static T Create()
        {
            return defaultCreator();
        }
    }
}
