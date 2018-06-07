using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamOnLine.Ashx
{
    public class BaseFuncHandler
    {

       protected  HttpContext context;
        /// <summary>
        /// 获取参数，如果是post请求,则用Form[变量名]取值,如果是get请求,用QueryString[变量名]取值.
        /// </summary>
        /// <param name="parameterName">变量名称</param>
        /// <returns>返回字符串，没有找到则返回string.Empty</returns>
        protected string GetParameter(HttpContext context, string parameterName, string defaultValue = "")
        {

            string result = defaultValue;
            if (context != null)
            {
                if (context.Request.RequestType.ToLower() == "get")
                {
                    result = context.Request.QueryString[parameterName];
                    if (string.IsNullOrEmpty(result))
                        result = context.Request.Form[parameterName];
                }
                if (context.Request.RequestType.ToLower() == "post")
                {
                    result = context.Request.Form[parameterName];
                    if (string.IsNullOrEmpty(result))
                        result = context.Request.QueryString[parameterName];
                }
            }
            return result;
        }

        protected string GetParameter(string parameterName, string defaultValue = "")
        {

            string result = defaultValue;
            if (context != null)
            {
                if (context.Request.RequestType.ToLower() == "get")
                {
                    result = context.Request.QueryString[parameterName];
                    if (string.IsNullOrEmpty(result))
                        result = context.Request.Form[parameterName];
                }
                if (context.Request.RequestType.ToLower() == "post")
                {
                    result = context.Request.Form[parameterName];
                    if (string.IsNullOrEmpty(result))
                        result = context.Request.QueryString[parameterName];
                }
            }
            return result;
        }
        /// <summary>
        /// 创建cookie集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cookieDic"></param>
        /// <param name="minuts"></param>
        /// <returns></returns>
        protected HttpCookie CreateCookie(string name,Dictionary<string,string> cookieDic,int? minuts=null)
        {
            HttpCookie cookie = new HttpCookie(name);//初使化并设置Cookie的名称
            DateTime dt=DateTime.Now;
            
            cookieDic.ToList().ForEach(x=>{
                cookie.Values.Add(x.Key,x.Value);
            });
            if(minuts.NotNull())
            {
                TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(minuts),0,0);
                cookie.Expires = dt.Add(ts);
            } 
            
            return cookie;
        }
        /// <summary>
        /// 创建单值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cookieValue"></param>
        /// <param name="minuts"></param>
        /// <returns></returns>
         protected HttpCookie CreateCookie(string name,string cookieValue,int? minuts=null)
        {
            HttpCookie cookie = new HttpCookie(name);//初使化并设置Cookie的名称
            cookie.Value = cookieValue;
            if (minuts.NotNull())
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(minuts), 0, 0);
                cookie.Expires = dt.Add(ts);
            } 
            return cookie;
        }

        /// <summary>
        /// 获取参数传来的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected T GetParameterObject<T>(HttpContext context, string parameterName)
        {
            var text = GetParameter(context, parameterName);
            if (string.IsNullOrEmpty(text)) return default(T);
            return text.JsonToObject<T>();
        }
    }

    public class WebResult
    {
        public bool Success { get; set; }
        public string MessageInfo { get; set; }
        public object Data { get; set; }
    }


}