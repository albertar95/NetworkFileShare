using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FolderColor : BaseProperties
    {
        public string ColorCode { get; set; }
        public virtual List<Folder> Folders { get; set; }
    }
}
