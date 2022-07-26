using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class CommentResponse
    {
        public int ResponseId { get; set; }
        public int? MemberId { get; set; }
        public int? EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? CommentId { get; set; }
        public bool? IsEmployee { get; set; }

        public virtual MemberComment Comment { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Member Member { get; set; }
    }
}
