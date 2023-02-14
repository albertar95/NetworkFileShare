using Application.DTO.AccessLevel;
using Application.DTO.FolderColor;
using Application.DTO.FolderIcon;
using Application.DTO.FolderType;

namespace NetworkFileShareUI.ViewModels
{
    public class AddFolderViewModel
    {
        public List<AccessLevelDTO> AccessLevels { get; set; }
        public List<FolderColorDTO> FolderColors { get; set; }
        public List<FolderIconDTO> FolderIcons { get; set; }
        public List<FolderTypeDTO> FolderTypes { get; set; }
    }
}
