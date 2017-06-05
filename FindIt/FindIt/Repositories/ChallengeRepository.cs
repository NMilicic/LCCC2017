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
        public static Func<string, string> Rejected = x => "Player " + x + " rejected your petty challenge! ";
        public static Func<string, string, double, double, string> Accepted = 
            (x, y, a, b) => "Player " + x + " accepted to ride to battle!" 
            + "And the winner is " + y + ", final score is " + a + "-" + b;

        private readonly IPlayedGameRepository _playedGameRepository = new PlayedGameRepository();
        private readonly IQuestionRepository _questionRepository = new QuestionRepository();

        public async Task<IEnumerable<ChallengeViewModel>> GetChallenges(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var challenges = await GetAllWhere(m => !m.Seen && !m.Response && m.ReceivingUserId == userGuid);
            var result = new List<ChallengeViewModel>();

            foreach (var challenge in challenges)
            {
                result.Add(new ChallengeViewModel(challenge));
                challenge.Seen = true;
                Update(challenge);
            }

            return result;
        }

        public async Task<IEnumerable<ChallengeViewModel>> GetChallengeResponse(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var responses = await GetAllWhere(m => !m.Seen && m.Response && m.ReceivingUserId == userGuid);
            var result = new List<ChallengeViewModel>();

            foreach (var challenge in responses)
            {
                result.Add(new ChallengeViewModel(challenge));
                challenge.Seen = true;
                Update(challenge);
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
                Response = false,
                Message = Challenged(challenger.Username)
            };

            Insert(challenge);
        }

        public void RespondToChallenge(string challengeId, string challengerId, UserInfo challengee, bool accepted = false,
            string playedGameId = null)
        {
            var challenge = GetById(Guid.Parse(challengeId));
            challenge.Seen = true;
            Update(challenge);

            var response = new Challenge()
            {
                ChallengeId = Guid.NewGuid(),
                ReceivingUserId = Guid.Parse(challengeId),
                SendingUserId = challengee.UserInfoId,
                Seen = false,
                Response = true
            };

            if (!accepted)
            {
                response.Message = Rejected(challengee.Username);
            }
            else
            {
                var challengerGuid = Guid.Parse(challengerId);
                var challengerGame = _playedGameRepository.GetById(challenge.GameId);
                var challengeeGame = _playedGameRepository.GetById(Guid.Parse(playedGameId));

                if (challengeeGame.Score > challengerGame.Score)
                {
                    response.Message = Accepted(challengee.Username, challengee.Username, 
                        challengeeGame.Score, challengerGame.Score);
                }
                else
                {
                    response.Message = Accepted(challengee.Username, "you", challengeeGame.Score, challengerGame.Score);
                }
            }

            Insert(response);
        }

        public NewGameViewModel GetChallengeGame(string playedGameId)
        {
            var playedGame = _playedGameRepository.GetById(Guid.Parse(playedGameId));
            var game = playedGame.Games;
            var questions = new List<Questions>
            {
                _questionRepository.GetById(game.Question1Id),
                _questionRepository.GetById(game.Question2Id),
                _questionRepository.GetById(game.Question3Id),
                _questionRepository.GetById(game.Question4Id),
                _questionRepository.GetById(game.Question5Id),
                _questionRepository.GetById(game.Question6Id),
                _questionRepository.GetById(game.Question7Id),
                _questionRepository.GetById(game.Question8Id),
                _questionRepository.GetById(game.Question9Id),
                _questionRepository.GetById(game.Question10Id)
            };


            return new NewGameViewModel(game.GameId, questions);
        }
    }
}