using XCI.Core;

namespace XCI.Component
{
    public class QueryBuildFactory: BaseFactory<IQueryBuild>
    {
        private static readonly QueryBuildFactory _instance = new QueryBuildFactory();

        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override IQueryBuild GetDefaultProvider()
        {
            return new SqlServerQueryBuild();
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal QueryBuildFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static QueryBuildFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IQueryBuild Current
        {
            get { return _instance.Default; }
        }
    }
}