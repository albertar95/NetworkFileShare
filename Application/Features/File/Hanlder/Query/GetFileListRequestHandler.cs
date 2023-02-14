using Application.DTO.File;
using Application.Features.File.Request;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Hanlder.Query
{
    public class GetFileListRequestHandler : IRequestHandler<GetFileListRequest, List<FileDTO>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileListRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<List<FileDTO>> Handle(GetFileListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FileDTO>>(await _fileRepository.GetFiles(request.FolderId));
        }
    }
}
