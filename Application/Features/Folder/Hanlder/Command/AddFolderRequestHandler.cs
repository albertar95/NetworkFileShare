using Application.Features.Folder.Request;
using Application.Helpers;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Hanlder.Command
{
    public class AddFolderRequestHandler : IRequestHandler<AddFolderRequest, bool>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public AddFolderRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddFolderRequest request, CancellationToken cancellationToken)
        {
            var tmpFolderTypes = await _folderRepository.GetFolderTypes();
            if (request.Folder.FolderTypeId == tmpFolderTypes.FirstOrDefault(p => p.Name.ToLower() == "server").Id)
            {
                var tmpFolder = _mapper.Map<Domain.Folder>(request.Folder);
                var result = await _folderRepository.Add(tmpFolder);
                if(result)
                {
                    foreach (var newfile in DirectoryHelper.ProbeFolder(tmpFolder.Id,tmpFolder.AccessLevelId,tmpFolder.Path))
                    {
                        await _folderRepository.Add<Domain.File>(_mapper.Map<Domain.File>(newfile));
                    }
                    DirectoryHelper.CreateVirtualDirectory(tmpFolder.Path, tmpFolder.Id.ToString());
                }
                return result;
            }
            else
            {
                request.Folder.Path = Helpers.DirectoryHelper.Generate();
                return await _folderRepository.Add(_mapper.Map<Domain.Folder>(request.Folder));
            }
            
        }
    }
}
