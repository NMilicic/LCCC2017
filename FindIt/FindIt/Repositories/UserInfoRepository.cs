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
        private readonly IUserSkillRepository _userSkillRepository = new UserSkillRepository();

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
            var skills = await (new SkillRepository()).GetAll();

            _userAchievementsRepository.AddAchievementsToNewUser(userInfo, achievements);
            _userSkillRepository.AddSkillsToNewUser(userInfo, skills);
        }

        public async Task<IEnumerable<Achievements>> GetEarnedAchievements(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var earned = await _userAchievementsRepository.GetAllWhere(m => m.UserInfoId == userGuid);
            var achievements = new List<Achievements>();

            foreach (var achievement in earned)
            {
                achievements.Add(achievement.Achievements);
            }

            return achievements;
        }

        public async Task<IEnumerable<UserInfo>> GetTopPlayers(int n)
        {
            var users = await GetAll();
            return users.OrderBy(m => m.HighScore).ThenBy(m => m.TotalScore).Take(n).ToList();
        }

        public async Task<int> GetPlayersLeaderboardPosition(string userId)
        {
            var users = await GetAll();
            users = users.OrderBy(m => m.HighScore).ThenBy(m => m.TotalScore).ToList();
            var userGuid = Guid.Parse(userId);
            int index = 0;

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserInfoId == userGuid)
                {
                    index = i;
                    break;
                }
            }

            return index + 1;
        }
    }
}