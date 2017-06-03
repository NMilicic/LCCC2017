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
        private readonly IAchievementRepository _achievementRepository = new AchievementRepository();
        private readonly IUserInfoRepository _userInfoRepository = new UserInfoRepository();

        public async Task AddAchievementsToNewUser(UserInfo user)
        {
            var achievements = await _achievementRepository.GetAll();
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

        public async Task AddNewAchievementToUsers(Achievements achievement)
        {
            var users = await _userInfoRepository.GetAll();
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