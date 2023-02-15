using Application.Features.File.Request;
using Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Hanlder.Query
{
    public class GetFileNavRequestHandler : IRequestHandler<GetFileNavRequest, string[]>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileNavRequestHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<string[]> Handle(GetFileNavRequest request, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetFileNavigation(request.Id);
        }
    }
}
