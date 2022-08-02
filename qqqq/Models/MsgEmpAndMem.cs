using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class MsgEmpAndMem
    {
        public int MsgEmpAndMem1 { get; set; }
        public int? EmployeeId { get; set; }
        public int? MemberId { get; set; }
        public bool? IsMemSend { get; set; }
        public string Mseeage { get; set; }
        public DateTime? MsgTime { get; set; }
        public bool? IsReceiveRead { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Member Member { get; set; }
    }
}
