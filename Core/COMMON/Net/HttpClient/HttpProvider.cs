namespace COMMON.Net.HttpClient
{
    public class HttpClient:IHttpClient
    {
        public HttpResponseParameter Excute(HttpRequestParameter requestParameter)
        {
            return HttpUtil.Excute(requestParameter);
        }


        public HttpResponseParameter ExcuteWithStringConten(HttpRequestParameter requestParameter)
        {
            return HttpUtil.ExcuteWithStringContent(requestParameter);
        }
    }
}
