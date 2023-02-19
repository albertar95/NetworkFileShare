using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.SubFolderFile
{
    public class SubFolderFileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileExt { get; set; }
        public string? FileLength { get; set; }
        public Guid SubFolderId { get; set; }
    }
}
