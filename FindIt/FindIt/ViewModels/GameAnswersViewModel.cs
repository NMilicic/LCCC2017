using System.Collections.Generic;

namespace FindIt.ViewModels
{
    public class GameAnswersViewModel
    {
        //public List<string> QuestionIds { get; set; }
        public string GameId { get; set; }
        public List<double> Latitudes { get; set; }
        public List<double> Longitudes { get; set; }
        public List<bool> Hints1Used { get; set; }
        public List<bool> Hints2Used { get; set; }

        public GameAnswersViewModel()
        {
            
        }
    }
}