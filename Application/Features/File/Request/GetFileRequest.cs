using Application.DTO.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Request
{
    public class GetFileRequest : IRequest<FileDTO>
    {
        public Guid Id { get; set; }
    }
}
