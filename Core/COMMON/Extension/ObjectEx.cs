using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;

namespace System
{
    /// <summary>
    /// 对object进行扩展(很基础的)
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 任意对象转json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch(Exception ex)
            {
                COMMON.Logs.Log.WriteException("ObjectEx.ToJsonString方法异常",ex);
                throw ex;
            }
        }
        /// <summary>
        /// 获取当前执行方法的类名(包含命名空间)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetClassName(this object o, int n)
        {
            try
            {
                StackTrace trace = new StackTrace();
                MethodBase method = trace.GetFrame(n).GetMethod();
                Type type = method.ReflectedType;
                string className = type.FullName;
                return className;
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
        public static string GetMethodName(this object o, int n)
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
        /// 将一个对象转换为匿名类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        public static T ToAnonymousCast<T>(this object o, T example) where T : class
        {
            IComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
            //Get constructor with lowest number of parameters and its parameters
            var constructor = typeof(T).GetConstructors(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                ).OrderBy(c => c.GetParameters().Length).First();
            var parameters = constructor.GetParameters();

            //Get properties of input object
            var sourceProperties = new List<PropertyInfo>(o.GetType().GetProperties());

            if (parameters.Length > 0)
            {
                var values = new object[parameters.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    Type t = parameters[i].ParameterType;
                    //See if the current parameter is found as a property in the input object
                    var source = sourceProperties.Find(delegate(PropertyInfo item)
                    {
                        return comparer.Compare(item.Name, parameters[i].Name) == 0;
                    });

                    //See if the property is found, is readable, and is not indexed
                    if (source != null && source.CanRead &&
                        source.GetIndexParameters().Length == 0)
                    {
                        //See if the types match.
                        if (source.PropertyType == t)
                        {
                            //Get the value from the property in the input object and save it for use
                            //in the constructor call.
                            values[i] = source.GetValue(o, null);
                            continue;
                        }
                        else
                        {
                            //See if the property value from the input object can be converted to
                            //the parameter type
                            try
                            {
                                values[i] = Convert.ChangeType(source.GetValue(o, null), t);
                                continue;
                            }
                            catch
                            {
                                //Impossible. Forget it then.
                            }
                        }
                    }
                    //If something went wrong (i.e. property not found, or property isn't
                    //converted/copied), get a default value.
                    values[i] = t.IsValueType ? Activator.CreateInstance(t) : null;
                }
                //Call the constructor with the collected values and return it.
                return (T)constructor.Invoke(values);
            }
            //Call the constructor without parameters and return the it.
            return (T)constructor.Invoke(null);
        }
        /// <summary>
        /// 克隆一个对象(借助于Newtonsoft.Json.dll,属于深克隆)
        /// </summary>
        /// <typeparam name="T">克隆的对象的类型</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneByNewtonsoft<T>(this T obj)where T:class
        {
            try
            {
                var objectString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var clonedObj = (T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(objectString);
                return clonedObj;
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("异常", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 克隆(适合单个对象)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static T Apply<T>(this T from,T target)where T:class
        {
            return Apply<T>(from, target, true);
        }
        /// <summary>
        /// 克隆(适合单个对象)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <param name="ignoreDefaultValues"></param>
        /// <returns></returns>
        public static T Apply<T>(this T from,T target, bool ignoreDefaultValues)where T:class
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
        /// 如果test成功,返回valueIfTrue,否则返回valueIfFalse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="test"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        public static T If<T>(this T value, Func<bool> test, T valueIfTrue, T valueIfFalse)
        {
            if (!test())
            {
                return valueIfFalse;
            }
            return valueIfTrue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="test"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        public static T IfNot<T>(this T value, Func<bool> test, T valueIfTrue, T valueIfFalse)
        {
            if (test())
            {
                return valueIfFalse;
            }
            return valueIfTrue;
        }
        /// <summary>
        /// 如果为null设置一个默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="valueIfNull">为null时的默认值</param>
        /// <returns></returns>
        public static T IfNull<T>(this T value, T valueIfNull)
        {
            return value.If<T>(() => value.IsNull<T>(), valueIfNull, value);
        }
        /// <summary>
        /// 判断当前对象是否为null
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T instance)
        {
            return (instance == null);
        }

    }
}
