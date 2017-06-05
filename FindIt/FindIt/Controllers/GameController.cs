using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using FindIt.Models;
using FindIt.Repositories;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels;
using Microsoft.AspNet.Identity;

namespace FindIt.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        
        private readonly IGameRepository _gameRepository = new GameRepository();
        private readonly IPlayedGameRepository _playedGameRepository = new PlayedGameRepository();
        private readonly IChallengeRepository _challengeRepository = new ChallengeRepository();

        [HttpGet]
        [Route("newgame")]
        public async Task<NewGameViewModel> StartNewGame()
        {
            return await _gameRepository.CreateNewGame();
        }

        [HttpGet]
        [Route("challengegame")]
        public NewGameViewModel ChallengeGame(string playedGameId)
        {
            return _challengeRepository.GetChallengeGame(playedGameId);
        }

        [HttpPost]
        [Route("submitgame")]
        public async Task<PostGameViewModel> SubmitGameForEvaluation([FromBody] GameAnswersViewModel model)
        {
            var test = model == null;
            return await _playedGameRepository.CalculateScore(model.GameId, model, 
                this.RequestContext.Principal.Identity.GetUserId());
            
        }



    }
}