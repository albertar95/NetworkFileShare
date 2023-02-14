using Application.Persistence.Contract;
using Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Repository
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        private readonly NetworkFileDbContext _context;

        public FileRepository(NetworkFileDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Domain.File> GetFile(Guid id, bool IncludeAll = true)
        {
            if(IncludeAll)
                return await _context.Files.Include(p => p.Folder).ThenInclude(q => q.FolderType).Include(p => p.AccessLevel).FirstOrDefaultAsync(p => p.Id == id);
            else
                return await _context.Files.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Domain.File>> GetFiles(Guid FolderId)
        {
            return await _context.Files.Include(p => p.Folder).ThenInclude(q => q.FolderType).Include(p => p.AccessLevel).Where(p => p.FolderId == FolderId).ToListAsync();
        }
    }
}
