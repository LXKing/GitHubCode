using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using XCI.Core;

namespace XCI.Helper
{
    /// <summary>
    /// 对象帮助类
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 转换数据类型(包括 int long double string bool DateTime)
        /// </summary>
        /// <typeparam name="T">转换后类型</typeparam>
        /// <param name="input">需要转换的对象</param>
        public static T ConvertTo<T>(object input)
        {
            object result = default(T);
            if (input == null || input == DBNull.Value) return (T)result;

            if (typeof(T) == typeof(int))
                result = System.Convert.ToInt32(input);
            else if (typeof(T) == typeof(long))
                result = System.Convert.ToInt64(input);
            else if (typeof(T) == typeof(string))
                result = System.Convert.ToString(input);
            else if (typeof(T) == typeof(bool))
                result = System.Convert.ToBoolean(input);
            else if (typeof(T) == typeof(double))
                result = System.Convert.ToDouble(input);
            else if (typeof(T) == typeof(DateTime))
                result = System.Convert.ToDateTime(input);

            return (T)result;
        }
        public static object ConvertTypeDefaultValue(Type type)
        {
            if (type == typeof(int))
                return (object)0;
            if (type == typeof(Decimal))
                return (object)0;
            if (type == typeof(DateTime))
                return (object)DateTime.Now;
            if (type == typeof(bool))
                return (object)false;
            else
                return (object)string.Empty;
        }

        /// <summary>
        /// 创建类实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public static T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        /// <summary>
        /// 创建类实例
        /// </summary>
        /// <param name="provider">对象类型字符串</param>
        public static object CreateInstance(string provider)
        {
            return CreateInstance(Type.GetType(provider, false, true));
        }

        /// <summary>
        /// 创建类实例
        /// </summary>
        /// <param name="t">对象类型</param>
        public static object CreateInstance(Type t)
        {
            return t.LambdaCreate();
        }


        /// <summary>
        /// 创建对象并且缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key"></param>
        /// <param name="Implement">类名描述字符串</param>
        /// <returns>对象实例</returns>
        public static T CreateObjectByCache<T>(string key, string Implement)
        {
            object cacheObject = StaticCacheHelper.Get(key);
            Type cacheType = null;
            if (cacheObject == null)
            {
                cacheType = Type.GetType(Implement);
                StaticCacheHelper.Add(key, cacheType);
            }
            else
            {
                cacheType = cacheObject as Type;
            }
            if (cacheType != null)
            {
                return (T)cacheType.LambdaCreate();
            }
            return default(T);
        }


