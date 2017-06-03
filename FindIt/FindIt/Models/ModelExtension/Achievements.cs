using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Models
{
    public partial class Achievements
    {
        public Achievements(AddAchievementViewModel model)
        {
            AchievementId = Guid.NewGuid();
            Name = model.Name;
            Description = model.Description;
            ImageUri = model.ImageUri;
        }
    }
}