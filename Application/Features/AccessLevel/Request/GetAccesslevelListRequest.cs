using Application.DTO.AccessLevel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccessLevel.Request
{
    public class GetAccesslevelListRequest : IRequest<List<AccessLevelDTO>>
    {
    }
}
