using Microsoft.AspNetCore.Http;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet.ViewModels
{
    public class CLoginView
    {
        public Member _Member;
        public Order _Order;
        public OrderDetail _OrderDetail;


        public CLoginView()
        {
            _Order = new Order();
            _OrderDetail = new OrderDetail();
            _Member = new Member();
        }
        public CLoginView(Member m)
        {
     
            _Member=m;
        }
       
        public int MemberId { get { return _Member.MemberId; } set { _Member.MemberId = value; } }
        [DisplayName("頭貼")]
        public string Photo { get { return _Member.Photo; } set { _Member.Photo = value; } }
        [DisplayName("Email")]
        [EmailAddress]   
        [Required]
        public string Email { get { return _Member.Email; } set { _Member.Email = value; } }
        [DisplayName("密碼"), Required]
        public string Password { get { return _Member.Password; } set { _Member.Password = value; } }
        [DisplayName("確認密碼"),Compare("Password")]
        public string PasswordConfirm { get; set; }
        [DisplayName("會員姓名"), Required()]
        public string Name { get { return _Member.Name; } set { _Member.Name = value; } }
        [DisplayName("手機"), Required()]
        public string MemberPhone { get { return _Member.MemberPhone; } set { _Member.MemberPhone = value; } }
        [DisplayName("生日"), DataType(DataType.Date), Required()]
        public DateTime BirthdayDate { get { return (DateTime)_Member.BirthdayDate; } set { _Member.BirthdayDate = value; } }
       
        [DisplayName("城市"), Required()]
        public int MemberCityId { get { return (int) _Member.CityId; }set { _Member.CityId = value; } }
        [DisplayName("地址"), Required()]
        public string Address { get { return _Member.Address; } set { _Member.Address = value; } }
        [DisplayName("性別"), Required()]
        public int? HgenderId { get { return (int)_Member.HgenderId; } set { _Member.HgenderId = value; } }
        public string Hgendertype { get { return _Member.Hgender.GenderType; } set { _Member.Hgender.GenderType = value; } }
        //看資料庫新增的欄位取什麼
        //[DisplayName("志工時數")]
        //public string Member志工時數 { get { return Member.志工時數; } set { Member.志工時數= value; } }



    }
}