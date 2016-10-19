using Newtonsoft.Json;
using OKRs.API.Tests.Helpers;
using OKRs.DataContract;
using Shouldly;
using System.IO;
using TechTalk.SpecFlow;

namespace OKRs.API.Tests.Steps
{
    [Binding]
    public class InsertTeamSteps : StepBase
    {
        private TeamDto _teamDtoRequest;
        private TeamDto _teamDtoResponse;

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

        [Then(@"the result should be the TeamDto submitted with the property TeamId greater than zero")]
        public void ThenTheResultShouldBeTheTeamDtoSubmittedWithThePropertyTeamIdGreaterThanZero()
        {
             _teamDtoResponse.TeamId.ShouldBeGreaterThan(0);
        }

        [When(@"I update TeamDto with empty team region")]
        public void WhenIUpdateTeamDtoWithEmptyTeamRegion()
        {
            _teamDtoRequest.Region = null;
        }


        [Then(@"the API returns '(.*)' status code")]
        public void ThenTheAPIReturnsStatusCode(string expectedStatusCode)
        {
            ResponseStatusCode.ShouldBe(expectedStatusCode);
        }

        [Then(@"'(.*)' error message is returned as a response")]
        public void ThenErrorMessageIsReturnedAsAResponse(string expectedErrorMessage)
        {
            ResponseErrorMessage.ShouldContain(expectedErrorMessage);
        }

    }
}
