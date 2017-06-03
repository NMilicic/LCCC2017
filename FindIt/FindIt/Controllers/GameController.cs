using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FindIt.Models;
using FindIt.Repositories;
using FindIt.Repositories.Interfaces;

namespace FindIt.Controllers
{

    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        
        private readonly IGameRepository _gameRepository = new GameRepository();

        [HttpGet]
        [Route(Name = "new")]
        public async Task<Games> Get()
        {
            return await _gameRepository.CreateNewGame();
        }

        
    }
}