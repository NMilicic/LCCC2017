using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class UserSkillRepository : BaseRepository<UserSkills, Guid>, IUserSkillRepository
    {
        public void AddNewSkillToUsers(Skills skill, IEnumerable<UserInfo> users)
        {
            throw new NotImplementedException();
        }

        public void AddSkillsToNewUser(UserInfo user, IEnumerable<Skills> skills)
        {
            throw new NotImplementedException();
        }
    }
}