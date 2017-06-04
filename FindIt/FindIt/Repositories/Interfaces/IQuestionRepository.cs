using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.ViewModels;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories.Interfaces
{
    public interface IQuestionRepository : IRepository<Questions, Guid>
    {
        Task<IEnumerable<Questions>> GetQuestionsForGame(int questionsPerGame);
        void Insert(AddQuestionViewModel model);
        Task<IEnumerable<QuestionViewModel>> GetQuestions();
        QuestionViewModel GetQuestionById(string questionId);
    }
}
