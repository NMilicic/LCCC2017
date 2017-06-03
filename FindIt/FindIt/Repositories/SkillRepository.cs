using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;
using FindIt.ViewModels.AddViewModels;

namespace FindIt.Repositories
{
    public class SkillRepository : BaseRepository<Skills, Guid>, ISkillRepository
    {
        private readonly IUserSkillRepository _userSkillRepository = new UserSkillRepository();


        public async Task Insert(AddSkillViewModel model)
        {
            var skill = new Skills(model);
            Insert(skill);

            var users = await (new UserInfoRepository()).GetAll();
            _userSkillRepository.AddNewSkillToUsers(skill, users);
        }
    }
}