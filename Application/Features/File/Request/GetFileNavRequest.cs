using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File.Request
{
    public class GetFileNavRequest : IRequest<string[]>
    {
        public Guid Id { get; set; }
    }
}
