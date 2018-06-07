using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;
using System.Web.Configuration;
using System.Web.Security;
using XCI.Extension;
using System.IO;

namespace XCI.Helper
{
    /// <summary>
    /// 字符串操作帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 去除HTML格式 多于最大长度 则会截断
        /// </summary>
        /// <param name="htmlText">html字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <returns>替换后的纯文本</returns>
        public static string ReplaceHtml(string htmlText, int maxLength)
        {
            if (htmlText.IsEmpty())
            {
                throw new ArgumentNullException("htmlText", "字符串不能为空");
            }
            htmlText = htmlText.Trim();
            if (maxLength > 0 && htmlText.Length > maxLength)
            {
                htmlText = htmlText.Substring(0, maxLength);
            }
            htmlText = Regex.Replace(htmlText, "[\\s]{2,}", " ");	//替换连个以上空格
            htmlText = Regex.Replace(htmlText, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//替换<br>
            htmlText = Regex.Replace(htmlText, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//替换&nbsp;
            htmlText = Regex.Replace(htmlText, "<(.|\\n)*?>", string.Empty);	//替换其他html标记
            htmlText = htmlText.Replace("'", "''");
            return htmlText;
        }



        /// <summary>
        /// 去除HTML格式
        /// </summary>
        /// <param name="htmlText">html字符串</param>
        /// <returns>去除Html格式后的纯文本</returns>
        public static string ReplaceHtml(string htmlText)
        {
            string stroutput = string.Empty;
            if (htmlText != null)
            {
                stroutput = htmlText.Trim();
                Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
                stroutput = regex.Replace(stroutput, "");
                stroutput = stroutput.Replace("\n", "");
                stroutput = stroutput.Replace("\r", "");
                stroutput = stroutput.Replace("&nbsp;", "");
                stroutput = stroutput.Replace("<br>", "").Replace("<BR>", "");
            }
            return stroutput;
        }


        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="htmlText">需要进行替换的文本。</param>
        public static string ReplaceHtmlEncode(string htmlText)
        {
            htmlText = htmlText.Replace(">", "&gt;");
            htmlText = htmlText.Replace("<", "&lt;");
            htmlText = htmlText.Replace("  ", " &nbsp;");
            htmlText = htmlText.Replace("  ", " &nbsp;");
            htmlText = htmlText.Replace("\"", "&quot;");
            htmlText = htmlText.Replace("\'", "&#39;");
            htmlText = htmlText.Replace("\n", "<br/> ");
            return htmlText;
        }


        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="text">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string ReplaceHtmlDiscode(string text)
        {
            text = text.Replace("&gt;", ">");
            text = text.Replace("&lt;", "<");
            text = text.Replace("&nbsp;", " ");
            text = text.Replace(" &nbsp;", "  ");
            text = text.Replace("&quot;", "\"");
            text = text.Replace("&#39;", "\'");
            text = text.Replace("<br/> ", "\n");
            return text;
        }


        /// <summary>
        /// 替换中文标点符号
        /// </summary>
        /// <param name="text">要替换的字符串</param>
        /// <returns>替换后结果</returns>
        public static string ReplaceChineseSymbol(string text)
        {
            string str = text.Replace("”", "").Replace("“", "").Replace("？", "").Replace("。", "")
                .Replace("，", "").Replace("《", "").Replace("【", "").Replace("】", "").Replace("》", "")
                .Replace("（", "").Replace("）", "").Replace("、", "").Trim();
            return str;
        }


        /// <summary>
        /// 替换字符 例如 "123456654321" 旧字符 "35" 新字符 "AB" 结果 "12A4B66B4A21"
        /// </summary>
        /// <param name="txt">源字符串</param>
        /// <param name="originalChars">旧字符</param>
        /// <param name="newChars">新字符</param>
        public static string ReplaceChars(string txt, string originalChars, string newChars)
        {
            string returned = "";

            for (int i = 0; i < txt.Length; i++)
            {
                int pos = originalChars.IndexOf(txt.Substring(i, 1), System.StringComparison.Ordinal);

                if (-1 != pos)
                    returned += newChars.Substring(pos, 1);
                else
                    returned += txt.Substring(i, 1);
            }
            return returned;
        }


        /// <summary>
        /// 根据当前文件名称生成一个Guid文件名
        /// </summary>
        /// <param name="fileName">当前文件名称</param>
        public static string BuildGuidFileName(string fileName)
        {
            return GetGuid() + Path.GetExtension(fileName);
        }

        /// <summary>
        /// 获取一个Guid
        /// </summary>
        /// <param name="Format">显示格式</param>
        /// <remarks>
        ///    //1、Guid.NewGuid().ToString("N") 结果为：
        ///    //      38bddf48f43c48588e0d78761eaa1ce6
        ///
        ///    //2、Guid.NewGuid().ToString("D") 结果为： 
        ///    //      57d99d89-caab-482a-a0e9-a0a803eed3ba
        ///
        ///    //3、Guid.NewGuid().ToString("B") 结果为：
        ///    //     {09f140d5-af72-44ba-a763-c861304b46f8}
        ///
        ///    //4、Guid.NewGuid().ToString("P") 结果为：
        ///    //     (778406c2-efff-4262-ab03-70a77d09c2b5)
        /// </remarks>
        public static Guid GetGuid(string Format = "N")
        {
            return Guid.Parse(Guid.NewGuid().ToString(Format));
        }
        /// <summary>
        /// 获取一个Guid字符串
        /// </summary>
        /// <param name="Format">显示格式</param>
        public static string GetGuidString(string Format = "N")
        {
            return Guid.NewGuid().ToString(Format);
        }
        /// <summary>
        /// 获取一个不重复日期格式的名称(2008-11-11-20-20-20)
        /// </summary>
        /// <param name="hasLine">是否含有线</param>
        /// <returns>返回的名称格式(2008-11-11-20-20-20)</returns>
        public static string GetDateName(bool hasLine = true)
        {
            Thread.Sleep(1000);
            string str1 = DateTime.Now.Year + "-";
            str1 += (DateTime.Now.Month).ToString().Length < 2 ? "0" + DateTime.Now.Month + "-" : DateTime.Now.Month + "-";
            str1 += DateTime.Now.Day.ToString().Length < 2 ? "0" + DateTime.Now.Day + "-" : DateTime.Now.Day + "-";
            str1 += DateTime.Now.Hour.ToString().Length < 2 ? "0" + DateTime.Now.Hour + "-" : DateTime.Now.Hour + "-";
            str1 += DateTime.Now.Minute.ToString().Length < 2 ? "0" + DateTime.Now.Minute + "-" : DateTime.Now.Minute + "-";
            str1 += DateTime.Now.Second.ToString().Length < 2 ? "0" + DateTime.Now.Second : DateTime.Now.Second.ToString();
            if (!hasLine)
            {
                str1 = str1.Replace("-", "");
            }
            return str1;
        }

        /// <summary>
        /// 获取一个MD5哈希码
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns>Md5加密码</returns>
        public static string GetMD5(string text)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(text, FormsAuthPasswordFormat.MD5.ToString());
        }


        #region 截取字符串

        /// <summary>
        /// 截取字符串,不限制字符串长度 \n
        /// </summary>
        /// <param name="text">所要截取的字符串</param>
        /// <param name="maxLen">每行的长度，多于这个长度自动换行</param>
        /// <param name="newLineSymbol">换行符号</param>
        /// <returns>截取之后的字符串</returns>
        public static string SubStringA(string text, int maxLen, string newLineSymbol)
        {
            if (text.Length < maxLen)
                return text;
            StringBuilder sb = new StringBuilder();
            int textLength = text.Length;
            int count = textLength / maxLen;
            for (int i = 0; i < count; i++)
            {
                sb.Append(text.Substring(i * maxLen, maxLen));
                sb.Append(newLineSymbol);
            }
            int ys = textLength % maxLen;
            if (ys > 0)
            {
                sb.Append(text.Substring(textLength - ys, ys));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 截断字符串 多于最大长度 则补指定的字符
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <param name="maxLen">最大长度</param>
        /// <param name="suffix">补指定的字符</param>
        public static string SubStringB(string text, int maxLen, string suffix)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (text.Length <= maxLen)
                return text;

            string partial = text.Substring(0, maxLen);
            return partial + suffix;
        }

        
        /// <summary>
        /// 从最后位置开始移除字符串
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="removeCount">移除个数</param>
        public static string TruncateEnd(string sourceString, int removeCount)
        {
            string result = sourceString;
            if ((removeCount > 0) && (sourceString.Length > removeCount - 1))
            {
                result = result.Remove(sourceString.Length - removeCount, removeCount);
            }
            return result;
        }


        /// <summary>
        /// 从最后位置开始移除字符串
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="backDownTo">计算字符串的位置以确定移除个数</param>
        public static string TruncateEnd(string sourceString, string backDownTo)
        {
            int removeDownTo = sourceString.LastIndexOf(backDownTo, StringComparison.Ordinal);
            int removeFromEnd = 0;
            if (removeDownTo > 0)
            {
                removeFromEnd = sourceString.Length - removeDownTo;
            }

            string result = sourceString;

            if (sourceString.Length > removeFromEnd - 1)
            {
                result = result.Remove(removeDownTo, removeFromEnd);
            }

            return result;
        }
        
        /// <summary>
        /// 从开始位置开始移除字符串
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="removeCount">移除个数</param>
        public static string TruncateStart(string sourceString, int removeCount)
        {
            string result = sourceString;
            if (sourceString.Length > removeCount)
            {
                result = result.Remove(0, removeCount);
            }
            return result;
        }


        /// <summary>
        /// 从开始位置开始移除字符串
        /// </summary>
        /// <param name="sourceString">源字符串</param>
        /// <param name="removeUpTo">计算字符串的位置以确定移除个数</param>
        public static string TruncateStart(string sourceString, string removeUpTo)
        {
            int removeFromBeginning = sourceString.IndexOf(removeUpTo, System.StringComparison.Ordinal);
            string result = sourceString;

            if (sourceString.Length > removeFromBeginning && removeFromBeginning > 0)
            {
                result = result.Remove(0, removeFromBeginning);
            }

            return result;
        }
        
        #endregion



        /// <summary>
        /// 获取Like查询值 格式为%xxx%
        /// </summary>
        /// <param name="text">查询值</param>
        public static string GetLikeAll(string text)
        {
            return string.Concat("%", text, "%");
        }


        /// <summary>
        /// 获取Like查询值 格式为%xxx
        /// </summary>
        /// <param name="text">查询值</param>
        public static string GetLikeLeft(string text)
        {
            return string.Concat("%", text);
        }


        /// <summary>
        /// 获取Like查询值 格式为xxx%
        /// </summary>
        /// <param name="text">查询值</param>
        public static string GetLikeRight(string text)
        {
            return string.Concat(text, "%");
        }


        /// <summary>
        /// 获取字串中的所有超链接
        /// </summary>
        /// <param name="htmlText">html文本</param>
        public static IList<string> GetPageAllUrl(string htmlText)
        {
            List<string> list = new List<string>();
            const string p = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex re = new Regex(p, RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(htmlText);

            for (int i = 0; i <= mc.Count - 1; i++)
            {
                string name = mc[i].ToString();
                if (!list.Contains(name))//排除重复URL
                {
                    list.Add(name);
                }
            }
            return list;
        }


        /// <summary>
        /// 获取占用空间大小的中文说明比如(30.1 GB  20.2 MB)
        /// </summary>
        /// <param name="size">字节大小</param>
        public static string GetSpaceSizeString(long size)
        {
            #region
            decimal NUM;
            string strResult;

            if (size > 1073741824)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1073741824));
                strResult = NUM.ToString("N") + " GB";
            }
            else if (size > 1048576)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1048576));
                strResult = NUM.ToString("N") + " MB";
            }
            else if (size > 1024)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1024));
                strResult = NUM.ToString("N") + " KB";
            }
            else
            {
                strResult = size + " B";
            }

            return strResult;
            #endregion
        }

        
        /// <summary>
        /// 生成一个完成度百分比字符串
        /// </summary>
        /// <param name="current">当前进度</param>
        /// <param name="sum">总进度</param>
        /// <param name="format">显示格式化 例如 完成度{0}</param>
        public static string BuildCompleteString(int current, int sum,string format)
        {
            int progress = Convert.ToInt32(Convert.ToDecimal(current) / Convert.ToDecimal(sum) * 100);
            return string.Format(format, progress);
        }



        /// <summary>
        /// 获取操作系统的版本信息
        /// </summary>
        public static string GetOperatingSystemVersion()
        {
            string Agent = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            if (Agent.IndexOf("NT 4.0", StringComparison.Ordinal) > 0)
            {
                return "Windows NT ";
            }
            if (Agent.IndexOf("NT 5.0", StringComparison.Ordinal) > 0)
            {
                return "Windows 2000";
            }
            if (Agent.IndexOf("NT 5.1", StringComparison.Ordinal) > 0)
            {
                return "Windows XP";
            }
            if (Agent.IndexOf("NT 5.2", StringComparison.Ordinal) > 0)
            {
                return "Windows 2003";
            }
            if (Agent.IndexOf("NT 6.0", StringComparison.Ordinal) > 0)
            {
                return "Windows Vista";
            }
            if (Agent.IndexOf("NT 6.1", StringComparison.Ordinal) > 0)
            {
                return "Windows 7";
            }
            if (Agent.IndexOf("WindowsCE", StringComparison.Ordinal) > 0)
            {
                return "Windows CE";
            }
            if (Agent.IndexOf("NT", StringComparison.Ordinal) > 0)
            {
                return "Windows NT ";
            }
            if (Agent.IndexOf("9x", StringComparison.Ordinal) > 0)
            {
                return "Windows ME";
            }
            if (Agent.IndexOf("98", StringComparison.Ordinal) > 0)
            {
                return "Windows 98";
            }
            if (Agent.IndexOf("95", StringComparison.Ordinal) > 0)
            {
                return "Windows 95";
            }
            if (Agent.IndexOf("Win32", StringComparison.Ordinal) > 0)
            {
                return "Win32";
            }
            if (Agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
            {
                return "Linux";
            }
            if (Agent.IndexOf("SunOS", StringComparison.Ordinal) > 0)
            {
                return "SunOS";
            }
            if (Agent.IndexOf("Mac", StringComparison.Ordinal) > 0)
            {
                return "Mac";
            }
            if (Agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
            {
                return "Linux";
            }
            if (Agent.IndexOf("Windows", StringComparison.Ordinal) > 0)
            {
                return "Windows";
            }

            return "未知系统";

        }


        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="times">重复次数</param>
        public static string GetRepeatString(string str, int times)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (times <= 1)
            {
                return str;
            }

            string strfinal = string.Empty;
            for (int i = 0; i < times; i++)
            {
                strfinal += str;
            }

            return strfinal;
        }
        

        /// <summary>
        /// 生成指定长度的字符串 长度不够用指定的字符补上
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="length"></param>
        /// <param name="paddingChar">补充字符串</param>
        /// <param name="isLeft">是否补在左边</param>
        public static string GetFixedLengthString(string text, int length, string paddingChar,bool isLeft = true)
        {
            int leftOver = length - text.Length;
            string finalText = text;
            for (int ndx = 0; ndx < leftOver; ndx++)
            {
                if (isLeft)
                {
                    finalText = paddingChar + finalText;
                }
                else
                {
                    finalText = finalText + paddingChar;
                }
            }
            return finalText;
        }

        
        /// <summary>
        /// 读取全部字符串 一行一条记录
        /// </summary>
        /// <param name="text">字符串</param>
        public static IList<string> ReadLines(string text)
        {
            // Check for empty and return empty list.
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            StringReader reader = new StringReader(text);
            string currentLine = reader.ReadLine();
            IList<string> lines = new List<string>();

            // More to read.
            while (currentLine != null)
            {
                lines.Add(currentLine);
                currentLine = reader.ReadLine();
            }
            return lines;
        }



        /// <summary>
        /// 增长字符串到指定的长度
        /// 如果字符串大于指定的长度 使用是否截断选项
        /// 如果字符串小于指定的长度 会重复源字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="truncate">是否截断</param>
        public static string IncreaseTo(string str, int maxLength, bool truncate)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length == maxLength) return str;
            if (str.Length > maxLength && truncate)
            {
                return SubStringB(str, maxLength,string.Empty);
            }

            string original = str;

            while (str.Length < maxLength)
            {
                if (str.Length + original.Length < maxLength)
                {
                    str += original;
                }
                else
                {
                    str += str.Substring(0, maxLength - str.Length);
                }
            }
            return str;
        }



        /// <summary>
        /// 增长字符串到随机长度(介于最大值最小值直接的随机数)
        /// 如果字符串大于指定的长度 使用是否截断选项
        /// 如果字符串小于指定的长度 会重复源字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="truncate">是否截断</param>
        public static string IncreaseRandomly(string str, int minLength, int maxLength, bool truncate)
        {
            Random random = new Random(minLength);
            int randomMaxLength = random.Next(minLength, maxLength);
            return IncreaseTo(str, randomMaxLength, truncate);
        }


        /// <summary>
        /// 转换字符串首字母为大写
        /// UPPER = Upper
        /// lower = Lower
        /// MiXEd = Mixed
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="delimiter">检测分隔符</param>
        public static string ConvertToSentanceCase(string str, char delimiter)
        {
            // Check null/empty
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.Trim();
            if (string.IsNullOrEmpty(str))
                return str;

            // Only 1 token
            if (str.IndexOf(delimiter) < 0)
            {
                str = str.ToLower();
                str = str[0].ToString().ToUpper() + str.Substring(1);
                return str;
            }

            // More than 1 token.
            string[] tokens = str.Split(delimiter);
            StringBuilder buffer = new StringBuilder();

            foreach (string token in tokens)
            {
                string currentToken = token.ToLower();
                currentToken = currentToken[0].ToString().ToUpper() + currentToken.Substring(1);
                buffer.Append(currentToken + delimiter);
            }

            str = buffer.ToString();
            return str.TrimEnd(delimiter);
        }
        
        

        /// <summary>
        /// 分割值名对到字典中 例如 "city=Queens, state=Ny, zipcode=12345, country=usa"
        /// </summary>
        /// <param name="delimitedText">值名对字符串</param>
        /// <param name="keyValuePairDelimiter">值名对分隔符 ","</param>
        /// <param name="keyValueDelimeter">值名对连接符 "="</param>
        /// <param name="makeKeysCaseSensitive">是否Key值大写</param>
        /// <param name="trimValues">是否移除值的空格</param>
        public static IDictionary<string, string> ToMap(string delimitedText, char keyValuePairDelimiter, char keyValueDelimeter,
            bool makeKeysCaseSensitive, bool trimValues)
        {
            IDictionary<string, string> map = new Dictionary<string, string>();
            string[] tokens = delimitedText.Split(keyValuePairDelimiter);

            // Each pair
            foreach (string token in tokens)
            {
                // Split city=Queens to "city", "queens"
                string[] pair = token.Split(keyValueDelimeter);

                string key = pair[0];
                string value = pair[1];

                if (makeKeysCaseSensitive)
                {
                    key = key.ToLower();
                }
                if (trimValues)
                {
                    key = key.Trim();
                    value = value.Trim();
                }
                map[key] = value;
            }
            return map;
        }

        
        
        /// <summary>
        /// 循环执行(从0到count)
        /// </summary>
        /// <param name="count">总数</param>
        /// <param name="action">动作</param>
        public static void Times(int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
            {
                action(i);
            }
        }


        /// <summary>
        /// 循环执行(从小到大)
        /// </summary>
        /// <param name="start">开始数</param>
        /// <param name="end">结束数</param>
        /// <param name="action">动作</param>
        public static void Upto(int start, int end, Action<int> action)
        {
            for (int i = start; i <= end; i++)
            {
                action(i);
            }
        }


        /// <summary>
        /// 循环执行(从大到小)
        /// </summary>
        /// <param name="end">结束数</param>
        /// <param name="start">开始数</param>
        /// <param name="action">动作</param>
        public static void Downto(int end, int start, Action<int> action)
        {
            for (int i = end; i >= start; i--)
            {
                action(i);
            }
        }

        

        /// <summary>
        /// 数组转为字符串 用指定符号连接
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="items">集合</param>
        /// <param name="delimeter">分割符号</param>
        public static string ArrayToString<T>(IEnumerable<T> items, string delimeter=",")
        {
            List<string> itemList = new List<string>();
            foreach (T item in items)
            {
                itemList.Add(item.ToString());
            }
            return String.Join(delimeter, itemList.ToArray());
        }


        
        /// <summary>
        /// 字符串转为数组 用指定符号分割
        /// </summary>
        /// <param name="delimitedText">字符串</param>
        /// <param name="delimeter">分割符号</param>
        public static string[] StringToArray(string delimitedText, string delimeter = ",")
        {
            if (string.IsNullOrEmpty(delimitedText))
                return null;

            string[] tokens = delimitedText.Split(new[] { delimeter }, StringSplitOptions.RemoveEmptyEntries);
            return tokens;
        }

        /// <summary>
        ///  字符串转为整数数组 用指定符号分割
        /// </summary>
        /// <param name="delimitedText">字符串</param>
        /// <param name="delimeter">分割符号</param>
        public static int[] StringToIntArray(string delimitedText, string delimeter = ",")
        {
            var sz = StringToArray(delimitedText, delimeter);
            int[] newsz = new int[sz.Length];
            for (int index = 0; index < sz.Length; index++)
            {
                newsz[index] = sz[index].ToInt();
            }
            return newsz;
        }

        /// <summary>
        ///  字符串转为整数数组 用指定符号分割
        /// </summary>
        /// <param name="delimitedText">字符串</param>
        /// <param name="delimeter">分割符号</param>
        public static ArrayList StringToIntArrayList(string delimitedText, string delimeter = ",")
        {
            var sz = StringToArray(delimitedText, delimeter);
            ArrayList list = new ArrayList();
            foreach (string t in sz)
            {
                list.Add(t.ToInt());
            }
            return list;
        }

        #region 字符转换

        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="b">字节</param>
        public static string ToHex(byte b)
        {
            return b.ToString("X2");
        }


        /// <summary>
        /// 转为16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static string ToHex(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }


        /// <summary>
        /// 转为Base64编码字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }


        /// <summary>
        /// 用指定编码解码字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string GetString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 用指定编码编码字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        #endregion


        
    }
}
