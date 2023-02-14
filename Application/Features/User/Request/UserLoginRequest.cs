using Application.DTO.User;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Request
{
    public class UserLoginRequest : IRequest<Tuple<byte,UserDTO>>
    {
        public LoginCredential Credential { get; set; }
    }
}
