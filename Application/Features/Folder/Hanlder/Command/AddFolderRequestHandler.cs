using Application.Features.Folder.Request;
using Application.Helpers;
using Application.Persistence.Contract;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
                SubFolder tmpSubFolder = null;
                var result = await _folderRepository.Add(tmpFolder);
                if(result)
                {
                    foreach (var newfile in DirectoryHelper.ProbeFolder(tmpFolder.Id,tmpFolder.AccessLevelId,tmpFolder.Path))
                    {
                        await _folderRepository.Add<Domain.File>(_mapper.Map<Domain.File>(newfile));
                    }
                    DirectoryHelper.CreateVirtualDirectory(tmpFolder.Path, tmpFolder.Id.ToString());
                    foreach (var newsubfolder in DirectoryHelper.ProbeSubFolder(tmpFolder.Id, tmpFolder.Path))
                    {
                        tmpSubFolder = _mapper.Map<Domain.SubFolder>(newsubfolder);
                        if(await _folderRepository.Add<Domain.SubFolder>(tmpSubFolder))
                        {
                            foreach (var newfile in DirectoryHelper.ProbeSubFolderFiles(tmpSubFolder.Id, tmpSubFolder.Path))
                            {
                                await _folderRepository.Add<Domain.SubFolderFile>(_mapper.Map<Domain.SubFolderFile>(newfile));
                            }
                            await AddSubFolderSubs(tmpSubFolder);
                        }
                    }
                }
                return result;
            }
            else
            {
                request.Folder.Path = Helpers.DirectoryHelper.Generate();
                return await _folderRepository.Add(_mapper.Map<Domain.Folder>(request.Folder));
            }
            
        }
        private async Task<bool> AddSubFolderSubs(SubFolder subFolder) 
        {
            foreach (var item in DirectoryHelper.ProbeSubFolderSubs(subFolder.RootFolderId,subFolder.Id,subFolder.Path))
            {
                var tmpSub = _mapper.Map<Domain.SubFolder>(item);
                if(await _folderRepository.Add<Domain.SubFolder>(tmpSub))
                {
                    foreach (var newfile in DirectoryHelper.ProbeSubFolderFiles(tmpSub.Id, tmpSub.Path))
                    {
                        await _folderRepository.Add<Domain.SubFolderFile>(_mapper.Map<Domain.SubFolderFile>(newfile));
                    }
                    await AddSubFolderSubs(tmpSub);
                }
            }
            return true;
        }
    }
}
