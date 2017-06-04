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
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Controllers
{
    [RoutePrefix("api/skills")]
    public class SkillsController : ApiController
    {

        private readonly ISkillRepository _skillRepository = new SkillRepository();

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Skills>> Get()
        {
            return await _skillRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet]
        public Skills Get([FromUri] string id)
        {
            return _skillRepository.GetById(Guid.Parse(id));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]AddSkillViewModel model)
        {
            await _skillRepository.Insert(model);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody]Skills model)
        {
            _skillRepository.Update(model);
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            _skillRepository.Delete(Guid.Parse(id));
        }
    }
}