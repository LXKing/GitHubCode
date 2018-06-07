using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Xml
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlNodeEx
    {
        /// <summary>
        /// XmlNode转json串
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string ToJsonString(this XmlNode node)
        {
            #region New
            //var json = Newtonsoft.Json.JsonConvertEx.SerializeXmlNode(node);
            //return json; 
            #endregion

            #region Old
            var json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(node);
            return json; 
            #endregion
        }
    }
}
