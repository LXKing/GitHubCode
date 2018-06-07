namespace COMMON.Net.HttpClient
{
    public interface IHttpClient
    {
        HttpResponseParameter Excute(HttpRequestParameter requestParameter);
        HttpResponseParameter ExcuteWithStringConten(HttpRequestParameter requestParameter);
    }
}
