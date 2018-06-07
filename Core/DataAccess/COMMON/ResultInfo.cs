using System.Collections.Generic;
using System.Linq;
namespace System
{
    public class ResultInfo<T>
    {
        private bool _Success=true;
        /// <summary>
        /// 表示执行是否成功(默认true)
        /// </summary>
        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }

        private int _Code;
        /// <summary>
        /// 执行返回代码
        /// </summary>
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Message=string.Empty;
        /// <summary>
        /// 执行返回消息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private T _Data=default(T);
        /// <summary>
        /// 执行返回结果
        /// </summary>
        public T Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private bool _HasException = false;
        /// <summary>
        /// 是否包含异常
        /// </summary>
        public bool HasException
        {
            get { return _HasException; }
            //set { _HasException = value; }
        }


        private IEnumerable<Exception> _ExceptionCollection=new List<Exception>();
        /// <summary>
        /// 异常集合
        /// </summary>
        public IEnumerable<Exception> ExceptionCollection
        {
            get { return _ExceptionCollection; }
            set { _ExceptionCollection = value; }
        }
        /// <summary>
        /// 绑定所有异常
        /// </summary>
        /// <param name="ex">传入顶级异常</param>
        public void BindAllException(Exception ex)
        {
            var exceptionList = new List<Exception>();
            
            bool hasInnerException=false;
            var tmpException = ex;
            if(ex!=null)
            {
                this._Success = false;
                _HasException = true;
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException!=null;
            }
            else
            {
                _HasException = false;
                return;
            }
            while(hasInnerException)
            {
                tmpException=tmpException.InnerException;
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException != null;
            }
            _ExceptionCollection = exceptionList;
        }
        /// <summary>
        /// 返回异常所有信息
        /// </summary>
        /// <param name="webHuanHang">是否为web换行</param>
        /// <param name="style">样式: color:red;  </param>
        /// <returns></returns>
        public string GetAllExceptionMessage(bool webHuanHang =false,string style="")
        {
            System.Text.StringBuilder message = new Text.StringBuilder("");
            if (_HasException)
            {
                int index = 0;
                _ExceptionCollection.Reverse().Distinct().ToList().ForEach(x => {
                    if (webHuanHang)
                    {
                        message.AppendFormat(@"<p style='{2}'>【{0}】:{1}</p>",index, x.Message,style);
                    }
                    else
                        message.AppendLine(x.Message);
                    index++;
                });
            }
            return message.ToString();
        }
    }
}
