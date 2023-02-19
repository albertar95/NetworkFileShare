using Application.DTO.SubFolder;
using Application.Features.Folder.Request;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Hanlder.Query
{
    public class GetSubFoldersRequestHandler : IRequestHandler<GetSubFoldersRequest, List<SubFolderDTO>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetSubFoldersRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<List<SubFolderDTO>> Handle(GetSubFoldersRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<SubFolderDTO>>(await _folderRepository.GetSubFolders(request.RootFolderId, request.ParentFolderId));
        }
    }
}
