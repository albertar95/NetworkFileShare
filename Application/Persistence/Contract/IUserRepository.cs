using Application.Persistence.Contract.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence.Contract
{
    public interface IUserRepository : IBaseRepository
    {
        Task<List<User>> GetUsers(bool includeAll = false);
        Task<User> GetUser(Guid id);
        Task<Tuple<byte,Domain.User>> LoginUser(string username, string password);
    }
}
