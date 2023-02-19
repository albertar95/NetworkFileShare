using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SubFolder : BaseProperties
    {
        public Guid RootFolderId { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual Folder Folder { get; set; }
        public virtual ICollection<SubFolderFile> SubFolderFiles { get; set; }
    }
}
