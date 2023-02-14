using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Folder
{
    public class UpdateFolderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid FolderColorId { get; set; }
        public Guid FolderIconId { get; set; }
    }
}
