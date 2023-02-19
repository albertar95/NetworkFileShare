using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.SubFolder
{
    public class CreateSubFolderDTO
    {
        public Guid RootFolderId { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
