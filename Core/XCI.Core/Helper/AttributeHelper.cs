using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace XCI.Helper
{
    /// <summary>
    /// �Զ������Բ���������
    /// </summary>
    public class AttributeHelper
    {
        /// <summary>
        /// ��ȡ�Զ������Զ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objType">��������</param>
        public static T GetClassAttribute<T>(Type objType) where T : Attribute
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

        
        /// <summary>
        /// ��ȡ�Զ������Զ����б�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objType">��������</param>
        public static IList<T> GetClassAttributeList<T>(Type objType) where T : Attribute
        {
            object[] attributes = objType.GetCustomAttributes(typeof(T), false);

            IList<T> attributeList = new List<T>();
            foreach (object attribute in attributes)
            {                
                attributeList.Add((T)attribute);
            }
            return attributeList;
        }


        /// <summary>
        /// ��ȡָ�����Ե������ֵ�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objType">��������</param>
        public static IDictionary<string, KeyValuePair<T, PropertyInfo>> GetPropsWithAttributes<T>(Type objType) where T : Attribute
        {
            Dictionary<string, KeyValuePair<T, PropertyInfo>> map = new Dictionary<string, KeyValuePair<T,PropertyInfo>>();

            PropertyInfo[] props = objType.GetProperties();
            foreach(PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(T), true);
                if (attrs.Length > 0)
                {
                    map[prop.Name] = new KeyValuePair<T, PropertyInfo>(attrs[0] as T, prop);
                }
            }
            return map;
        }


        /// <summary>
        /// ��ȡָ�����Ե������б�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objType">��������</param>
        public static List<PropertyInfo> GetPropsOnlyWithAttributes<T>(Type objType) where T : Attribute
        {
            List<PropertyInfo> matchedProps = new List<PropertyInfo>();

            PropertyInfo[] props = objType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(T), true);
                if (attrs.Length > 0)
                {
                    matchedProps.Add(prop);
                }       
            }
            return matchedProps;
        }


        /// <summary>
        /// ��ȡָ�����Ե������б�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="objType">��������</param>
        public static List<KeyValuePair<T, PropertyInfo>> GetPropsWithAttributesList<T>(Type objType) where T : Attribute
        {
            List<KeyValuePair<T, PropertyInfo>> map = new List<KeyValuePair<T, PropertyInfo>>();

            IList<PropertyInfo> props = objType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(T), true);
                if (attrs.Length > 0)
                {
                    map.Add(new KeyValuePair<T, PropertyInfo>(attrs[0] as T, prop));
                }
            }
            return map;
        }



        /// <summary>
        /// ���س��� ��ȡ�������б� ִ�лص�����
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="assemblyPath">����·��</param>
        /// <param name="action">�ص�����</param>
        public static IList<KeyValuePair<Type, T>> GetClassAttributesFromAssembly<T>(string assemblyPath, Action<KeyValuePair<Type, T>> action)
        {
            Assembly assembly = Assembly.Load(assemblyPath);
            var types = assembly.GetTypes();
            var components = new List<KeyValuePair<Type, T>>();
            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(typeof(T), false);
                if (attributes.Length > 0)
                {
                    var pair = new KeyValuePair<Type, T>(type, (T)attributes[0]);
                    components.Add(pair);
                    if (action != null)
                    {
                        action(pair);
                    }
                }
            }
            return components;
        }


      

        /// <summary>
        /// ��ȡָ�����ͼ��ϵ�ȫ���������Լ��� ִ�лص�����
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="types">�����б�</param>
        /// <param name="action">�ص�����</param>
        public static IList<KeyValuePair<PropertyInfo, T>> GetPropertiesWithAttributesOnTypes<T>(IList<Type> types, Action<Type, KeyValuePair<PropertyInfo, T>> action) where T : Attribute
        {
            var propertyAttributes = new List<KeyValuePair<PropertyInfo, T>>();
            foreach (var type in types)
            {
                var properties = type.GetProperties();
                foreach (var prop in properties)
                {
                    var attributes = prop.GetCustomAttributes(typeof(T), true);
                    if (attributes.Length > 0)
                    {
                        var pair = new KeyValuePair<PropertyInfo, T>(prop, attributes[0] as T);
                        propertyAttributes.Add(pair);
                        action(type, pair);
                    }
                }
            }
            return propertyAttributes;
        }
    }
}
