using Application.Features.File.Request;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Hanlder.Command
{
    public class CreateFileRequestHandler : IRequestHandler<CreateFileRequest,bool>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public CreateFileRequestHandler(IFileRepository fileRepository,IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateFileRequest request, CancellationToken cancellationToken)
        {
            return await _fileRepository.Add<Domain.File>(_mapper.Map<Domain.File>(request.File));
        }
    }
}
