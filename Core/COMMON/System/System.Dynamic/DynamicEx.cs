using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Dynamic
{
    public class DynamicEx
    {
        public static bool HasKey(dynamic dyObj, string key)
        {
            var jObject = (Newtonsoft.Json.Linq.JObject)dyObj;
            return jObject.Properties().Where(x => x.Name == key).FirstOrDefault() != null;
        }

        public static bool IsNull(dynamic dyObj, string key)
        {
            var isNull = false;
            if(DynamicEx.HasKey(dyObj,key))
            {
                var jObject = (Newtonsoft.Json.Linq.JObject)dyObj;
                isNull  = jObject.Property(key).Value==null;
            }
            return isNull;
        }

        public static bool IsEmpty(dynamic dyObj, string key)
        {
            var isEmpty = false;
            if (((IDictionary<String, Object>)dyObj).ContainsKey(key))
            {
                isEmpty = ((IDictionary<String, Object>)dyObj)[key] == string.Empty;
            }
            return isEmpty;
        }
    }
}
