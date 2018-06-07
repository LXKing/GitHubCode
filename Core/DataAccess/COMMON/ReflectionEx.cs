using System.ComponentModel;
using System.Reflection;
using System.Web.UI;

namespace System
{
    /// <summary>
    /// 反射扩展方法(Reflection)
    /// </summary>
    public static class ReflectionEx
    {
        /// <summary>
        /// 获取属性的描述特性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetDefaultValue(PropertyDescriptor property)
        {
            DefaultValueAttribute attribute = property.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }
        /// <summary>
        /// 获取属性的默认值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetDefaultValue(PropertyInfo property)
        {
            object[] customAttributes = property.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            if (customAttributes.Length <= 0)
            {
                return null;
            }
            return ((DefaultValueAttribute) customAttributes[0]).Value;
        }
        /// <summary>
        /// 获取当前UI控件的父类型
        /// </summary>
        /// <param name="control"></param>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static Control GetTypeOfParent(Control control, string typeFullName)
        {
            for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
            {
                if (IsTypeOf(control2, typeFullName))
                {
                    return control2;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Control GetTypeOfParent(Control control, Type type)
        {
            return GetTypeOfParent(control, type.FullName);
        }
        
        public static bool IsInTypeOf(Control control, string typeFullName)
        {
            return (GetTypeOfParent(control, typeFullName) != null);
        }
        
        public static bool IsInTypeOf(Control control, Type type)
        {
            return IsInTypeOf(control, type.FullName);
        }
        
        public static bool IsTypeOf(object obj, string typeFullName)
        {
            return IsTypeOf(obj, typeFullName, false);
        }
        
        public static bool IsTypeOf(object obj, Type type)
        {
            return IsTypeOf(obj, type.FullName, false);
        }
        
        public static bool IsTypeOf(object obj, string typeFullName, bool shallow)
        {
            if (obj != null)
            {
                if (shallow)
                {
                    return obj.GetType().FullName.Equals(typeFullName);
                }
                Type baseType = obj.GetType();
                for (string str = baseType.FullName; !str.Equals("System.Object"); str = baseType.FullName)
                {
                    if (str.Equals(typeFullName))
                    {
                        return true;
                    }
                    baseType = baseType.BaseType;
                }
            }
            return false;
        }
        
        public static bool IsTypeOf(object obj, Type type, bool shallow)
        {
            return IsTypeOf(obj, type.FullName, shallow);
        }
    }
}
