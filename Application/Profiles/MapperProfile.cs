using Application.DTO.AccessLevel;
using Application.DTO.File;
using Application.DTO.Folder;
using Application.DTO.FolderColor;
using Application.DTO.FolderIcon;
using Application.DTO.FolderType;
using Application.DTO.SubFolder;
using Application.DTO.SubFolderFile;
using Application.DTO.User;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //users
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>();
            CreateMap<CreateUserDTO, User>().BeforeMap((s,d) => { d.CreateDate = DateTime.Now;d.Id = Guid.NewGuid(); });
            CreateMap<User, UpdateUserDTO>();
            CreateMap<UpdateUserDTO, User>().BeforeMap((s,d) => { d.LastModified = DateTime.Now; });
            CreateMap<User, UpdateUserPasswordDTO>();
            CreateMap<UpdateUserPasswordDTO, User>().BeforeMap((s, d) => { d.LastModified = DateTime.Now; });
            //folders
            CreateMap<Folder, FolderDTO>().BeforeMap((s,d) => { d.AccessLevelName = s.AccessLevel.Name;d.Username = s.User.Username;
                d.FolderColorName = s.FolderColor.ColorCode;d.FolderIconName = s.FolderIcon.IconCode;d.FolderTypeName = s.FolderType.Name; });
            CreateMap<FolderDTO, Folder>();
            CreateMap<Folder, CreateFolderDTO>();
            CreateMap<CreateFolderDTO, Folder>().BeforeMap((s, d) => { d.CreateDate = DateTime.Now; d.Id = Guid.NewGuid(); });
            CreateMap<Folder, UpdateFolderDTO>();
            CreateMap<UpdateFolderDTO, Folder>().BeforeMap((s, d) => { d.LastModified = DateTime.Now; });
            //files
            CreateMap<Domain.File, FileDTO>().BeforeMap((s,d) => { d.AccessLevelName = s.AccessLevel.Name;d.FolderName = s.Folder.Name;d.FolderPath = s.Folder.Path;d.FolderTypeName = s.Folder.FolderType.Name; });
            CreateMap<FileDTO, Domain.File>();
            CreateMap<Domain.File, CreateFileDTO>();
            CreateMap<CreateFileDTO, Domain.File>().BeforeMap((s, d) => { d.CreateDate = DateTime.Now; d.Id = Guid.NewGuid(); });
            //sides
            CreateMap<FolderColor,FolderColorDTO>().ReverseMap();
            CreateMap<FolderIcon, FolderIconDTO>().ReverseMap();
            CreateMap<FolderType, FolderTypeDTO>().ReverseMap();
            CreateMap<AccessLevel, AccessLevelDTO>().ReverseMap();
            //sub folders
            CreateMap<SubFolder,SubFolderDTO>().ReverseMap();
            CreateMap<SubFolder, CreateSubFolderDTO>();
            CreateMap<CreateSubFolderDTO, SubFolder>().BeforeMap((s,d) => { d.CreateDate = DateTime.Now; d.Id = Guid.NewGuid(); });
            //sub folder files
            CreateMap<SubFolderFile, SubFolderFileDTO>().ReverseMap();
            CreateMap<SubFolderFile, CreateSubFolderFileDTO>();
            CreateMap<CreateSubFolderFileDTO, SubFolderFile>().BeforeMap((s, d) => { d.CreateDate = DateTime.Now; d.Id = Guid.NewGuid(); });
        }
    }
}
