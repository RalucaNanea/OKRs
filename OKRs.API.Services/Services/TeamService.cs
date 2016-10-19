using OKRs.API.Services.Interfaces;
using OKRs.DataContract;
using System;
using System.Data;
using System.Linq;
using static OKRs.API.Services.Infrastructure.TransactionFactoryDelegates;

namespace OKRs.API.Services.Services
{
    public class TeamService : ITeamService
    {
        private OKRsDataAccessFactory _dataAccessLayer;
        public TeamService(OKRsDataAccessFactory dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        public TeamDto InsertTeam(TeamDto team)
        {
            if (team.Region == null)
            {
                throw new ArgumentException($"Team region is null");
            }

            var dataAccess = _dataAccessLayer();
            string sqlQuery =
            @"INSERT INTO Team (TeamName, Region, ProductManager, TechLead)
            VALUES (@teamName, @region, @productManager, @techLead);
            SELECT SCOPE_IDENTITY();";

            team.TeamId = dataAccess.ExecuteScalar<int>(sqlQuery, new { teamName = team.TeamName, region = team.Region, productManager=team.ProductManager, techLead = team.TechLead }, CommandType.Text);
            return team;
        }

        public TeamDto GetTeamById(int teamId)
        {
            var dataAccess = _dataAccessLayer();
            string sqlQuery =
                @"SELECT TOP 1000 [TeamId]
                  ,[TeamName]
                  ,[Region]
                FROM Team where TeamId=@Id";
            var team= dataAccess.Query<TeamDto>(sqlQuery, new { Id = teamId }, CommandType.Text).ToList().FirstOrDefault();
            return team;
        }

        public bool UpdateTeam(TeamDto team)
        {
            var dataAccess = _dataAccessLayer();
            var sql =
            @"UPDATE team
            SET
                teamName = @teamName,
                region = @region,
                productManager = @productManager,
                techLead = @techLead
            WHERE teamId=@teamId;";

            var numOfRowsAffected = dataAccess.ExecuteScalar<int>(
                sql,
                new
                {
                    teamId = team.TeamId,
                    teamName = team.TeamName,
                    region = team.Region,
                    productManager = team.ProductManager,
                    techLead = team.TechLead
                },
                CommandType.Text);

            return numOfRowsAffected > 0;
        }

        public bool DeleteTeam(int teamId)
        {
            var dataAccess = _dataAccessLayer();
            string sqlQuery = @"DELETE FROM Team WHERE TeamId=@Id";

            var numOfRowsAffected = dataAccess.ExecuteScalar<int>(
                sqlQuery,
                new
                {
                    Id = teamId
                },
                CommandType.Text);

            return numOfRowsAffected > 0;
        }
    }
 }
