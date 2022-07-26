using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CCommentView
    {
        public MemberComment memberComment;
        public CCommentView(MemberComment m)
        {
            memberComment = m;
        }
        static public List<CCommentView> CCommentViews(List<MemberComment> list_mc)
        {
            List<CCommentView> list= new List<CCommentView>(); 
            foreach(var mc in list_mc)
            {
                CCommentView ccv= new CCommentView(mc);
                list.Add(ccv);
            }
            return list;
        }
        [DisplayName("商品名稱")]
        public string ProductName { get { return memberComment.Product.ProductName; } }
        [DisplayName("會員")]
        public string MemberId { get { return memberComment.CommentId+$"_{memberComment.Member.Name}"; } }
        [DisplayName("評價")]
        public int? Grade { get { return memberComment.Grade; } }
        [DisplayName("評論內容")]
        public string Description { get { return memberComment.Description; } }
        [DisplayName("時間")]
        public DateTime? CommentDate { get { return memberComment.CommentDate; } }
        [DisplayName("回覆數")]
        public int CountOfResponse { get { return memberComment.CommentResponses.Count; } }
    }
}
