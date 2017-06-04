using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;

namespace FindIt.ViewModels
{
    public class NewGameViewModel
    {
        public Guid GameId { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

        public NewGameViewModel(Guid gameId, IEnumerable<Questions> questions)
        {
            GameId = gameId;
            Questions = new List<QuestionViewModel>();

            foreach (var question in questions)
            {
                Questions.Add(new QuestionViewModel(question));
            }
        }
    }
}