using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.ViewModels;

namespace FindIt.Repositories.Interfaces
{
    public interface IUserInfoRepository : IRepository<UserInfo, Guid>
    {
        Task CreateUserInfoFromUser(string identityId, string identityUsername);
        Task<IEnumerable<Achievements>> GetEarnedAchievements(string userId);
        Task<IEnumerable<Skills>> GetUserSkills(string userId);
        Task<bool> ActivateSkill(string userId, string skillId);
        Task<IEnumerable<UserInfo>> GetTopPlayers(int n);
        Task<int> GetPlayersLeaderboardPosition(string userId);
        Task<IEnumerable<PlayedGames>> GetPlayedGames(string userId);
        Task<PlayedGames> GetBestGame(string userId);
        Task<IEnumerable<ChallengeViewModel>> GetChallenges(string userId);
        void CreateChallenge(string challengerId, string challengeeId, string gameId);
    }
}
