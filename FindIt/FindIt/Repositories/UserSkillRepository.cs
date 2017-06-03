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
            var userSkills = new List<UserSkills>();

            foreach (var user in users)
            {
                userSkills.Add(new UserSkills()
                {
                    Activated = false,
                    SkillId = skill.SkillId,
                    UserInfoId = user.UserInfoId,
                    UserSkillId = Guid.NewGuid()
                });
            }

            BatchInsert(userSkills);
        }

        public void AddSkillsToNewUser(UserInfo user, IEnumerable<Skills> skills)
        {
            var userSkills = new List<UserSkills>();

            foreach (var skill in skills)
            {
                userSkills.Add(new UserSkills()
                {
                    Activated = false,
                    SkillId = skill.SkillId,
                    UserInfoId = user.UserInfoId,
                    UserSkillId = Guid.NewGuid()
                });
            }

            BatchInsert(userSkills);
        }
    }
}