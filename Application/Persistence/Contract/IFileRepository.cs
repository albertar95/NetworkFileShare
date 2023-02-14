using Application.Persistence.Contract.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence.Contract
{
    public interface IFileRepository : IBaseRepository
    {
        Task<Domain.File> GetFile(Guid id,bool IncludeAll = true);
        Task<List<Domain.File>> GetFiles(Guid FolderId);
    }
}
