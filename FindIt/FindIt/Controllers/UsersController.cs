using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using FindIt.Models;
using FindIt.Repositories;
using FindIt.Repositories.Interfaces;
using Microsoft.AspNet.Identity;

namespace FindIt.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserInfoRepository _userInfoRepository = new UserInfoRepository();

        [HttpGet]
        public async Task<IEnumerable<UserInfo>> Get()
        {
            return await _userInfoRepository.GetAll();
        }

        [HttpGet]
        public UserInfo Get([FromUri] string id)
        {
            return _userInfoRepository.GetById(Guid.Parse(id));
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return this.RequestContext.Principal.Identity.GetUserId();
        }

        [HttpGet]
        [Route("earnedachievements")]
        public async Task<IEnumerable<Achievements>> GetEarnedAchievements()
        {
            return await _userInfoRepository.GetEarnedAchievements(this.RequestContext.Principal.Identity.GetUserId());
        }

        [HttpGet]
        [Route("playerposition")]
        public async Task<int> GetPlayersLeaderboardPosition()
        {
            return await _userInfoRepository.GetPlayersLeaderboardPosition(this.RequestContext.Principal.Identity.GetUserId());
        }

        [HttpGet]
        [Route("leaderboard")]
        public async Task<IEnumerable<UserInfo>> GetLeaderBoard()
        {
            return await _userInfoRepository.GetTopPlayers(10);
        }

        [HttpGet]
        [Route("playedgames")]
        public async Task<IEnumerable<PlayedGames>> GetPlayedGames()
        {
            return await _userInfoRepository.GetPlayedGames(this.RequestContext.Principal.Identity.GetUserId());
        }

        [HttpGet]
        [Route("bestgame")]
        public async Task<PlayedGames> GetBestGame()
        {
            return await _userInfoRepository.GetBestGame(this.RequestContext.Principal.Identity.GetUserId());
        }

        [HttpGet]
        [Route("skills")]
        public async Task<IEnumerable<Skills>> GetUserSkills()
        {
            return await _userInfoRepository.GetUserSkills(this.RequestContext.Principal.Identity.GetUserId());
        }

        [HttpPost]
        [Route("activateskill")]
        public async Task<bool> ActivateSkill([FromBody] string skillId)
        {
            return await _userInfoRepository.ActivateSkill(this.RequestContext.Principal.Identity.GetUserId(), skillId);
        }

        // PUT api/<controller>/5
        [HttpPost]
        public void Put(string id, [FromBody] UserInfo userInfo)
        {
            var user = _userInfoRepository.GetById(Guid.Parse(id));

            user.AvatarUri = userInfo.AvatarUri;
            user.Username = userInfo.Username;

            _userInfoRepository.Update(user);
        }

        // DELETE api/<controller>/5

        public void Delete(string id)
        {
            _userInfoRepository.Delete(Guid.Parse(id));
        }
    }
}