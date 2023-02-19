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

        public async Task<List<Folder>> GetFolders(Guid UserId, bool IncludePublics = false, string FolderIconId = "", string FolderName = "")
        {
            List<Folder> result = new List<Folder>();
            if(string.IsNullOrWhiteSpace(FolderIconId) && string.IsNullOrWhiteSpace(FolderName))
            {
                result.AddRange(await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
    .Include(p => p.FolderType).Include(p => p.User).Where(p => p.UserId == UserId).ToListAsync());
                //add public folders
                if (IncludePublics)
                {
                    result.AddRange(_context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                    .Include(p => p.FolderType).Include(p => p.User).Where(p => p.AccessLevel.Name == "public" && p.UserId != UserId));
                }
            }
            else if (string.IsNullOrWhiteSpace(FolderIconId) && !string.IsNullOrWhiteSpace(FolderName))
            {
                result.AddRange(await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
    .Include(p => p.FolderType).Include(p => p.User)
    .Where(p => p.UserId == UserId && p.Name.Contains(FolderName)).ToListAsync());
                //add public folders
                if (IncludePublics)
                {
                    result.AddRange(_context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                    .Include(p => p.FolderType).Include(p => p.User)
                    .Where(p => p.AccessLevel.Name == "public" && p.UserId != UserId && p.Name.Contains(FolderName)));
                }
            }
            else if (!string.IsNullOrWhiteSpace(FolderIconId) && string.IsNullOrWhiteSpace(FolderName))
            {
                result.AddRange(await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
    .Include(p => p.FolderType).Include(p => p.User)
    .Where(p => p.UserId == UserId && p.FolderIconId.ToString() == FolderIconId).ToListAsync());
                //add public folders
                if (IncludePublics)
                {
                    result.AddRange(_context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                    .Include(p => p.FolderType).Include(p => p.User)
                    .Where(p => p.AccessLevel.Name == "public" && p.UserId != UserId && p.FolderIconId.ToString() == FolderIconId));
                }
            }
            else if (!string.IsNullOrWhiteSpace(FolderIconId) && !string.IsNullOrWhiteSpace(FolderName))
            {
                result.AddRange(await _context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
    .Include(p => p.FolderType).Include(p => p.User)
    .Where(p => p.UserId == UserId && p.FolderIconId.ToString() == FolderIconId && p.Name.Contains(FolderName)).ToListAsync());
                //add public folders
                if (IncludePublics)
                {
                    result.AddRange(_context.Folders.Include(p => p.AccessLevel).Include(p => p.FolderColor).Include(p => p.FolderIcon)
                    .Include(p => p.FolderType).Include(p => p.User)
                    .Where(p => p.AccessLevel.Name == "public" && p.UserId != UserId && p.FolderIconId.ToString() == FolderIconId && p.Name.Contains(FolderName)));
                }
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

        public async Task<List<SubFolder>> GetSubFolders(Guid RootFolderId, Guid? ParentFolderId)
        {
            if (ParentFolderId == null)
                return await _context.SubFolders.Where(p => p.RootFolderId == RootFolderId).ToListAsync();
            else
                return await _context.SubFolders.Where(p => p.RootFolderId == RootFolderId && p.ParentFolderId == ParentFolderId).ToListAsync();
        }
    }
}
