using XCI.Core;

namespace XCI.Component
{
    public class AppUpdateFactory : BaseFactory<IAppUpdate>
    {
        private static readonly AppUpdateFactory _instance = new AppUpdateFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IAppUpdate GetDefaultProvider()
        {
            return new DefaultAppUpdate();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal AppUpdateFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static AppUpdateFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IAppUpdate Current
        {
            get { return _instance.Default; }
        }

        /// <summary>
        /// 检测更新
        /// </summary>
        /// <returns></returns>
        public static bool CheckUpdate()
        {
            return Current.CheckUpdate();
        }

        /// <summary>
        /// 更新程序
        /// </summary>
        public static void Update()
        {
            Current.Update();
        }
         
    }
}