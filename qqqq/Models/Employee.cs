using System;
using System.Collections.Generic;

#nullable disable

namespace qqqq.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CommentResponses = new HashSet<CommentResponse>();
            MsgEmpAndMems = new HashSet<MsgEmpAndMem>();
            MsgEmpToEmpEmpReceives = new HashSet<MsgEmpToEmp>();
            MsgEmpToEmpEmpSends = new HashSet<MsgEmpToEmp>();
            Orders = new HashSet<Order>();
            Suppliers = new HashSet<Supplier>();
        }

        public int EmpoyeeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CommentResponse> CommentResponses { get; set; }
        public virtual ICollection<MsgEmpAndMem> MsgEmpAndMems { get; set; }
        public virtual ICollection<MsgEmpToEmp> MsgEmpToEmpEmpReceives { get; set; }
        public virtual ICollection<MsgEmpToEmp> MsgEmpToEmpEmpSends { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
