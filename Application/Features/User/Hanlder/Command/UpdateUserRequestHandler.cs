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
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.User.Id);
            if (user == null) return false;
            else
            {
                _mapper.Map(request.User,user);
                return await _userRepository.Update(user);
            }
        }
    }
}
