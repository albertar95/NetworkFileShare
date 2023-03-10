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

        public async Task<string[]> GetFileNavigation(Guid Id)
        {
            var file = _context.Files.FirstOrDefault(p => p.Id == Id);
            if (file == null) return null;
            var Filelist = await _context.Files.Where(p => p.FolderId == file.FolderId && p.FileExt.ToLower() != ".config").OrderByDescending(p => p.CreateDate).Select(p => new { Id = p.Id,createdate = p.CreateDate }).ToArrayAsync();
            int FileIndex = 0;
            for (int i = 0; i < Filelist.Length; i++)
            {
                if (Filelist[i].Id == file.Id)
                    FileIndex = i;
            }
            if(FileIndex > 0)
            {
                if (FileIndex == Filelist.Length - 1)
                    return new string[] { Filelist[FileIndex - 1].Id.ToString(), "" };
                else
                    return new string[] { Filelist[FileIndex - 1].Id.ToString(), Filelist[FileIndex + 1].Id.ToString() };
            }
            else
                return new string[] { "", Filelist[FileIndex + 1].Id.ToString() };
        }

        public async Task<List<Domain.File>> GetFiles(Guid FolderId)
        {
            return await _context.Files.Include(p => p.Folder).ThenInclude(q => q.FolderType).Include(p => p.AccessLevel).Where(p => p.FolderId == FolderId).ToListAsync();
        }
    }
}
