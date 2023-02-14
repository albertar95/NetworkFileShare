using Application.Features.Folder.Request;
using Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Hanlder.Command
{
    public class DeleteFolderRequestHandler : IRequestHandler<DeleteFolderRequest, bool>
    {
        private readonly IFolderRepository _folderRepository;

        public DeleteFolderRequestHandler(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<bool> Handle(DeleteFolderRequest request, CancellationToken cancellationToken)
        {
            var folder = await _folderRepository.GetFolder(request.Id,false);
            if (folder == null) return false;
            else
            {
                var result = await _folderRepository.Delete(folder);
                var foldertypes = await _folderRepository.GetFolderTypes();
                if (result)
                {
                    if(folder.FolderTypeId == foldertypes.FirstOrDefault(p => p.Name.ToLower() == "server").Id)
                        Helpers.DirectoryHelper.DeleteVirtualDirectory(folder.Path, folder.Id.ToString());
                    else
                        Helpers.DirectoryHelper.Remove(folder.Path);
                }
                return result;
            }
        }
    }
}
