using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class File : BaseProperties
    {
        public string Name { get; set; }
        public string FileExt { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid FolderId { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
