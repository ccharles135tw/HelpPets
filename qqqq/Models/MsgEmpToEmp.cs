using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class MsgEmpToEmp
    {
        public int MsgEmpToEmpId { get; set; }
        public int? EmpSendId { get; set; }
        public int? EmpReceiveId { get; set; }
        public string Message { get; set; }
        public DateTime? MsgTime { get; set; }
        public bool? IsReceiveRead { get; set; }

        public virtual Employee EmpReceive { get; set; }
        public virtual Employee EmpSend { get; set; }
    }
}
