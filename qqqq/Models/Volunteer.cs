using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class Volunteer
    {
        public int VolunteerId { get; set; }
        public int? MemberId { get; set; }
        public int? ActivityId { get; set; }
        public string AllowDate { get; set; }
        public int? AllowTimeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? CheckEmail { get; set; }
        public bool? Waiting { get; set; }
        public string OrderDate { get; set; }
        public string VerificationCode { get; set; }

        public virtual Vactivity Activity { get; set; }
        public virtual VallowTime AllowTime { get; set; }
        public virtual Member Member { get; set; }
    }
}
