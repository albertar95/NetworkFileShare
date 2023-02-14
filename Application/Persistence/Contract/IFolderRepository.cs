using Application.Persistence.Contract.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence.Contract
{
    public interface IFolderRepository : IBaseRepository
    {
        Task<Folder> GetFolder(Guid id,bool IncludeAll = true);
        Task<List<Folder>> GetFolders(Guid UserId,bool IncludePublics = false,string FolderIconId = "",string FolderName = "");
        Task<List<FolderIcon>> GetFolderIcons();
        Task<List<FolderType>> GetFolderTypes();
        Task<List<FolderColor>> GetFolderColors();
    }
}
