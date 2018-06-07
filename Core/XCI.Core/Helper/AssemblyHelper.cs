using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace XCI.Helper
{
    /// <summary>
    /// 程序集操作帮助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 获取程序集中从指定类型继承的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="baseType">基类</param>
        public static IList<Type> GetTypeByBase(Assembly assembly, Type baseType)
        {
            IList<Type> typeList = new List<Type>();
            Type[] allType = assembly.GetTypes();
            foreach (Type typeItem in allType)
            {
                if (typeItem.BaseType != baseType)
                {
                    continue;
                }
                typeList.Add(typeItem);
            }
            return typeList;
        }


        /// <summary>
        /// 获取程序集中从指定类型继承的类型
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="baseType">基类型</param>
        public static IList<Type> GetTypeByBase(string assemblyPath, Type baseType)
        {
            Assembly assembly = Assembly.Load(assemblyPath);
            return GetTypeByBase(assembly, baseType);
        }


        /// <summary>
        /// 获取程序集资源内容
        /// </summary>
        /// <param name="name">资源名称（区分大小写）</param>
        public static string GetInternalFileContent(string name)
        {
            Assembly current = Assembly.GetExecutingAssembly();

            Stream stream = current.GetManifestResourceStream(name);
            if (stream == null)
            {
                return String.Empty;
            }
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }


        /// <summary>
        /// 循环程序集的每个类型 执行指定的动作
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="action">动作</param>
        public static void ForEach(string assemblyPath, Action<Type> action)
        {
            Assembly assembly = Assembly.Load(assemblyPath);
            ForEach(assembly, action);
        }


        /// <summary>
        /// 循环程序集的每个类型 执行指定的动作
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="action">动作</param>
        public static void ForEach(Assembly assembly, Action<Type> action)
        {
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    action(type);
                }
            }
            finally
            {
            }
        }


        /// <summary>
        /// 获取程序集中继承自指定接口的接口
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="interfaces">接口</param>
        public static IList<Type> GetInterfaceType(string assemblyPath, params Type[] interfaces)
        {
            Assembly assembly = Assembly.Load(assemblyPath);
            return GetInterfaceType(assembly, interfaces);
        }


        /// <summary>
        /// 获取程序集中继承自指定接口的接口
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="interfaces">接口</param>
        public static IList<Type> GetInterfaceType(Assembly assembly, params Type[] interfaces)
        {
            IList<Type> list = new List<Type>();
            Action<Type> action = p =>
            {
                if (ValidInterface(p, interfaces))
                {
                    list.Add(p);
                }
            };
            ForEach(assembly, action);
            return list;
        }


        /// <summary>
        /// 获取程序集中实现了指定接口的类
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="interfaces">接口</param>
        public static IList<Type> GetClassType(string assemblyPath, params Type[] interfaces)
        {
            Assembly assembly = Assembly.Load(assemblyPath);
            return GetClassType(assembly, interfaces);
        }


        /// <summary>
        /// 获取程序集中实现了指定接口的类
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="interfaces">接口</param>
        public static IList<Type> GetClassType(Assembly assembly, params Type[] interfaces)
        {
            IList<Type> list = new List<Type>();
            Action<Type> action = p =>
            {
                if (ValidInterfaceClass(p, false, interfaces))
                {
                    list.Add(p);
                }
            };
            ForEach(assembly, action);
            return list;
        }

        /// <summary>
        /// 验证接口是否继承自指定的接口(继承任意一个接口即可)
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="interfaces">接口</param>
        /// <returns>如果是 返回true</returns>
        public static bool ValidInterface(Type type, params Type[] interfaces)
        {
            if (!type.IsInterface)
            {
                return false;
            }

            return interfaces.Any(p => type.GetInterface(p.FullName) != null);
        }


        /// <summary>
        /// 验证类是否实现指定的接口(继承任意一个接口即可)
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="containAbstract">是否包含抽象类</param>
        /// <param name="interfaces">接口</param>
        /// <returns>如果是 返回true</returns>
        public static bool ValidInterfaceClass(Type type, bool containAbstract, params Type[] interfaces)
        {
            if (!type.IsClass || !type.IsPublic || type.ContainsGenericParameters
                || (!containAbstract && type.IsAbstract))
            {
                return false;
            }

            return interfaces.Any(p => type.GetInterface(p.FullName) != null);
        }


        /// <summary>
        /// 获取类型全名
        /// </summary>
        /// <param name="type">类型</param>
        public static string GetTypeFullName(Type type)
        {
            if (type.AssemblyQualifiedName != null)
            {
                string[] qualisz = type.AssemblyQualifiedName.Split(',');
                return String.Concat(qualisz[0], ",", qualisz[1]);
            }
            return String.Empty;
        }

        /// <summary>
        /// 获取程序集描述信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        public static string GetAssemblyInfoDescription(Assembly assembly)
        {
            bool isDefined = Attribute.IsDefined(assembly, typeof(AssemblyDescriptionAttribute));

            if (isDefined)
            {
                AssemblyDescriptionAttribute adAttr =
                    (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));

                if (adAttr != null)
                {
                    return adAttr.Description;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取程序集描述信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="defaultValue">默认值</param>
        public static string GetAssemblyInfoDescription(Type type, string defaultValue)
        {
            Assembly assembly = type.Assembly;
            string description = GetAssemblyInfoDescription(assembly);
            if (string.IsNullOrEmpty(description))
            {
                description = defaultValue;
            }

            return description;
        }

        /// <summary>
        /// 获取自定义属性对象
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="objType">目标对象类型</param>
        /// <returns>属性实例</returns>
        public static T GetCustomAttributes<T>(Type objType)
        {
            object[] attsz = objType.GetCustomAttributes(typeof(T), false);
            if (attsz.Length > 0)
            {
                T att = (T)attsz[0];
                if (att != null)
                {
                    return att;
                }
            }
            return default(T);
        }
    }
}
