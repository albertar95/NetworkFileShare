using Application.DTO.FolderColor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Folder.Request
{
    public class GetFolderColorListRequest : IRequest<List<FolderColorDTO>>
    {
    }
}
