using System.Reflection;

namespace XCI.Core
{
    /// <summary>
    /// 快速方法信息
    /// </summary>
    public sealed class FastMethodInfo
    {
        /// <summary>
        /// 方法对象
        /// </summary>
        public MethodInfo method;
        /// <summary>
        /// 方法调用委托
        /// </summary>
        private FastInvokeHandler callHandler;
        /// <summary>
        /// 快速方法信息
        /// </summary>
        /// <param name="method">方法元数据</param>
        public FastMethodInfo(MethodInfo method)
        {
            this.method = method;
        }
        /// <summary>
        /// 快速调用方法
        /// </summary>
        /// <param name="instanse">实例</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public object Call(object instanse, params object[] parameters)
        {
            if (callHandler == null)
            {
                callHandler = method.GetFastInvoker();
            }
            return callHandler(instanse, parameters);
        }
    }
}
