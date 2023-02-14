using Application.Features.Folder.Request;
using Application.Persistence.Contract;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Hanlder.Command
{
    public class UpdateFolderRequestHandler : IRequestHandler<UpdateFolderRequest, bool>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;

        public UpdateFolderRequestHandler(IFolderRepository folderRepository, IMapper mapper)
        {
            _folderRepository = folderRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateFolderRequest request, CancellationToken cancellationToken)
        {
            var folder = await _folderRepository.GetFolder(request.Folder.Id,false);
            if (folder == null) return false;
            else
            {
                _mapper.Map(request.Folder,folder);
            }
            return await _folderRepository.Update<Domain.Folder>(folder);
        }
    }
}
