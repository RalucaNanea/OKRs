using OKRs.API.Services.Interfaces;
using OKRs.DataContract;
using System.Data;
using static OKRs.API.Services.Infrastructure.TransactionFactoryDelegates;

namespace OKRs.API.Services.Services
{
    public class TeamMemberService: ITeamMemberService
    {
        private OKRsDataAccessFactory _dataAccessLayer;
        public TeamMemberService(OKRsDataAccessFactory dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public int InsertTeamMember(TeamMembersDto teamMember)
        {
            var dataAccess = _dataAccessLayer();
            string sqlQuery =
            @"INSERT INTO TeamMembers (TeamId, DevName)
            VALUES (@teamId, @devName);
            SELECT SCOPE_IDENTITY();";

            var teamMemberId = dataAccess.ExecuteScalar<int>(sqlQuery, new { teamId= teamMember.TeamId, devName= teamMember.DevName }, CommandType.Text);
            return teamMemberId;
        }
    }
}