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

namespace Application.Features.User.Hanlder.Query
{
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserListRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<UserDTO>>(await _userRepository.GetUsers(request.IncludeDisabled));
        }
    }
}
