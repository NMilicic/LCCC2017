using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;

namespace FindIt.Repositories.Interfaces
{
    public interface IUserSkillRepository : IRepository<UserSkills, Guid>
    {
        void AddNewSkillToUsers(Skills skill, IEnumerable<UserInfo> users);
        void AddSkillsToNewUser(UserInfo user, IEnumerable<Skills> skills);
    }
}
