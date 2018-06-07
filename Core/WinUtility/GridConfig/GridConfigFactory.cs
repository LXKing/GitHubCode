using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 表格配置存取对象工厂
    /// </summary>
    public class GridConfigFactory : ServiceFactory<IGridConfig>
    {
        private static readonly GridConfigFactory _instance = new GridConfigFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IGridConfig GetDefaultProvider()
        {
            return new XmlGridConfig();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal GridConfigFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static GridConfigFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IGridConfig Current
        {
            get { return _instance.Default; }
        }
    }
}