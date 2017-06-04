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
using FindIt.ViewModels;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Controllers
{
    [RoutePrefix("api/questions")]
    public class QuestionsController : ApiController
    {

        private readonly IQuestionRepository _questionRepository = new QuestionRepository();

        [HttpGet]
        public async Task<IEnumerable<QuestionViewModel>> Get()
        {
            return await _questionRepository.GetQuestions();
        }

        // GET api/<controller>/5
        [HttpGet]
        public QuestionViewModel Get([FromUri] string questionId)
        {
            return _questionRepository.GetQuestionById(questionId);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] AddQuestionViewModel model)
        {
            _questionRepository.Insert(model);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(string id, [FromBody] Questions model)
        {
            _questionRepository.Update(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(string id)
        {
            _questionRepository.Delete(Guid.Parse(id));
        }
    }
}