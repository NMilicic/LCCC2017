using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

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
    }
}