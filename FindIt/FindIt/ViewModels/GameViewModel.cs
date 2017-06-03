using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;

namespace FindIt.ViewModels
{
    public class GameViewModel
    {
        public string GameId { get; set; }
        public GameAnswersViewModel Answers { get; set; }
    }
}