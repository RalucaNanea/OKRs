using RestSharp;
using System.Linq;
using System.Net;

namespace OKRs.ApiClient.Common.Extensions
{
    public static class RestExtensions
    {
        public static T AsClientResponseExpecting<T>(this IRestResponse<T> response, HttpStatusCode expectedStatusCode, params ResultMap<T>[] resultMap)
        {
            var map = resultMap.FirstOrDefault(r => r.StatusCode == response.StatusCode);
            if (map != null)
                return map.Response;

            if (IsUnexpectedStatusCode(response, expectedStatusCode))
            {
                var exception = new WebResponseException(string.Format("Unexpected Status Code - Expected: {0}, Received: {1}", expectedStatusCode, response.StatusCode));
                exception.StatusCode = response.StatusCode;
                exception.ResponseContent = response.Content;
                throw exception;
            }

            if (response.ErrorException != null)
                throw response.ErrorException;

            return response.Data;
        }

        private static bool IsUnexpectedStatusCode(IRestResponse response, HttpStatusCode expectedStatusCode)
        {
            // If the status code is 0, this means that an error occured connecting to the server and
            // the response code should be ignored as a comparison.
            return response.StatusCode != 0
                && response.StatusCode != expectedStatusCode;
        }

        public static RestRequest CreateRequest(this RestClient client, string resource, Method method = Method.GET)
        {
            var request = new RestRequest(resource, method);
            return request;
        }
    }
}
