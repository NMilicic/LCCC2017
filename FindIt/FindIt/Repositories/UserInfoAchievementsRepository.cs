using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class UserInfoAchievementsRepository : BaseRepository<UserAchievements, Guid>, IUserAchievementsRepository
    {

        public void AddAchievementsToNewUser(UserInfo user, IEnumerable<Achievements> achievements)
        {
            var userAchievements = new List<UserAchievements>();

            foreach (var achievement in achievements)
            {
                userAchievements.Add(new UserAchievements()
                {
                    Achieved = false,
                    AchievementId = achievement.AchievementId,
                    UserInfoId = user.UserInfoId,
                    UserAchievementId = Guid.NewGuid()
                });
            }

            BatchInsert(userAchievements);
        }

        public void AddNewAchievementToUsers(Achievements achievement, IEnumerable<UserInfo> users)
        {
            var userAchievements = new List<UserAchievements>();

            foreach (var user in users)
            {
                userAchievements.Add(new UserAchievements()
                {
                    Achieved = false,
                    AchievementId = achievement.AchievementId,
                    UserInfoId = user.UserInfoId,
                    UserAchievementId = Guid.NewGuid()
                });
            }

            BatchInsert(userAchievements);
        }

    }
}