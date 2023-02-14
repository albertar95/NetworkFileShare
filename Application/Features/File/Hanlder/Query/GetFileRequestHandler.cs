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
    public class GetFileRequestHandler : IRequestHandler<GetFileRequest, FileDTO>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<FileDTO> Handle(GetFileRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<FileDTO>(await _fileRepository.GetFile(request.Id));
        }
    }
}
