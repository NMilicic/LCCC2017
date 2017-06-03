using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.ViewModels.AddViewModels
{
    public class AddQuestionViewModel
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public string Info { get; set; }
        public string FirstHint { get; set; }
        public string SecondHint { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}