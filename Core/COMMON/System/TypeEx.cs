using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeEx
    {
        /// <summary>
        /// 判断该成员对象的类型(方法、构造函数何事件等)
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static MemberTypes MemberType(this MemberInfo memberInfo)
        {
            return memberInfo.MemberType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ContainsGenericParameters(this Type type)
        {
            return type.ContainsGenericParameters;
        }
        public static bool IsInterface(this Type type)
        {
            return type.IsInterface;
        }
        /// <summary>
        /// 判断当前类型是否是泛型类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericType(this Type type)
        {
            return type.IsGenericType;
        }
        /// <summary>
        /// 获取一个值，该值指示当前 System.Type 是否表示可以用来构造其他泛型类型的泛型类型定义。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericTypeDefinition(this Type type)
        {
            return type.IsGenericTypeDefinition;
        }
        /// <summary>
        /// 返回当前类型的基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type BaseType(this Type type)
        {
            return type.BaseType;
        }
        /// <summary>
        /// 判断当前类型是否表示枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }
        /// <summary>
        /// 判断当前类型是否是一个类(不是值类型或接口)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsClass(this Type type)
        {
            return type.IsClass;
        }
        /// <summary>
        /// 返回当前类型是否是密封类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSealed(this Type type)
        {
            return type.IsSealed;
        }
        /// <summary>
        /// 判断类型是否是抽象的且必须被重写
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAbstract(this Type type)
        {
            return type.IsAbstract;
        }
        /// <summary>
        /// 是否可以被程序及外部的代码访问
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsVisible(this Type type)
        {
            return type.IsVisible;
        }
        /// <summary>
        /// 判断是否是值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(this Type type)
        {
            return type.IsValueType;
        }
        public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
        {
            Type type2 = type;
            while (type2 != null)
            {
                if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
                {
                    match = type2;
                    return true;
                }
                type2 = type2.BaseType();
            }
            Type[] interfaces = type.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                Type type3 = interfaces[i];
                if (string.Equals(type3.Name, fullTypeName, StringComparison.Ordinal))
                {
                    match = type;
                    return true;
                }
            }
            match = null;
            return false;
        }

        public static bool AssignableToTypeName(this Type type, string fullTypeName)
        {
            Type type2;
            return type.AssignableToTypeName(fullTypeName, out type2);
        }
        /// <summary>
        /// 按名称和参数查找方法对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] parameterTypes)
        {
            IEnumerable<MethodInfo> enumerable =
                from method in type.GetMethods()
                where method.Name == name
                select method;
            foreach (MethodInfo current in enumerable)
            {
                if (current.HasParameters(parameterTypes))
                {
                    return current;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取某个方法的参数
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static bool HasParameters(this MethodInfo method, params Type[] parameterTypes)
        {
            Type[] array = (
                from parameter in method.GetParameters()
                select parameter.ParameterType).ToArray<Type>();
            if (array.Length != parameterTypes.Length)
            {
                return false;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].ToString() != parameterTypes[i].ToString())
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取当前类实现或者继承的所有接口
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllInterfaces(this Type target)
        {
            try
            {
                Type[] interfaces = target.GetInterfaces();
                for (int i = 0; i < interfaces.Length; i++)
                {
                    Type type = interfaces[i];
                    yield return type;
                    try
                    {
                        Type[] interfaces2 = type.GetInterfaces();
                        for (int j = 0; j < interfaces2.Length; j++)
                        {
                            Type type2 = interfaces2[j];
                            yield return type2;
                        }
                    }
                    finally
                    {
                    }
                }
            }
            finally
            {
            }
            yield break;
        }
        /// <summary>
        /// 获取所有方法
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this Type target)
        {
            List<Type> list = target.GetAllInterfaces().ToList<Type>();
            list.Add(target);
            return
                from type in list
                from method in type.GetMethods()
                select method;
        }
        /// <summary>
        /// 判断给类型是否可以为空
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
