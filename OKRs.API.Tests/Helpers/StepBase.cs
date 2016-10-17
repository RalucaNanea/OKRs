using OKRs.ApiClient.Common;
using OKRs.ApiClient.Common.CoreApiClient;
using System;
using System.Configuration;
using System.IO;

namespace OKRs.API.Tests.Helpers
{
    public class StepBase
    {
        protected static OKRsApiClient _apiClient;
        protected static string ResponseErrorMessage;
        protected static string ResponseStatusCode;

        public StepBase()
        {
            if (_apiClient == null)
            {
                var apiBaseUrl = ConfigurationManager.AppSettings["OKRsCoreApiBaseUrl"];
                _apiClient = new OKRsApiClient(apiBaseUrl);
            }
        }
        public string GetTestJsonFile(string path)
        {
            return File.ReadAllText(path);
        }

        public void CallApiCatchingWebException(Action apiCall)
        {
            try
            {
                apiCall.Invoke();
            }
            catch (WebResponseException ex)
            {
                ResponseErrorMessage = ex.ResponseContent.ToString();
                ResponseStatusCode = ex.StatusCode.ToString();
            }
        }
    }
}
