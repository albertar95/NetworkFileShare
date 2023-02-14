using Application.DTO.Folder;
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
    public class GetFolderListRequestHandler : IRequestHandler<GetFolderListRequest, List<FolderDTO>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetFolderListRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<List<FolderDTO>> Handle(GetFolderListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FolderDTO>>(await _folderRepository.GetFolders(request.UserId,request.IncludePublics,request.FolderIconFilter,request.FolderNameFilter));
        }
    }
}
