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

namespace FindIt.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserInfoRepository _userInfoRepository = new UserInfoRepository();

        [HttpGet]
        public async Task<IEnumerable<UserInfo>> Get()
        {
            return await _userInfoRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet]
        public UserInfo Get(string id)
        {
            return _userInfoRepository.GetById(Guid.Parse(id));
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