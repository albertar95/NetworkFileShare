using Application.DTO.AccessLevel;
using Application.DTO.Folder;
using Application.DTO.FolderColor;
using Application.DTO.FolderIcon;
using Application.DTO.FolderType;

namespace NetworkFileShareUI.ViewModels
{
    public class UpdateFolderViewModel
    {
        public FolderDTO Folder { get; set; }
        public List<AccessLevelDTO> AccessLevels { get; set; }
        public List<FolderColorDTO> FolderColors { get; set; }
        public List<FolderIconDTO> FolderIcons { get; set; }
    }
}
