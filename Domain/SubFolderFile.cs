using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SubFolderFile : BaseProperties
    {
        public string Name { get; set; }
        public string FileExt { get; set; }
        public string? FileLength { get; set; }
        public Guid SubFolderId { get; set; }
        public virtual SubFolder SubFolder { get; set; }
    }
}
