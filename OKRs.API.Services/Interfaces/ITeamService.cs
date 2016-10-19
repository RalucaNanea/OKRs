using OKRs.DataContract;

namespace OKRs.API.Services.Interfaces
{
    public interface ITeamService
    {
        TeamDto InsertTeam(TeamDto team);
        TeamDto GetTeamById(int teamId);
        bool UpdateTeam(TeamDto team);
        bool DeleteTeam(int teamId);
    }
}
