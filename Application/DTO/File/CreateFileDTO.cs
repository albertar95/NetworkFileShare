using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.File
{
    public class CreateFileDTO
    {
        public string Name { get; set; }
        public string? FileLength { get; set; }
        public string FileExt { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid FolderId { get; set; }
    }
}
