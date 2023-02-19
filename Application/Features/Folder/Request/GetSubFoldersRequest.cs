using Application.DTO.SubFolder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Request
{
    public class GetSubFoldersRequest : IRequest<List<SubFolderDTO>>
    {
        public Guid RootFolderId { get; set; }
        public Guid? ParentFolderId { get; set; }
    }
}
