using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.File
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string Name { get; set; }
        public string? FileLength { get; set; }
        public string FileExt { get; set; }
        public Guid FolderId { get; set; }
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string FolderTypeName { get; set; }
        public Guid AccessLevelId { get; set; }
        public string AccessLevelName { get; set; }
    }
}
