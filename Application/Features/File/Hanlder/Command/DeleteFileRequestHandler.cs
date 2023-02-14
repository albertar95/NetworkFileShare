using Application.Features.File.Request;
using Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Hanlder.Command
{
    public class DeleteFileRequestHandler : IRequestHandler<DeleteFileRequest,bool>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFolderRepository _folderRepository;

        public DeleteFileRequestHandler(IFileRepository fileRepository,IFolderRepository folderRepository)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;
        }

        public async Task<bool> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFile(request.Id,false);
            if (file == null) return false;
            else
            {
                var tmpfolder = await _folderRepository.GetFolder(file.FolderId);
                var foldertypes = await _folderRepository.GetFolderTypes();
                if (tmpfolder.FolderTypeId == foldertypes.FirstOrDefault(p => p.Name.ToLower() == "simple").Id)
                {
                    Helpers.DirectoryHelper.RemoveFile(Path.Combine(tmpfolder.Path, file.Name));
                    return await _fileRepository.Delete<Domain.File>(file);
                }
                else return false;
            }
        }
    }
}
