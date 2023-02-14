using Application.Persistence.Contract;
using Domain;
using Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly NetworkFileDbContext _context;

        public UserRepository(NetworkFileDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<User>> GetUsers(bool includeAll = false)
        {
            if (includeAll)
                return await _context.Users.ToListAsync();
            else
                return await _context.Users.Where(p => p.IsDisabled == false).ToListAsync();
        }

        public async Task<Tuple<byte, User>> LoginUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == username);
            if (user == null) return new Tuple<byte, User>(0,null);
            else
            {
                if(user.IsDisabled) return new Tuple<byte, User>(1, null);
                else
                {
                    if (user.Password != password) return new Tuple<byte, User>(2, null);
                    else
                    {
                        user.LastLoginDate= DateTime.Now;
                        _context.Update(user);
                        _context.SaveChanges();
                        return new Tuple<byte, User>(3, user);
                    }
                }
            }
        }
    }
}
