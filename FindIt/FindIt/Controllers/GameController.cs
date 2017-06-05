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
    //[EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
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

        [HttpGet]
        [Route("submitgame")]
        public async Task<PostGameViewModel> SubmitGameForEvaluationTest([FromUri] GameAnswersViewModel model)
        {
            return await _playedGameRepository.CalculateScore(model);
        }

        [HttpPost]
        [Route("submitgame")]
        public async Task<PostGameViewModel> SubmitGameForEvaluation([FromBody] GameAnswersViewModel model)
        {
            return await _playedGameRepository.CalculateScore(model);
            //return await _playedGameRepository.CalculateScore(model, "2ABBCA73-1E06-4F1F-9105-71B97E84451A");
            //this.RequestContext.Principal.Identity.GetUserId());

        }



    }
}