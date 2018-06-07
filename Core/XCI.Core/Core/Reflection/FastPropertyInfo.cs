using System.Reflection;
using XCI.Helper;
using System;

namespace XCI.Core
{
    /// <summary>
    /// 快速属性信息
    /// </summary>
    public sealed class FastPropertyInfo
    {
        /// <summary>
        /// 默认属性
        /// </summary>
        public PropertyInfo Property { get; private set; }
        /// <summary>
        /// 获取属性值委托
        /// </summary>
        private FastInvokeHandler getHandler;
        /// <summary>
        /// 设置属性值委托
        /// </summary>
        private FastInvokeHandler setHandler;
        /// <summary>
        /// 快速属性信息
        /// </summary>
        /// <param name="property">属性元数据</param>
        public FastPropertyInfo(PropertyInfo property)
        {
            Property = property;
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="instanse">实例</param>
        /// <returns>属性值</returns>
        public object Get(object instanse)
        {
            if (getHandler == null)
            {
                getHandler = Property.GetGetMethod().GetFastInvoker();
            }
            return getHandler(instanse, null);
        }
        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="instanse">实例</param>
        /// <param name="value">设置的值</param>
        public void Set(object instanse, object value)
        {
            if (!IsNullableType(Property.PropertyType)&&value == null) return;
            if (setHandler == null)
            {
                setHandler = Property.GetSetMethod().GetFastInvoker();
            }
           
            setHandler(instanse, value);
        }

        public bool IsNullableType(System.Type theType)
        {
            if (!theType.IsValueType) return true;
            return (theType.UnderlyingSystemType.IsGenericType && theType.UnderlyingSystemType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

    }
}
