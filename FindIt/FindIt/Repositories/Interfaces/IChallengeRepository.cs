using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.ViewModels;

namespace FindIt.Repositories.Interfaces
{
    public interface IChallengeRepository : IRepository<Challenge, Guid>
    {
        Task<IEnumerable<ChallengeViewModel>> GetChallenges(string userId);
        Task<IEnumerable<ChallengeViewModel>> GetChallengeResponse(string userId);

        void CreateChallenge(UserInfo challenger, string challengeeId, string gameId);
        void RespondToChallenge(string challengeId, string challengerId, UserInfo challengee, 
            bool accepted =false, string playedGameId = null);
        
    }
}
