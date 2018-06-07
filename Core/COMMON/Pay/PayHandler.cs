using COMMON.Net.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Pay
{
    /// <summary>
    /// 支付基类
    /// </summary>
    public abstract class PayHandler
    {
        private HttpRequest _RequestObject;

        public HttpRequest RequestObject
        {
            get { return _RequestObject; }
            set { _RequestObject = value; }
        }

        Dictionary<string, string> _CommitParms = new Dictionary<string, string>();

        public Dictionary<string, string> CommitParms
        {
            get { return _CommitParms; }
            set { _CommitParms = value; }
        }
        Dictionary<string, string> _UserParms = new Dictionary<string, string>();

        public Dictionary<string, string> UserParms
        {
            get { return _UserParms; }
            set { _UserParms = value; }
        }

        KeyValuePair<string, string> Sign;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="request"></param>
        public PayHandler(HttpRequest request, ParmsFromType parmsType = ParmsFromType.Params,string CommitParmsFlag="$_",string UserParmsFlag="P_")
        {
            #region 初始化
            if (request == null)
                throw new Exception("请求对象HttpRequest不能为空");
            else
            {
                RequestObject = request;
            } 
            #endregion

            #region 参数
            NameValueCollection ParmsAll=null;
            switch (parmsType)
            {
                case ParmsFromType.QueryString:
                    ParmsAll=RequestObject.QueryString;
                    break;
                case ParmsFromType.Form:
                    ParmsAll=RequestObject.Form;
                    break;
                case ParmsFromType.Params:
                    ParmsAll = RequestObject.Params;
                    break;
            }
            if (ParmsAll==null || ParmsAll.Count == 0)
            {
                throw new Exception("请求对象HttpRequest未取到任何参数");
            }
            else
            {
                for(var i=0;i<ParmsAll.Count;i++)
                {
                    var key = ParmsAll.GetKey(i);
                    var value = ParmsAll.GetValues(i)[0];
                    if(key.StartsWith(CommitParmsFlag))
                    {
                        CommitParms.Add(key, value);
                    }
                    if (key.StartsWith(UserParmsFlag))
                    {
                        UserParms.Add(key, value);
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="signKey">签名生成键值对的键名</param>
        /// <returns></returns>
        public abstract KeyValuePair<string, string> CreateSign(string signKey); 
        public virtual string DoRequest_Content(string Url, bool IsPost, KeyValuePair<string, string> sign)
        {
            var respContent = string.Empty;
            HttpClient client = new HttpClient();
            HttpRequestParameter parms = new HttpRequestParameter()
            {
                IsPost = IsPost,
                Encoding = System.Text.Encoding.UTF8,
                Url = Url,
                ContentInput = this.CreatePostData(sign)
            };
            var rsp = client.Excute(parms);
            respContent = rsp.Body;
            return respContent;
        }
        public virtual string DoRequest(string Url,bool IsPost,KeyValuePair<string,string> sign)
        {
            var respContent = string.Empty;
            HttpClient client = new HttpClient();
            HttpRequestParameter parms = new HttpRequestParameter() { 
                IsPost=IsPost,
                Encoding=System.Text.Encoding.UTF8,
                Url=Url,
                Parameters=CommitParms.AddOrReplace(sign.Key,sign.Value)
            };
            var rsp = client.Excute(parms);
            respContent = rsp.Body;
            return respContent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="IsPost"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public virtual string DoRequest_JsonContent(string Url, bool IsPost, KeyValuePair<string, string> sign)
        {
            return DoRequest_Content(Url,IsPost,sign);
        }

        public virtual string CreatePostData(string signKey)
        {
            Sign=CreateSign(signKey);
            var json = CommitParms.AddOrReplace(Sign.Key, Sign.Value).ToJsonString().FormatJsonString();
            return json;
        }

        public virtual string CreatePostData(KeyValuePair<string,string> sign)
        {
            Sign = sign;
            var json = CommitParms.AddOrReplace(Sign.Key, Sign.Value).ToJsonString().FormatJsonString();
            return json;
        }
       
        /// <summary>
        /// 键值对参数转字符串
        /// </summary>
        /// <param name="Dic"></param>
        /// <param name="IsASCOrder"></param>
        /// <returns></returns>
        public string QueryStringToValueKey(Dictionary<string,string> Dic,bool IsASCOrder)
        {
            StringBuilder result = new StringBuilder();
            OrdinalComparer comp = new OrdinalComparer();
            List<KeyValuePair<string,string>> orderedResult;
            if (IsASCOrder)
            {
                orderedResult = Dic.OrderBy(
                (keyvalue) =>
                {
                    return keyvalue.Key;
                },
            comp
            ).ToList();
            }
            else
            {
                orderedResult = Dic.OrderByDescending(
                (keyvalue) =>
                {
                    return keyvalue.Key;
                },
            comp
            ).ToList();
            }
            orderedResult.ForEach(x =>
                {
                    result.AppendFormat("{0}={1}&", x.Key, x.Value);
                }
            );
            result = result.Remove(result.Length - 1, 1);

            return result.ToString();
        }
    }
    /// <summary>
    /// 字符串ASIC码表排序
    /// </summary>
    public class OrdinalComparer : System.Collections.Generic.IComparer<String>
    {
        public int Compare(String x, String y)
        {
            return string.CompareOrdinal(x, y);
        }
    }

    /// <summary>
    /// 参数来源类型
    /// </summary>
    public enum ParmsFromType
    {
        /// <summary>
        /// 查询字符串
        /// </summary>
        QueryString,
        /// <summary>
        /// 请求参数
        /// </summary>
        Params,
        /// <summary>
        /// 请求表单
        /// </summary>
        Form
    }
}
