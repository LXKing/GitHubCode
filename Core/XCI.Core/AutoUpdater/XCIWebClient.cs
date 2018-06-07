using System.Net;

namespace XCI.Component
{
    public class XCIWebClient : WebClient
    {
        private int _timeout = 10;
        /// <summary>
        /// 超时时间,单位秒.默认10秒
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        protected override WebRequest GetWebRequest(System.Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = 1000 * Timeout;
                request.ReadWriteTimeout = 1000 * Timeout;
            }
            return request;
        }
    }
}