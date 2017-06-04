using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;

namespace FindIt.Repositories
{
    public class GameRepository : BaseRepository<Games, Guid>, IGameRepository
    {

        private readonly IQuestionRepository _questionRepository = new QuestionRepository();
        public const int QuestionsPerGame = 10;

        public async Task<NewGameViewModel> CreateNewGame()
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

            //game.Questions = _questionRepository.GetById(game.Question1Id);
            //game.Questions2 = _questionRepository.GetById(game.Question2Id);
            //game.Questions3 = _questionRepository.GetById(game.Question3Id);
            //game.Questions4 = _questionRepository.GetById(game.Question4Id);
            //game.Questions5 = _questionRepository.GetById(game.Question5Id);
            //game.Questions6 = _questionRepository.GetById(game.Question6Id);
            //game.Questions7 = _questionRepository.GetById(game.Question7Id);
            //game.Questions8 = _questionRepository.GetById(game.Question8Id);
            //game.Questions9 = _questionRepository.GetById(game.Question9Id);
            //game.Questions1 = _questionRepository.GetById(game.Question10Id);

            return new NewGameViewModel(game.GameId, questions);
        }
    }
}