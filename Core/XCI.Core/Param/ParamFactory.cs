using XCI.Core;

namespace XCI.Component
{
    public class ParamFactory : BaseFactory<IParam>
    {
        private static readonly ParamFactory _instance = new ParamFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IParam GetDefaultProvider()
        {
            return new XmlParam();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal ParamFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static ParamFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IParam Current
        {
            get { return _instance.Default; }
        }
    }
}