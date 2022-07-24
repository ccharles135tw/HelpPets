using qqqq.Models;
using Pet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace qqqq.ViewModels
{
    public class VActivityViewModel
    {
        private Vactivity _vactivity;

        //private CMemberView _cMemberView;
        private Member _cMemberView;
        public VActivityViewModel()
        {
            //_cMemberView = new CMemberView();
            _cMemberView = new Member();
            _vactivity = new Vactivity();
        }
        //public CMemberView cMemberView { get { return _cMemberView; } set { _cMemberView = value; } }
        public Member cMemberView { get { return _cMemberView; } set { _cMemberView = value; } }
        public Vactivity vactivity { get { return _vactivity; } set { _vactivity = value; } }
        public int ActivityID { get { return _vactivity.ActivityId; } set { _vactivity.ActivityId = value; } }
        public string Title { get { return _vactivity.Title; } set { _vactivity.Title = value; } }
        public string StartDate { get { return _vactivity.StartDate; } set { _vactivity.StartDate = value; } }
        public string EndDate { get { return _vactivity.EndDate; } set { _vactivity.EndDate = value; } }
        public int? ActivityCategoryID { get { return _vactivity.ActivityCategoryId; } set { _vactivity.ActivityCategoryId = value; } }
        public int? PeopleInNeed { get { return _vactivity.PeopleInNeed; } set { _vactivity.PeopleInNeed = value; } }
        public string ActivityPhoto { get { return _vactivity.ActivityPhoto; } set { _vactivity.ActivityPhoto = value; } }
        public string ActivityCategoryName { get; set; }
        public int MemberID { get { return _cMemberView.MemberId; } set { _cMemberView.MemberId = value; } }
        public string MemberName { get { return _cMemberView.Name; } set { _cMemberView.Name = value; } }
        public string MemberPhone { get { return _cMemberView.MemberPhone; } set { _cMemberView.MemberPhone = value; } }
        public string MemberEmail { get { return _cMemberView.Email; } set { _cMemberView.Email = value; } }
        public string MemberAddress { get { return _cMemberView.Address; } set { _cMemberView.Address = value; } }
        public string Description { get { return _vactivity.Description; } set { _vactivity.Description = value; } }

    }
}
