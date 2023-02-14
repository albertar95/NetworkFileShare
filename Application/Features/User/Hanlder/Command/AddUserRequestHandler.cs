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
    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            return await _userRepository.Add(_mapper.Map<Domain.User>(request.User));
        }
    }
}
