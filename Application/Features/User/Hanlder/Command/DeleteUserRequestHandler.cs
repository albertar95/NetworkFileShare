using Application.Features.User.Request;
using Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Hanlder.Command
{
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFolderRepository _folderRepository;

        public DeleteUserRequestHandler(IUserRepository userRepository,IFolderRepository folderRepository)
        {
            _userRepository = userRepository;
            _folderRepository = folderRepository;
        }
        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id);
            if (user == null) return false;
            else
            {
                var result = await _userRepository.Delete(user);
                var foldertypes = await _folderRepository.GetFolderTypes();
                if(result)
                {
                    foreach (var item in await _folderRepository.GetFolders(user.Id,false))
                    {
                        if(item.FolderTypeId == foldertypes.FirstOrDefault(p => p.Name.ToLower() == "server").Id)
                            Helpers.DirectoryHelper.DeleteVirtualDirectory(item.Path, item.Id.ToString());
                        else
                            Helpers.DirectoryHelper.Remove(item.Path);
                    }
                }  
                return result;

            }
        }
    }
}
