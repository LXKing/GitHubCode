using XCI.Core;

namespace XCI.Component
{
    public class SynchronousDateTimeFactory:BaseFactory<ISynchronousDateTime>
    {
        private static readonly SynchronousDateTimeFactory _instance = new SynchronousDateTimeFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override ISynchronousDateTime GetDefaultProvider()
        {
            return new SynchronousDateTimeWeb();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal SynchronousDateTimeFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static SynchronousDateTimeFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static ISynchronousDateTime Current
        {
            get { return _instance.Default; }
        }
    }
}