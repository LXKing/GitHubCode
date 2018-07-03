using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
namespace System
{
    /// <summary>
    /// String扩展方法类
    /// </summary>
    public static class StringEx
    {
        #region 类型转换
        /// <summary>
        /// Json串转任意类型
        /// </summary>
        /// <typeparam name="T">要转换成的目标类型</typeparam>
        /// <param name="jsonString">要转换的Json字符串</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string jsonString)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("ObjectEx.ToJsonString方法异常", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 判断是否为正确的json串
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static bool IsJson(this string jsonString)
        {
            var result = false;
            try
            {
                var dy = JsonConvert.DeserializeObject<dynamic>(jsonString, new JsonSerializerSettings()
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Error
                    });
                result=true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 字符串转值类型方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToType(this string value, Type type)
        {
            try
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFrom(value);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("StringEx.ToType方法异常", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 字符串转值类型方法
        /// </summary>
        /// <typeparam name="T">值类型(int decimal double byte)</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToValueType<T>(this string value) where T : struct
        {
            try
            {
                return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("StringEx.ToValueType<T>方法异常", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 转换成为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="strEnum"></param>
        /// <returns></returns>
        public static TEnum ToEnumType<TEnum>(this string strEnum)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), strEnum);
        }
        #endregion

        #region 判断空字符串或null
        /// <summary>
        /// 判断字符串是否为null或者空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 字符串不为null且不为Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotNullAndEpmty(this string value)
        {
            return (value != null && value != string.Empty);
        }
        #endregion

        #region 格式化字符
        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="value">复合格式字符串。</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string value,params object[] args)
        {
            return string.Format(value, args);
        }


        /// <summary>
        /// 格式化Json字符串
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string FormatJsonString(this string jsonString)
        {
            #region New
            //格式化json字符串
            //Newtonsoft.Json.JsonSerializerEx serializer = new Newtonsoft.Json.JsonSerializerEx();
            //TextReader tr = new StringReader(jsonString);
            //Newtonsoft.Json.JsonTextReaderEx jtr = new Newtonsoft.Json.JsonTextReaderEx(tr);
            //object obj = serializer.Deserialize(jtr);
            //if (obj != null)
            //{
            //    StringWriter textWriter = new StringWriter();
            //    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            //    {
            //        Formatting = Newtonsoft.Json.Formatting.Indented,
            //        Indentation = 4,
            //        IndentChar = ' '
            //    };
            //    serializer.Serialize(jsonWriter, obj);
            //    return textWriter.ToString();
            //}
            //else
            //{
            //    return jsonString;
            //}
            #endregion
            #region Old
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(jsonString);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return jsonString;
            } 
            #endregion
        }
        #endregion

        #region 字符串简体繁体全角半角转换(不常用)

        /// <summary>
        /// 英文全角转换为半角字符串("wｗｘｙ"=>"wwxy")
        /// </summary>
        public static string ToDBC(this string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }
        /// <summary>
        /// 任意字符串半角转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 繁体中文转换为简体中文
        /// </summary>
        public static string ToChineseSimplified(this string value)
        {
            return Strings.StrConv(value, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 简体中文转换为繁体中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToChineseTraditional(this string value)
        {
            return Strings.StrConv(value, VbStrConv.TraditionalChinese, 0);
        }
        #endregion

        #region 正则匹配
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) 
                return false;
            else 
                return Regex.IsMatch(s, pattern);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) 
                return "";
            return 
                Regex.Match(s, pattern).Value;
        }
        #endregion

        #region 转xml对象
        /// <summary>
        /// xml字符串转XmlDocument
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(this string strXml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);
            return xmlDoc;
        }
        /// <summary>
        /// xml字符串转XmlNode
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static XmlNode ToXmlNode(this string strXml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);
            var firstNode = xmlDoc.LastChild;
            return firstNode;
        }
        #endregion

        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string GetCharSpellCode(this string CnChar)
        {
            long iCnChar;
            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);
            //如果是字母，则直接返回

            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {

                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {

                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else
                return ("?");
        }

        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(this string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;
            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }
            return strTemp;
        }
    }
}
