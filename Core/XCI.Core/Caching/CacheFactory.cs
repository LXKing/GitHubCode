using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Caching;
using System.Web;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// �������
    /// </summary>
    public class CacheFactory : BaseFactory<ICache>
    {
        private static readonly CacheFactory _instance = new CacheFactory();
        
        /// <summary>
        /// ��ȡĬ��ʵ�ֶ���
        /// </summary>
        public override ICache GetDefaultProvider()
        {
            return new HttpRuntimeCache();
        }
        
        /// <summary>
        /// �������� ֻ�����Լ�����
        /// </summary>
        internal CacheFactory()
        {
        }

        /// <summary>
        /// ʵ���������
        /// </summary>
        public static CacheFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// Ĭ��ʵ�ֶ��� ��ǰʵ�ֶ���
        /// </summary>
        public static ICache Current
        {
            get { return _instance.Default; }
        }
    }
}