        /// <summary>
        /// 把对象转为字符串表示形式
        /// </summary>
        /// <param name="obj">对象</param>
        public static string GetObjectString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            var convert = TypeDescriptor.GetConverter(obj.GetType());
            return convert.ConvertToString(obj);
        }

        /// <summary>
        /// 检测此类型是否是可空类型
        /// </summary>
        /// <param name="type">测试的类型</param>
        /// <returns>如果是可空类型 返回true</returns>
        public static bool IsNullableType(Type type)
        {
            if (!type.IsValueType) return true;
            if (!type.UnderlyingSystemType.IsGenericType) return false;
            var def = type.UnderlyingSystemType.GetGenericTypeDefinition();
            if (def != null)
            {
                return def == typeof(Nullable<>);
            }
            return false;
        }

        /// <summary>
        /// 将指定文本转换为对象
        /// </summary>
        /// <param name="objString">对象字符串表示形式</param>
        /// <param name="type">对象类型</param>
        public static object GetObjectFromString(string objString, Type type)
        {
            var convert = TypeDescriptor.GetConverter(type);
            return convert.ConvertFromString(objString);
        }

        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <exception cref="System.ArgumentNullException">对象为空</exception>
        /// <exception cref="System.ArgumentException">指定的属性名称不正确</exception>
        /// <returns>属性值</returns>
        public static object GetValueByProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "对象为空");
            }
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty(propertyName);
            if (p == null)
            {
                throw new ArgumentException("指定的属性名称不正确", "propertyName");
            }
            return p.GetValue(obj, null);
        }


        /// <summary>
        /// 根据属性名称设置属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <exception cref="System.ArgumentNullException">对象为空</exception>
        /// <exception cref="System.ArgumentException">指定的属性名称不正确</exception>
        public static void SetValueByProperty(object obj, string propertyName, object propertyValue)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "对象为空");
            }
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty(propertyName);
            if (p == null)
            {
                throw new ArgumentException("指定的属性名称不正确", "propertyName");
            }
            p.SetValue(obj, propertyValue, null);
        }


        /// <summary>
        /// 返回具有指定 System.Type 而且其值等效于指定对象的 System.Object
        /// </summary>
        /// <param name="value">对象值</param>
        /// <param name="type">对象类型</param>
        /// <returns>返回具有指定 System.Type 而且其值等效于指定对象的 System.Object</returns>
        public static object ConvertObjectValue(object value, Type type)
        {
            if (value == DBNull.Value || null == value)
                return null;

            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                System.ComponentModel.NullableConverter nullableConverter
                    = new System.ComponentModel.NullableConverter(type);

                type = nullableConverter.UnderlyingType;
            }
            if (type.IsEnum)
            {
                return Convert.ChangeType(Enum.Parse(type, value.ToString()), type);
            }

            return Convert.ChangeType(value, type);
        }


        #region 属性操作

        /// <summary>
        /// 设置对象属性
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyDic">属性键值</param>
        public static void SetObjectPropertys(object instance, Dictionary<string, object> propertyDic)
        {
            foreach (KeyValuePair<string, object> propertyKey in propertyDic)
            {
                SetObjectProperty(instance, propertyKey.Key, propertyKey.Value);
            }
        }

        /// <summary>
        /// 设置对象属性
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyStrings">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        public static void SetObjectPropertys(object instance, string propertyStrings, object propertyValue)
        {
            if (propertyStrings.Length <= 0)
            {
                return;
            }
            string[] propertys = propertyStrings.Split(';');
            foreach (string property in propertys)
            {
                SetObjectProperty(instance, property, propertyValue);
            }
        }

        /// <summary>
        /// 检测属性对象是否是基本类型
        /// </summary>
        /// <param name="instanceInfo">属性对象</param>
        /// <returns>如果是基本类型返回True</returns>
        public static bool CheckBasicProperty(PropertyInfo instanceInfo)
        {
            return (instanceInfo.PropertyType == typeof(int)
                 || instanceInfo.PropertyType == typeof(long)
                 || instanceInfo.PropertyType == typeof(double)
                 || instanceInfo.PropertyType == typeof(float)
                 || instanceInfo.PropertyType == typeof(string)
                 || instanceInfo.PropertyType == typeof(bool)
                 || instanceInfo.PropertyType == typeof(DateTime)
                 || instanceInfo.PropertyType.IsEnum)
                &&
                (instanceInfo.CanRead
                 && instanceInfo.CanWrite
                 && instanceInfo.PropertyType.IsPublic);
        }

        /// <summary>
        /// 设置对象基本属性
        /// </summary>
        /// <param name="instance">对象</param>
        /// <param name="propertyList">基本属性列表</param>
        public static void SetObjectBasicProperty(object instance, XCIList<XCIKeyValuePair<string, string>> propertyList)
        {
            Type instanceType = instance.GetType();
            foreach (var item in propertyList)
            {
                string key = item.Key;
                string value = item.Value;
                var instanceInfo = instanceType.GetProperty(key);
                if (CheckBasicProperty(instanceInfo))
                {
                    instanceInfo.SetValue(instance,
                        ObjectHelper.ConvertObjectValue(value,
                        instanceInfo.PropertyType), null);
                }
            }
        }

        public static XCIList<string> GetObjectBasicPropertyList(object instance)
        {
            XCIList<string> propertyList = new XCIList<string>();
            PropertyInfo[] configPropertys = instance.GetType().GetProperties();
            foreach (PropertyInfo instanceInfo in configPropertys)
            {
                if (CheckBasicProperty(instanceInfo))
                {
                    propertyList.Add(instanceInfo.Name);
                }
            }
            return propertyList;
        }

        /// <summary>
        /// 获取对象基本属性
        /// </summary>
        /// <param name="instance">对象</param>
        /// <returns>返回基本属性列表</returns>
        public static XCIList<XCIKeyValuePair<string, string>> GetObjectBasicProperty(object instance)
        {
            XCIList<XCIKeyValuePair<string, string>> propertyList = new XCIList<XCIKeyValuePair<string, string>>();
            PropertyInfo[] configPropertys = instance.GetType().GetProperties();
            foreach (PropertyInfo instanceInfo in configPropertys)
            {
                if (CheckBasicProperty(instanceInfo))
                {
                    object proValue = instanceInfo.GetValue(instance, null);
                    if (proValue != null)
                    {
                        propertyList.Add(
                            new XCIKeyValuePair<string, string>(instanceInfo.Name, proValue.ToString()));
                    }
                }
            }
            return propertyList;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyString">属性字符串</param>
        /// <param name="propertyValue">属性值</param>
        public static void SetObjectProperty(object instance, string propertyString, object propertyValue)
        {
            if (propertyString.Length <= 0)
            {
                return;
            }

            string[] propertys = propertyString.Split('.');
            object tempObject = instance;
            for (int i = 0; i < propertys.Length; i++)
            {
                string propName = propertys[i];
                Type objectType = tempObject.GetType();
                FastPropertyInfo fastProperty = null;
                if (i == propertys.Length - 1)
                {
                    fastProperty = GetFastPropertyInfo(objectType, propName);
                    if (null != fastProperty && fastProperty.Property.CanWrite)
                    {
                        fastProperty.Set(tempObject, ObjectHelper.GetObjectFromString(propertyValue.ToString(), fastProperty.Property.PropertyType));
                    }
                }
                else
                {
                    fastProperty = GetFastPropertyInfo(objectType, propName);
                    if (null != fastProperty)
                    {
                        tempObject = fastProperty.Get(tempObject);
                    }
                }
            }
        }

        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyStrings">属性字符串</param>
        public static object GetObjectPropertys(object instance, string propertyStrings)
        {
            if (propertyStrings.Length <= 0)
            {
                return null;
            }
            string[] propertys = propertyStrings.Split(';');
            foreach (string property in propertys)
            {
                return GetObjectProperty(instance, property);
            }
            return null;
        }

        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyString">属性字符串</param>
        public static object GetObjectProperty(object instance, string propertyString)
        {
            if (propertyString.Length <= 0)
            {
                return null;
            }
            string[] propertys = propertyString.Split('.');
            object tempObject = instance;
            for (int i = 0; i < propertys.Length; i++)
            {
                string propName = propertys[i];
                Type objectType = tempObject.GetType();
                FastPropertyInfo fastProperty = null;
                if (i == propertys.Length - 1)
                {
                    fastProperty = GetFastPropertyInfo(objectType, propName);
                    if (null != fastProperty && fastProperty.Property.CanRead)
                    {
                        return fastProperty.Get(tempObject);
                    }
                }
                else
                {
                    fastProperty = GetFastPropertyInfo(objectType, propName);
                    if (null != fastProperty)
                    {
                        tempObject = fastProperty.Get(tempObject);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 表格属性缓存
        /// </summary>
        private static readonly Dictionary<string, FastPropertyInfo> ObjectPropertyCache = new Dictionary<string, FastPropertyInfo>();

        /// <summary>
        /// 获取对象快速属性
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <param name="propName">属性名称</param>
        public static FastPropertyInfo GetFastPropertyInfo(Type objectType, string propName)
        {
            FastPropertyInfo fastProperty = null;
            var key = string.Concat(propName, objectType.FullName);
            if (ObjectPropertyCache.ContainsKey(key))
            {
                fastProperty = ObjectPropertyCache[key];
            }
            else
            {
                PropertyInfo property = null;
                try
                {
                    property = objectType.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.DeclaredOnly);
                }
                catch (AmbiguousMatchException)
                {
                }

                if (property == null)
                {
                    try
                    {
                        property = objectType.GetProperty(propName);
                    }
                    catch (AmbiguousMatchException)
                    {
                    }
                }
                if (property == null)
                {
                    return null;
                }

                fastProperty = new FastPropertyInfo(property);
                if (!ObjectPropertyCache.ContainsKey(key))
                {
                    ObjectPropertyCache.Add(key, fastProperty);
                }
            }
            return fastProperty;
        }



        #endregion


    }
}
