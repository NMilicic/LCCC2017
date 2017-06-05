using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;
using FindIt.ViewModels;

namespace FindIt.Repositories.Interfaces
{
    public interface IPlayedGameRepository : IRepository<PlayedGames, Guid>
    {
        Task<PostGameViewModel> CalculateScore(string gameId, GameAnswersViewModel answers, string userId);
    }
}
