using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories.Interfaces
{
    public interface IAchievementRepository : IRepository<Achievements, Guid>
    {
        Task Insert(AddAchievementViewModel model);
    }
}
