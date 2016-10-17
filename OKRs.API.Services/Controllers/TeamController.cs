using OKRs.API.Services.Interfaces;
using OKRs.DataContract;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OKRs.API.Services.Controllers
{
    public class TeamController : ApiController
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [ResponseType(typeof(TeamDto))]
        [HttpGet, Route("team/{teamId}", Name = "GetTeamById")]
        public HttpResponseMessage GetTeamById(int teamId)
        {
            var team = _teamService.GetTeamById(teamId);
            if (team == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Team with Id={teamId} not found");

            return Request.CreateResponse(HttpStatusCode.OK, team);
        }

        [ResponseType(typeof(TeamDto))]
        [HttpPost, Route("team/", Name = "InsertTeam")]
        public HttpResponseMessage InsertTeam([FromBody]TeamDto team)
        {
            if (team == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "team is null");

            _teamService.InsertTeam(team);
            return Request.CreateResponse(HttpStatusCode.Created, team);
        }

        [ResponseType(typeof(TeamDto))]
        [HttpPost, Route("team/Update", Name = "UpdateTeam")]
        public HttpResponseMessage UpdateTeam([FromBody]TeamDto team)
        {
            if (team  == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "team is null");

            _teamService.UpdateTeam(team);
            return Request.CreateResponse(HttpStatusCode.Created, team);
        }

        [ResponseType(typeof(TeamDto))]
        [HttpPost, Route("delete/", Name = "DeleteTeam")]
        public HttpResponseMessage DeleteTeam([FromBody]int teamId)
        {
            var team = _teamService.DeleteTeam(teamId);
            return Request.CreateResponse(HttpStatusCode.Created, team);
        }
    }
}