using Application.DTO.FolderColor;
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
    public class GetFolderColorListRequestHandler : IRequestHandler<GetFolderColorListRequest, List<FolderColorDTO>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetFolderColorListRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<List<FolderColorDTO>> Handle(GetFolderColorListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FolderColorDTO>>(await _folderRepository.GetFolderColors());
        }
    }
}
