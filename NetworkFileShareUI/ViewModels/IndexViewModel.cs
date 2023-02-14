using Application.DTO.Folder;
using Application.DTO.FolderIcon;
using Application.DTO.FolderType;

namespace NetworkFileShareUI.ViewModels
{
    public class IndexViewModel
    {
        public List<FolderDTO> Folders { get; set; } = new List<FolderDTO>();
        public List<FolderIconDTO> FolderIcons { get; set; } = new List<FolderIconDTO>();
    }
}
