using XCI.Core;
namespace XCI.Component
{
    public class UserParamFactory : BaseFactory<IUserParam>
    {
        private static readonly UserParamFactory _instance = new UserParamFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IUserParam GetDefaultProvider()
        {
            return new XmlUserParam();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal UserParamFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static UserParamFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IUserParam Current
        {
            get { return _instance.Default; }
        }
    }
}