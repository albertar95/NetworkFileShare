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
    public class GetFolderRequestHandler : IRequestHandler<GetFolderRequest, FolderDTO>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public GetFolderRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<FolderDTO> Handle(GetFolderRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<FolderDTO>(await _folderRepository.GetFolder(request.Id));
        }
    }
}
