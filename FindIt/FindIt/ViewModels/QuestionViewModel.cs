using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;

namespace FindIt.ViewModels
{
    public class QuestionViewModel
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public string Info { get; set; }
        public string FirstHint { get; set; }
        public string SecondHint { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public QuestionViewModel(Questions question)
        {
            Name = question.Name;
            ImageUri = question.ImageUri;
            Info = question.Info;
            FirstHint = question.FirstHint;
            SecondHint = question.SecondHint;
            Latitude = question.Latitude;
            Longitude = question.Longitude;
        }
    }
}