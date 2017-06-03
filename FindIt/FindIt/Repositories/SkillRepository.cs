using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;
using FindIt.Repositories.Interfaces;

namespace FindIt.Repositories
{
    public class SkillRepository : BaseRepository<Skills, Guid>, ISkillRepository
    {
    }
}