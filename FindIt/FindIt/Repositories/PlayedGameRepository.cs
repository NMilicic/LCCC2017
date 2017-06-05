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
        public const double CostFactor = 0.01;

        public async Task<PostGameViewModel> CalculateScore(GameAnswersViewModel answers, string userId)
        {
            var game = _gameRepository.GetById(Guid.Parse(answers.GameId));

            var playedGame = new PlayedGames()
            {
                PlayedGameId = Guid.NewGuid(),
                DatePlayed = DateTime.Now,
                GameId = game.GameId,
                Score = CalculateScore(game, answers),
                UserInfoId = Guid.Parse(userId)
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

        public async Task<PostGameViewModel> CalculateScore(GameAnswersViewModel answers)
        {
            var userInfoRepository = new UserInfoRepository();
            var userGuid = Guid.NewGuid();
            await userInfoRepository.CreateUserInfoFromUser(userGuid.ToString(), answers.Username);
            var newUser = userInfoRepository.GetById(userGuid);

            var game = _gameRepository.GetById(Guid.Parse(answers.GameId));
            var playedGame = new PlayedGames()
            {
                PlayedGameId = Guid.NewGuid(),
                DatePlayed = DateTime.Now,
                GameId = game.GameId,
                Score = CalculateScore(game, answers),
                UserInfoId = newUser.UserInfoId
            };

            Insert(playedGame);

            newUser.TotalScore += playedGame.Score;

            if (newUser.HighScore < playedGame.Score)
            {
                newUser.HighScore = playedGame.Score;
            }

            userInfoRepository.Update(newUser);

            var usersAchievements = await userInfoRepository.GetEarnedAchievements(newUser.UserInfoId.ToString());
            var achievements = await GetNewAchievements(usersAchievements, playedGame.Score, newUser.UserInfoId);

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
                await CheckIfAchievementNew(usersAchievements, userGuid, Constants.MoreThan90Id, achievements);
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

            var tempScore = GetTempScore(game.Question1Id, answers, 0);
            score += tempScore;
            tempScore = GetTempScore(game.Question2Id, answers, 1);
            score += tempScore;
            tempScore = GetTempScore(game.Question3Id, answers, 2);
            score += tempScore;
            tempScore = GetTempScore(game.Question4Id, answers, 3);
            score += tempScore;
            tempScore = GetTempScore(game.Question5Id, answers, 4);
            score += tempScore;
            tempScore = GetTempScore(game.Question6Id, answers, 5);
            score += tempScore;
            tempScore = GetTempScore(game.Question7Id, answers, 6);
            score += tempScore;
            tempScore = GetTempScore(game.Question8Id, answers, 7);
            score += tempScore;
            tempScore = GetTempScore(game.Question9Id, answers, 8);
            score += tempScore;
            tempScore = GetTempScore(game.Question10Id, answers, 9);
            score += tempScore;

            return score;
        }

        private static double GetTempScore(Guid questionId, GameAnswersViewModel answers, int questionIndex)
        {
            var _questionRepository = new QuestionRepository();
            var question = _questionRepository.GetById(questionId);

            var tempScore =
                CostFunction(DegreeDifferenceToKmDifference(question.Latitude, question.Longitude,
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
            return QuestionPoints / (Math.Cosh(CostFactor*x));
        }

        private static double DegreeDifferenceToKmDifference(double gameLatitude, double gameLongitude,
            double answerLatitude, double answerLongitude)
        {
            return Math.Sqrt(Math.Pow((gameLatitude - answerLatitude)*DegreeToKm, 2)
                           + Math.Pow((gameLongitude - answerLongitude)*DegreeToKm, 2));
        }
    }
}