using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : BaseProperties
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
    }
}
