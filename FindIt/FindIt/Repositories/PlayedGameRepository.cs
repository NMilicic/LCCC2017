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
    public class PlayedGameRepository : BaseRepository<PlayedGames, Guid>, IPlayedGameRepository
    {

        private readonly IGameRepository _gameRepository = new GameRepository();
        private readonly IUserAchievementsRepository _userAchievementsRepository = new UserInfoAchievementsRepository();
        private readonly IAchievementRepository _achievementRepository = new AchievementRepository();

        public const int QuestionPoints = 1000;
        public const int NoOfQuestions = 10;
        public const double DegreeToKm = 111.12;

        public async Task<PostGameViewModel> CalculateScore(string gameId, GameAnswersViewModel answers, string userId)
        {
            var game = _gameRepository.GetById(Guid.Parse(gameId));

            var playedGame = new PlayedGames()
            {
                PlayedGameId = Guid.NewGuid(),
                DatePlayed = DateTime.Now,
                GameId = game.GameId,
                Score = CalculateScore(game, answers),
                UserInfoId = Guid.NewGuid()
            };

            Insert(playedGame);

            var userInfoRepository =  new UserInfoRepository();

            var user = userInfoRepository.GetById(Guid.Parse(userId));
            user.TotalScore += playedGame.Score;

            if (user.HighScore < playedGame.Score)
            {
                user.HighScore = playedGame.Score;
            }

            userInfoRepository.Update(user);

            var usersAchievements = await userInfoRepository.GetEarnedAchievements(user.UserInfoId.ToString());
            var achievements = await GetNewAchievements(usersAchievements, playedGame.Score, user.UserInfoId);

            return new PostGameViewModel()
            {
                Score = playedGame.Score,
                NewAchievements = achievements
            };
        }

        private async Task<List<Achievements>> GetNewAchievements(IEnumerable<Achievements> usersAchievements, 
            double score, Guid userGuid)
        {
            var achievements = new List<Achievements>();

            if (score < QuestionPoints * NoOfQuestions * 0.5)
            {
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.LessThan50Id, achievements);
            }

            if (score >= QuestionPoints * NoOfQuestions * 0.5 && score < QuestionPoints * NoOfQuestions * 0.6)
            {
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.LessThan60Id, achievements);
            }

            if (score >= QuestionPoints * NoOfQuestions * 0.6 && score < QuestionPoints * NoOfQuestions * 0.8)
            {
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.LessThan80Id, achievements);
            }

            if (score >= QuestionPoints * NoOfQuestions * 0.8 && score < QuestionPoints * NoOfQuestions * 0.9)
            {
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.LessThan90Id, achievements);
            }

            if (score >= QuestionPoints * NoOfQuestions * 0.9)
            {
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.LessThan90Id, achievements);
            }

            return achievements;
        }

        private async Task CheckIfAchievementNew(IEnumerable<Achievements> usersAchievements, Guid userGuid, 
            Guid achievementGuid, List<Achievements> achievements)
        {
            if (!usersAchievements.Any(m => m.AchievementId == achievementGuid))
            {
                await _userAchievementsRepository.SetAchievement(achievementGuid, userGuid);
                achievements.Add(_achievementRepository.GetById(achievementGuid));
            }
        }

        private static double CalculateScore(Games game, GameAnswersViewModel answers)
        {
            var score = 0.0;

            var tempScore = GetTempScore(game.Questions1, answers, 1);
            score += tempScore;
            tempScore = GetTempScore(game.Questions2, answers, 2);
            score += tempScore;
            tempScore = GetTempScore(game.Questions3, answers, 3);
            score += tempScore;
            tempScore = GetTempScore(game.Questions4, answers, 4);
            score += tempScore;
            tempScore = GetTempScore(game.Questions5, answers, 5);
            score += tempScore;
            tempScore = GetTempScore(game.Questions6, answers, 6);
            score += tempScore;
            tempScore = GetTempScore(game.Questions7, answers, 7);
            score += tempScore;
            tempScore = GetTempScore(game.Questions8, answers, 8);
            score += tempScore;
            tempScore = GetTempScore(game.Questions9, answers, 9);
            score += tempScore;
            tempScore = GetTempScore(game.Questions, answers, 0);
            score += tempScore;

            return score * QuestionPoints;
        }

        private static double GetTempScore(Questions question1, GameAnswersViewModel answers, int questionIndex)
        {
            var tempScore =
                CostFunction(DegreeDifferenceToKmDifference(question1.Latitude, question1.Longitude,
                    answers.Latitudes[questionIndex], answers.Longitudes[questionIndex]));
            if (answers.Hints2Used[questionIndex])
            {
                tempScore *= 0.25;
            }
            else if (answers.Hints1Used[questionIndex])
            {
                tempScore *= 0.5;
            }
            return tempScore;
        }

        private static double CostFunction(double x)
        {
            return 1 / Math.Cosh(4*x);
        }

        private static double DegreeDifferenceToKmDifference(double gameLatitude, double gameLongitude,
            double answerLatitude, double answerLongitude)
        {
            return Math.Sqrt(Math.Pow((gameLatitude - answerLatitude)*DegreeToKm, 2)
                           + Math.Pow((gameLongitude - answerLongitude)*DegreeToKm, 2));
        }
    }
}