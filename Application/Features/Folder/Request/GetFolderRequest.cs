using Application.DTO.Folder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Request
{
    public class GetFolderRequest : IRequest<FolderDTO>
    {
        public Guid Id { get; set; }
    }
}
