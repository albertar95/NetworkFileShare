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
    public class FolderRepository : BaseRepository, IFolderRepository
    {
        private readonly NetworkFileDbContext _context;

        public FolderRepository(NetworkFileDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Folder> GetFolder(Guid id, bool IncludeAll = true)
        {
            if (IncludeAll)
            {
                return await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                .Include(p => p.FolderType).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
            }
            else
                return await _context.Folders.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Folder>> GetFolders(Guid UserId, bool IncludePublics = false)
        {
            var result = await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                .Include(p => p.FolderType).Include(p => p.User).Where(p => p.UserId == UserId).ToListAsync();
            //add public folders
            if(IncludePublics)
            {
                result.AddRange(_context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                .Include(p => p.FolderType).Include(p => p.User).Where(p => p.AccessLevel.Name == "public" && p.UserId != UserId));
            }
            return result;
        }

        public async Task<List<FolderType>> GetFolderTypes()
        {
            return await _context.FolderTypes.ToListAsync();
        }

        public async Task<List<FolderColor>> GetFolderColors()
        {
            return await _context.FolderColors.ToListAsync();
        }

        public async Task<List<FolderIcon>> GetFolderIcons()
        {
            return await _context.FolderIcons.ToListAsync();
        }
    }
}
