using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class GameRepository : BaseRepository<Games, Guid>, IGameRepository
    {

        private readonly IQuestionRepository _questionRepository = new QuestionRepository();
        public const int QuestionsPerGame = 10;

        public async Task<Games> CreateNewGame()
        {
            var questions = await _questionRepository.GetQuestionsForGame(QuestionsPerGame);
            var game = new Games()
            {
                GameId = Guid.NewGuid()
            };

            var noOfQuestions = questions.Count();

            if (noOfQuestions < QuestionsPerGame)
            {
                return null;
            }

            game.Question1Id = questions.ElementAt(0).QuestionId;
            game.Question2Id = questions.ElementAt(1).QuestionId;
            game.Question3Id = questions.ElementAt(2).QuestionId;
            game.Question4Id = questions.ElementAt(3).QuestionId;
            game.Question5Id = questions.ElementAt(4).QuestionId;
            game.Question6Id = questions.ElementAt(5).QuestionId;
            game.Question7Id = questions.ElementAt(6).QuestionId;
            game.Question8Id = questions.ElementAt(7).QuestionId;
            game.Question9Id = questions.ElementAt(8).QuestionId;
            game.Question10Id = questions.ElementAt(9).QuestionId;

            Insert(game);

            return game;
        }
    }
}