using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories
{
    public class QuestionRepository : BaseRepository<Questions, Guid>, IQuestionRepository
    {

        public const int QuestionsPerGame = 10;

        public async Task<IEnumerable<Questions>> GetQuestionsForGame(int questionsPerGame = QuestionsPerGame)
        {
            var random = new Random();
            var questions = await GetAll();

            if (questions.Count <= questionsPerGame)
            {
                return questions;
            }


            var gameQuestions = new List<Questions>();
            var insertedQuestions = new HashSet<int>();

            while (gameQuestions.Count < questionsPerGame)
            {
                var index = random.Next(0, questions.Count);

                if (insertedQuestions.Add(index))
                {
                    var question = questions.ElementAt(index);
                    gameQuestions.Add(question);
                }
            }

            return gameQuestions;
        }

        public void Insert(AddQuestionViewModel model)
        {
            Insert(new Questions(model));
        }

        public async Task<IEnumerable<QuestionViewModel>> GetQuestions()
        {
            var questions = await GetAll();
            var result = new List<QuestionViewModel>();

            foreach (var question in questions)
            {
                result.Add(new QuestionViewModel(question));
            }

            return result;
        }

        public QuestionViewModel GetQuestionById(string questionId)
        {
            return new QuestionViewModel(GetById(Guid.Parse(questionId)));
        }
    }
}