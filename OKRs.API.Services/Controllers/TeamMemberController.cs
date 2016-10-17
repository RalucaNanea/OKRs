using OKRs.API.Services.Interfaces;
using OKRs.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace OKRs.API.Services.Controllers
{
    public class TeamMemberController: ApiController
    {
        private readonly ITeamMemberService _teamMemberService;
        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [ResponseType(typeof(TeamMembersDto))]
        [HttpPost, Route("teamMember/", Name = "InsertTeamMember")]
        public HttpResponseMessage InsertTeamMember([FromBody]TeamMembersDto teamMember)
        {
            if (teamMember == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "team member is null");

            _teamMemberService.InsertTeamMember(teamMember);
            return Request.CreateResponse(HttpStatusCode.Created, teamMember);
        }
    }
}