using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 表格配置模板存取对象工厂
    /// </summary>
    public class GridConfigTemplateFactory : ServiceFactory<IGridConfigTemplate>
    {
        private static readonly GridConfigTemplateFactory _instance = new GridConfigTemplateFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IGridConfigTemplate GetDefaultProvider()
        {
            return new XmlGridConfigTemplate();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal GridConfigTemplateFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static GridConfigTemplateFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IGridConfigTemplate Current
        {
            get { return _instance.Default; }
        }
    }
}