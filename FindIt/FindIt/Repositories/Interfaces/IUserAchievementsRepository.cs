using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;

namespace FindIt.Repositories.Interfaces
{
    public interface IUserAchievementsRepository : IRepository<UserAchievements, Guid>
    {

        void AddAchievementsToNewUser(UserInfo user, IEnumerable<Achievements> achievements);
        void AddNewAchievementToUsers(Achievements achievement, IEnumerable<UserInfo> users);
        Task SetAchievement(Guid achievementGuid, Guid userGuid);
    }
}
