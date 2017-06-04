using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;

namespace FindIt.Repositories
{
    public class UserInfoRepository : BaseRepository<UserInfo, Guid>, IUserInfoRepository
    {
        private readonly IUserAchievementsRepository _userAchievementsRepository = new UserInfoAchievementsRepository();
        private readonly IUserSkillRepository _userSkillRepository = new UserSkillRepository();
        private readonly IChallengeRepository _challengeRepository = new ChallengeRepository();

        public async Task CreateUserInfoFromUser(string identityId, string identityUsername)
        {
            var userInfo = new UserInfo
            {
                UserInfoId = Guid.Parse(identityId),
                AvatarUri = "",
                Username = identityUsername
            };

            Insert(userInfo);

            var achievements = await new AchievementRepository().GetAll();
            var skills = await new SkillRepository().GetAll();

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

        public async Task<IEnumerable<Skills>> GetUserSkills(string userId)
        {
            var userGuid = Guid.Parse(userId);

            return (await _userSkillRepository.GetAllWhere(m => m.UserInfoId == userGuid)).Select(m => m.Skills);
        }

        public async Task<bool> ActivateSkill(string userId, string skillId)
        {
            var userGuid = Guid.Parse(userId);
            var user = GetById(userGuid);

            var skillGuid = Guid.Parse(skillId);
            var skill = new SkillRepository().GetById(skillGuid);

            if (user.Coins < skill.Cost)
            {
                return false;
            }


            var userSkill =
                (await _userSkillRepository.GetAllWhere(m => (m.SkillId == skillGuid) && (m.UserInfoId == userGuid)))
                    .First();

            userSkill.Activated = true;
            user.Coins -= skill.Cost;

            Update(user);
            _userSkillRepository.Update(userSkill);

            return true;
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
            var index = 0;

            for (var i = 0; i < users.Count; i++)
            {
                if (users[i].UserInfoId == userGuid)
                {
                    index = i;
                    break;
                }
            }

            return index + 1;
        }

        public async Task<IEnumerable<PlayedGames>> GetPlayedGames(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var playedGameRepository = new PlayedGameRepository();

            return await playedGameRepository.GetAllWhere(m => m.UserInfoId == userGuid);
        }

        public async Task<PlayedGames> GetBestGame(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var playedGameRepository = new PlayedGameRepository();
            return (await playedGameRepository.GetAllWhere(m => m.UserInfoId == userGuid))
                .OrderBy(m => m.Score)
                .First();
        }

        public Task<IEnumerable<ChallengeViewModel>> GetChallenges(string userId)
        {
            return _challengeRepository.GetChallenges(userId);
        }

        public void CreateChallenge(string challengerId, string challengeeId, string gameId)
        {
            var user = GetById(Guid.Parse(challengerId));

            _challengeRepository.CreateChallenge(user, challengeeId, gameId);
        }
    }
}