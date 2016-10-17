using System.Net;

namespace OKRs.ApiClient.Common
{
    public class WebResponseException: WebException
    {
        public WebResponseException(string message): base(message)
        {

        }

        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
    }
}
