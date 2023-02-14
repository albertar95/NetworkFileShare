using Application.DTO.FolderType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Request
{
    public class GetFolderTypeListRequest : IRequest<List<FolderTypeDTO>>
    {
    }
}
