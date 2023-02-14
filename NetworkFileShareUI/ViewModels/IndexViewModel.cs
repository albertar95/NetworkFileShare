using Application.DTO.Folder;

namespace NetworkFileShareUI.ViewModels
{
    public class IndexViewModel
    {
        public List<FolderDTO> Folders { get; set; } = new List<FolderDTO>();
    }
}
