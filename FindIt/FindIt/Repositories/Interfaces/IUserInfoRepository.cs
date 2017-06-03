using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindIt.Models;

namespace FindIt.Repositories.Interfaces
{
    public interface IUserInfoRepository : IRepository<UserInfo, Guid>
    {
        Task CreateUserInfoFromUser(string identityId, string identityUsername);
    }
}
