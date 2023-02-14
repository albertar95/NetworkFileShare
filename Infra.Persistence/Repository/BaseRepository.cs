using Application.Persistence.Contract.Common;
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
    public class BaseRepository : IBaseRepository
    {
        private readonly NetworkFileDbContext _context;

        public BaseRepository(NetworkFileDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add<T>(T entity)
        {
            if (entity == null) return false;
            else
            {
                _context.Add(entity);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }

        }

        public async Task<bool> Delete<T>(T entity)
        {
            if (entity == null) return false;
            else
            {
                _context.Remove(entity);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Update<T>(T entity)
        {
            if (entity == null) return false;
            else
            {
                _context.Update(entity);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<List<AccessLevel>> GetAccessLevels()
        {
            return await _context.AccessLevels.ToListAsync();
        }
    }
}
