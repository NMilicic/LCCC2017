using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Models
{
    public partial class Questions
    {
        public Questions(AddQuestionViewModel model)
        {
            QuestionId = Guid.NewGuid();
            Name = model.Name;
            ImageUri = model.ImageUri;
            FirstHint = model.FirstHint;
            Info = model.Info;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            SecondHint = model.SecondHint;
        }
    }
}