using Application.DTO.Folder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Request
{
    public class GetFolderListRequest : IRequest<List<FolderDTO>>
    {
        public Guid UserId { get; set; }
        public bool IncludePublics { get; set; }
        public string? FolderIconFilter { get; set; }
        public string? FolderNameFilter { get; set; }
    }
}
