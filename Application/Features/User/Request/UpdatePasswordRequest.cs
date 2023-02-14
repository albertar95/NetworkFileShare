using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Request
{
    public class UpdatePasswordRequest : IRequest<bool>
    {
        public UpdateUserPasswordDTO Password { get; set; }
    }
}
