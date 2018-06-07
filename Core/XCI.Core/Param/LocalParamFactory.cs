using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 本地参数
    /// </summary>
    public static class LocalParamFactory
    {
        private static IParam _xmlprovider;
        private static IParam XmlProvider
        {
            get
            {
                if (_xmlprovider == null)
                {
                    _xmlprovider = new XmlParam("LocalParam.xml");
                }
                return _xmlprovider;
            }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static IParam Current
        {
            get { return XmlProvider; }
        }
    }
}