using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories
{
    public class AchievementRepository : BaseRepository<Achievements, Guid>, IAchievementRepository
    {
        public void Insert(AddAchievementViewModel model)
        {
            Insert(new Achievements(model));
        }
    }
}