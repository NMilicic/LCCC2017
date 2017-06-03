using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class UserInfoRepository : BaseRepository<UserInfo, Guid>, IUserInfoRepository
    {
   
        private readonly IUserAchievementsRepository _userAchievementsRepository = new UserInfoAchievementsRepository();

        public async Task CreateUserInfoFromUser(string identityId, string identityUsername)
        {
            var userInfo = new UserInfo()
            {
                UserInfoId = Guid.Parse(identityId),
                AvatarUri = "",
                Username = identityUsername
            };

            Insert(userInfo);

            var achievements = await (new AchievementRepository()).GetAll();

            _userAchievementsRepository.AddAchievementsToNewUser(userInfo, achievements);
        }

        public async Task<IEnumerable<Achievements>> GetEarnedAchievements(string userId)
        {
            var earned = await _userAchievementsRepository.GetAllWhere(m => m.UserInfoId == Guid.Parse(userId));
            var achievements = new List<Achievements>();

            foreach (var achievement in earned)
            {
                achievements.Add(achievement.Achievements);
            }

            return achievements;
        }
    }
}