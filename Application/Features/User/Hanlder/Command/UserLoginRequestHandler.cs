using Application.DTO.User;
using Application.Features.User.Request;
using Application.Persistence.Contract;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Hanlder.Command
{
    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, Tuple<byte, UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserLoginRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Tuple<byte, UserDTO>> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.LoginUser(request.Credential.Username,request.Credential.Password);
            return new Tuple<byte, UserDTO>(result.Item1,_mapper.Map<UserDTO>(result.Item2));
        }
    }
}
