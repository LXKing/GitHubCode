using System.Reflection;

namespace System
{
    public static class ObjectEx
    {
        /// <summary>
        /// 克隆所有属性(适合单个对象)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <param name="ignoreDefaultValues">忽略默认值</param>
        /// <returns></returns>
        public static T Apply<T>(this T from, T target, bool ignoreDefaultValues) where T : class
        {
            object obj2 = null;
            object defaultValue = null;
            foreach (PropertyInfo info2 in from.GetType().GetProperties())
            {
                if (info2.CanRead)
                {
                    obj2 = info2.GetValue(from, null);
                    if (ignoreDefaultValues)
                    {
                        defaultValue = ReflectionEx.GetDefaultValue(info2);
                        if ((obj2 != null) && obj2.Equals(defaultValue))
                        {
                            continue;
                        }
                    }
                    if (obj2 != null)
                    {
                        PropertyInfo property = target.GetType().GetProperty(info2.Name, BindingFlags.Public | BindingFlags.Instance);
                        if (((property != null) && property.CanWrite) && ((property != null) && property.GetType().Equals(info2.GetType())))
                        {
                            property.SetValue(target, obj2, null);
                        }
                    }
                }
            }
            return target;
        }
    }
}
