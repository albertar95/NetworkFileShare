using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Folder : BaseProperties
    {
        public string Name { get; set; }
        public Guid FolderTypeId { get; set; }
        public string Path { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid FolderColorId { get; set; }
        public Guid FolderIconId { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual FolderType FolderType { get; set; }
        public virtual FolderColor FolderColor { get; set; }
        public virtual FolderIcon FolderIcon { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
