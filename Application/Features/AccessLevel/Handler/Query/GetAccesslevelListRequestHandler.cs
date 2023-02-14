using Application.DTO.AccessLevel;
using Application.Features.AccessLevel.Request;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccessLevel.Handler.Query
{
    public class GetAccesslevelListRequestHandler : IRequestHandler<GetAccesslevelListRequest,List<AccessLevelDTO>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetAccesslevelListRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }

        public async Task<List<AccessLevelDTO>> Handle(GetAccesslevelListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<AccessLevelDTO>>(await _folderRepository.GetAccessLevels());
        }
    }
}
