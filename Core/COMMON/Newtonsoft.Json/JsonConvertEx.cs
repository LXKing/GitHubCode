using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Newtonsoft.Json
{
    public static class JsonConvertEx
    {
        public static string SerializeObject(object obj)
        {
            try
            {
                var ass = Assembly.Load(new AssemblyName("Newtonsoft.Json"));
                var JsonConvert_Type = ass.GetType("Newtonsoft.Json.JsonConvert");
                var SerializeObject_Method = JsonConvert_Type.GetMethod("SerializeObject");
                var json = SerializeObject_Method.Invoke(null, new object[1] { obj }).ToString();
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeserializeObject<T>(string json)
        {
            var ass = Assembly.Load(new AssemblyName("Newtonsoft.Json"));
            var JsonConvert_Type = ass.GetType("Newtonsoft.Json.JsonConvert");
            var DeserializeObject_Method = JsonConvert_Type.GetMethod("DeserializeObject").MakeGenericMethod(typeof(T));
            var obj = (T)DeserializeObject_Method.Invoke(null, new object[1] { json });
            return obj;
        }

        public static string SerializeXmlNode(XmlNode node)
        {
            try
            {
                var ass = Assembly.Load(new AssemblyName("Newtonsoft.Json"));
                var JsonConvert_Type = ass.GetType("Newtonsoft.Json.JsonConvert");
                var SerializeXmlNode_Method = JsonConvert_Type.GetMethod("SerializeXmlNode");
                var json = SerializeXmlNode_Method.Invoke(null, new object[1] { node }).ToString();
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return JsonConvert.SerializeXmlNode(node, Formatting.None);
        }
    }


}
