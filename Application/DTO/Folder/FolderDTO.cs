using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Folder
{
    public class FolderDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid FolderTypeId { get; set; }
        public string? FolderTypeName { get; set; }
        public Guid AccessLevelId { get; set; }
        public string? AccessLevelName { get; set; }
        public Guid FolderColorId { get; set; }
        public string? FolderColorName { get; set; }
        public Guid FolderIconId { get; set; }
        public string? FolderIconName { get; set; }
        public Guid UserId { get; set; }
        public string? Username { get; set; }
    }
}
