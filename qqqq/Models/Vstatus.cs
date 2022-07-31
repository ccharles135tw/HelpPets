using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class Vstatus
    {
        public Vstatus()
        {
            Volunteers = new HashSet<Volunteer>();
        }

        public int VstatusId { get; set; }
        public string StatusType { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}
