using Newtonsoft.Json;
using OKRs.API.Tests.Helpers;
using OKRs.DataContract;
using Shouldly;
using System.IO;
using TechTalk.SpecFlow;

namespace OKRs.API.Tests.Steps
{
    [Binding]
    public class InsertTeamSteps: StepBase
    {
        private TeamDto _teamDtoRequest;
        private int _teamDtoResponse;

        [Given(@"I have a valid TeamDto")]
        public void GivenIHaveAValidTeamDto()
        {
            var teamJson = GetTestJsonFile(Path.GetFullPath(@"TeamDto.json"));
            _teamDtoRequest = JsonConvert.DeserializeObject<TeamDto>(teamJson);
        }

        [When(@"I call the insert Team API endpoint")]
        public void WhenICallTheInsertTeamAPIEndpoint()
        {
            CallApiCatchingWebException(() =>
            {
                _teamDtoResponse = _apiClient.InsertTeam(_teamDtoRequest);
            });
        }

        [Then(@"(.*) success code is returned as a response")]
        public void ThenSuccessCodeIsReturnedAsAResponse(int p0)
        {
            var test = _teamDtoResponse;
            //teamDtoResponse.ShouldBe(true);
        }

    }
}
