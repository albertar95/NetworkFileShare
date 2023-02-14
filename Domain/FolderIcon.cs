using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FolderIcon : BaseProperties
    {
        public string IconCode { get; set; }
        public string? IconName { get; set; }
        public virtual List<Folder> Folders { get; set; }
    }
}
