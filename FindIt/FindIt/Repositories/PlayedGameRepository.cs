using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;

namespace FindIt.Repositories
{
    public class PlayedGameRepository : BaseRepository<PlayedGames, Guid>, IPlayedGameRepository
    {

        private readonly IGameRepository _gameRepository = new GameRepository();
        private readonly IUserInfoRepository _userInfoRepository = new UserInfoRepository();
        public const int QuestionPoints = 1000;

        public PlayedGames CalculateScore(string gameId, GameAnswersViewModel answers, string userId)
        {
            var game = _gameRepository.GetById(Guid.Parse(gameId));

            var playedGame = new PlayedGames()
            {
                PlayedGameId = Guid.NewGuid(),
                DatePlayed = DateTime.Today,
                GameId = game.GameId,
                Score = CalculateScore(game, answers),
                UserInfoId = Guid.NewGuid()
            };

            Insert(playedGame);

            var user = _userInfoRepository.GetById(Guid.Parse(userId));
            user.TotalScore += playedGame.Score;

            if (user.HighScore < playedGame.Score)
            {
                user.HighScore = playedGame.Score;
            }

            _userInfoRepository.Update(user);

            return playedGame;
        }

        private static double CalculateScore(Games game, GameAnswersViewModel answers)
        {
            throw new NotImplementedException();
        }
    }
}