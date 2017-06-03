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
        private readonly UserInfoAchievementsRepository _userAchievements = new UserInfoAchievementsRepository();

        public void Insert(AddAchievementViewModel model)
        {
            var achievement = new Achievements(model);
            Insert(achievement);

            _userAchievements.AddNewAchievementToUsers(achievement);
        }
    }
}