using XCI.Core;

namespace XCI.Component
{
    public class DataDictionaryFactory: BaseFactory<IDataDictionary>
    {
        private static readonly DataDictionaryFactory _instance = new DataDictionaryFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IDataDictionary GetDefaultProvider()
        {
            return new XmlDataDictionary();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal DataDictionaryFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static DataDictionaryFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IDataDictionary Current
        {
            get { return _instance.Default; }
        }
    }
}