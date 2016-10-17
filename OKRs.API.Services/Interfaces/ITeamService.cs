using OKRs.DataContract;

namespace OKRs.API.Services.Interfaces
{
    public interface ITeamService
    {
        int InsertTeam(TeamDto team);
        TeamDto GetTeamById(int teamId);
        bool UpdateTeam(TeamDto team);
        bool DeleteTeam(int teamId);
    }
}
