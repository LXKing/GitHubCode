using System.ComponentModel;
using System.Reflection;

namespace System
{
    /// <summary>
    /// ������չ����(Reflection)
    /// </summary>
    public static class ReflectionEx
    {
        /// <summary>
        /// ��ȡ���Ե���������
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
        /// ��ȡ���Ե�Ĭ��ֵ
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
