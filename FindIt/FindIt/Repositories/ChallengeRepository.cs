using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;

namespace FindIt.Repositories
{
    public class ChallengeRepository : BaseRepository<Challenge, Guid>, IChallengeRepository
    {

        public static Func<string, string> Challenged =
            x => "Player " + x + " challenged you to a game! Destroy him swiftly!";

        public async Task<IEnumerable<ChallengeViewModel>> GetChallenges(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var challenges = await GetAllWhere(m => !m.Seen && m.ReceivingUserId == userGuid);
            var result = new List<ChallengeViewModel>();

            foreach (var challenge in challenges)
            {
                result.Add(new ChallengeViewModel(challenge));
            }

            return result;
        }

        public void CreateChallenge(UserInfo challenger, string challengeeId, string gameId)
        {
            var challenge = new Challenge()
            {
                ChallengeId = Guid.NewGuid(),
                GameId = Guid.Parse(gameId),
                SendingUserId = challenger.UserInfoId,
                ReceivingUserId = Guid.Parse(challengeeId),
                Seen = false,
                Message = Challenged(challenger.Username)
            };

            Insert(challenge);
        }
    }
}