using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Request
{
    public class GetUserRequest : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}
