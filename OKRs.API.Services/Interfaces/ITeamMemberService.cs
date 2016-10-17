using OKRs.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKRs.API.Services.Interfaces
{
    public interface ITeamMemberService
    {
        int InsertTeamMember(TeamMembersDto teamMember);
    }
}
