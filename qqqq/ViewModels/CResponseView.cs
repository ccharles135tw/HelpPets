using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CResponseView
    {
        public CommentResponse commentResponse;
        public CResponseView(CommentResponse cr)
        {
            commentResponse = cr;
        }
        static public List<CResponseView> CResponseViews(List<CommentResponse> list_cr)
        {
            List<CResponseView> list=new List<CResponseView>();
            foreach(var cr in list_cr)
            {
                CResponseView cResponseView = new CResponseView(cr);
                list.Add(cResponseView);
            }
            return list;
        }
        public int ResponseId { get { return commentResponse.ResponseId; } }
        public string MemberId { get { return  commentResponse.MemberId + $"(會員){commentResponse.Member.Name}"; } }
        public string EmployeeId { get {return commentResponse.EmployeeId+$"(員工){commentResponse.Employee.Name}"; } }
        public string Description { get { return commentResponse.Description; } }
        public DateTime CommentDate { get { return (DateTime)commentResponse.CommentDate; } }
        public bool IsEmployee { get { return (bool)commentResponse.IsEmployee; } }
    }
}
