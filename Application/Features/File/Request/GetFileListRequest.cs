using Application.DTO.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Request
{
    public class GetFileListRequest : IRequest<List<FileDTO>>
    {
        public Guid FolderId { get; set; }
    }
}
