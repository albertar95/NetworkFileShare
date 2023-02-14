using Application.DTO.FolderIcon;
using Application.DTO.FolderType;
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
    public class GetFolderIconListRequestHandler : IRequestHandler<GetFolderIconListRequest, List<FolderIconDTO>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetFolderIconListRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<List<FolderIconDTO>> Handle(GetFolderIconListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FolderIconDTO>>(await _folderRepository.GetFolderIcons());
        }
    }
}
