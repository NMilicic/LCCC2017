using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories
{
    public class AchievementRepository : BaseRepository<Achievements, Guid>, IAchievementRepository
    {
        private readonly UserInfoAchievementsRepository _userAchievements = new UserInfoAchievementsRepository();

        public async Task Insert(AddAchievementViewModel model)
        {
            var achievement = new Achievements(model);
            Insert(achievement);

            var users = await (new UserInfoRepository()).GetAll();
            _userAchievements.AddNewAchievementToUsers(achievement, users);
        }
    }
}