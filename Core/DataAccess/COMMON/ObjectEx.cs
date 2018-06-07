using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

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
        public static T Apply_DA<T>(this T from, T target, bool ignoreDefaultValues) where T : class
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

        /// <summary>
        /// 对象在在转化为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AsJsonString_DA<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Text.Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        /// <summary>
        /// json串转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize_DA<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary>
        /// json字符串转List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static List<T> JsonStringToList_DA<T>(this string jsonString)
        {    
            JavaScriptSerializer Serializer = new JavaScriptSerializer();    
            List<T> objs = Serializer.Deserialize<List<T>>(jsonString);    
            return objs;    
        }
        /// <summary>
        /// 获取当前执行方法的类名(包含命名空间)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetClassName_DA(this object o, int n = 1)
        {
            try
            {
                StackTrace trace = new StackTrace();
                MethodBase method = trace.GetFrame(n).GetMethod();
                Type type = method.ReflectedType;
                string className = type.FullName;
                return className + ".cs";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取当前方法的名称(包含命名空间)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetMethodName_DA(this object o, int n = 1)
        {
            try
            {
                StackTrace trace = new StackTrace();
                MethodBase method = trace.GetFrame(n).GetMethod();
                Type type = method.ReflectedType;
                return string.Format("Function Name:{0}.{1}", type.FullName, method.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断当前对象是否为null
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNull_DA<T>(this T instance)
        {
            return (instance == null);
        }
        /// <summary>
        /// 判断不为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool NotNull_DA<T>(this T instance)
        {
            return (instance != null);
        }
    }
}
