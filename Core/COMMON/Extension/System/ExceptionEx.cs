using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ExceptionEx
    {
        /// <summary>
        /// 返回异常所有信息
        /// </summary>
        /// <param name="webHuanHang">是否为web换行</param>
        /// <param name="style">样式: color:red;  </param>
        /// <returns></returns>
        public static string GetAllExceptionMessage(this Exception ex,bool webHuanHang = false, string style = "")
        {
            var exceptionList = new List<Exception>();

            bool hasInnerException = false;
            var tmpException = ex;
            if (ex != null)
            {
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException != null;
            }
            while (hasInnerException)
            {
                tmpException = tmpException.InnerException;
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException != null;
            }

            StringBuilder message = new Text.StringBuilder("");
            int index = 0;
            exceptionList.Reverse();
            exceptionList.Distinct().ToList().ForEach(x =>
            {
                if (webHuanHang)
                {
                    message.AppendFormat(@"<p style='{2}'>【{0}】:{1}</p>", index, x.Message, style);
                }
                else
                    message.AppendLine(x.Message);
                index++;
            });
            return message.ToString();
        }
    }
}
