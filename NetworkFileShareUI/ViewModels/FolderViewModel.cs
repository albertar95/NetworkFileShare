using Application.DTO.AccessLevel;
using Application.DTO.File;
using Application.DTO.Folder;

namespace NetworkFileShareUI.ViewModels
{
    public class FolderViewModel
    {
        public FolderDTO Folder { get; set; }
        public List<FileDTO> Files { get; set; }
        public List<AccessLevelDTO> AccessLevels { get; set; }
    }
}
