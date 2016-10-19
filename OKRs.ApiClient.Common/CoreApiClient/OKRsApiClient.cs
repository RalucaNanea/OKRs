using OKRs.ApiClient.Common.Extensions;
using OKRs.DataContract;
using RestSharp;
using System.Net;

namespace OKRs.ApiClient.Common.CoreApiClient
{
    public class OKRsApiClient
    {
        private RestClient _client;

        public OKRsApiClient(string apiBaseUrl)
        {
            _client = new RestClient(apiBaseUrl);
        }

        public TeamDto InsertTeam(TeamDto teamDtoRequest)
        {
            var request = _client.CreateRequest($"team/");
            request.AddJsonBody(teamDtoRequest);

            return _client
               .ExecuteAsPost<TeamDto>(request, "POST")
               .AsClientResponseExpecting(HttpStatusCode.Created);
        }
    }
}
