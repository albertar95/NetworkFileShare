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
    public class UpdatePasswordRequestHandler : IRequestHandler<UpdatePasswordRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdatePasswordRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Password.Id);
            if (user == null) return false;
            else
            {
                _mapper.Map(request.Password,user);
                return await _userRepository.Update(user);
            }
        }
    }
}
