using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FindIt.Models;
using FindIt.Repositories;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;
using Microsoft.AspNet.Identity;

namespace FindIt.Controllers
{

    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        
        private readonly IGameRepository _gameRepository = new GameRepository();
        private readonly IPlayedGameRepository _playedGameRepository = new PlayedGameRepository();

        [HttpGet]
        [Route("newgame")]
        public async Task<Games> StartNewGame()
        {
            return await _gameRepository.CreateNewGame();
        }

        [HttpPost]
        [Route("submitgame")]
        public double SubmitGameForEvaluation([FromBody] GameViewModel model)
        {
            var playedGame = _playedGameRepository.CalculateScore(model.GameId, model.Answers, 
                this.RequestContext.Principal.Identity.GetUserId());
            return playedGame.Score;
        }



    }
}