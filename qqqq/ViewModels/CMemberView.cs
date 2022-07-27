using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pet.ViewModels
{
    public class CMemberView
    {
        public Member Member;
        public CMemberView()
        {
            Member = new Member();
        }
        public int MemberID { get { return Member.MemberId; } }
        //public int MemberId { get { return Member.MemberId; } set { Member.MemberId = value; } }
        [DisplayName("會員名稱")]
        public string Name { get { return Member.Name; } }
        [DisplayName("密碼")]
        public string Password { get { return Member.Password; }  }
        [DisplayName("手機")]
        public string MemberPhone { get { return Member.MemberPhone; }  }
        [DisplayName("地址")]
        public string Address { get { return Member.Address; }  }
        public int CityID { get { return (int)Member.CityId; }  }
        [DisplayName("城市")]
        public string CityName { get { return Member.City.CityName; }}
        //[DisplayName("生日")]
        //public DateTime BirthDate { get { return (DateTime)Member.BirthdayDate; }}
        [DisplayName("性別")]
        public string GenderType { get { return Member.Hgender.GenderType; } }
        public int HgenderId { get { return (int)Member.HgenderId;}}
        [DisplayName("Email")]
        public string Email { get { return Member.Email; }}

        /// <summary>
        /// ////////////////////
        /// </summary>
        /// 
        [DisplayName("生日")]
        public string BirthdayDate { get { return ((DateTime)Member.BirthdayDate).ToString("yyyy/MM/dd"); } }

        [DisplayName("頭貼")]
        public string Photo { get { return Member.Photo; } }

        public CMemberView(Member member)
        {
            Member = member;

        }
        //看資料庫新增的欄位取什麼
        //[DisplayName("志工時數")]
        //public string Member志工時數 { get { return Member.志工時數; } set { Member.志工時數= value; } }



    }
}