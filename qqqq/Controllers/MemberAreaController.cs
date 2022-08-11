﻿using final_test.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet.ViewModels;
using prjMVCDemo.vModel;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjHomeLess_R.Controllers
{
    public class MemberAreaController : Controller
    {
        private readonly ILogger<MemberAreaController> _logger;
        private 我救浪Context _context = new 我救浪Context();
        private IWebHostEnvironment _environment;
        //注入IWebHostEnvironment




        public MemberAreaController(ILogger<MemberAreaController> logger, IWebHostEnvironment p)
        {
            _logger = logger;
            _environment = p;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberArea(int? id)
        {

            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                return RedirectToAction("Login", "HOME");
            }
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var Vmem = _context.Members.Where(m => m.MemberId == memberview.MemberID).FirstOrDefault();
          

            return View(Vmem);

        }



        public IActionResult MemberEdoit( )
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                return RedirectToAction("Login", "HOME");
            }
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var Vmem = _context.Members.Where(m => m.MemberId == memberview.MemberID).FirstOrDefault();
            CLoginView log = new CLoginView(Vmem);
            return View(log);
        }
        [HttpPost]
        public IActionResult MemberEdoit(Member mem, IFormFile File)
        {


            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var Vmem = _context.Members.Where(m => m.MemberId == memberview.MemberID).FirstOrDefault();

            string pName = Guid.NewGuid().ToString() + ".jpg";
            File.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));

            if (mem.Photo != null)
                Vmem.Photo =Vmem.Photo;
            else
                Vmem.Photo = pName;

            Vmem.Name = mem.Name;
            Vmem.Email = mem.Email;
            Vmem.Password = mem.Password;
            Vmem.MemberPhone = mem.MemberPhone;
            Vmem.CityId = mem.CityId;
            Vmem.Address = mem.Address;
            Vmem.BirthdayDate = mem.BirthdayDate;
            Vmem.HgenderId = mem.HgenderId;


                _context.SaveChanges();
       

            return RedirectToAction("MemberArea");


        }
        public IActionResult MemberAreatry()
        {
            return View();
        }

        public IActionResult MemberDoneList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberDoneList(Member mem)
        {
            return View();
        }

        public IActionResult MemberDoneListTry()
        {

            return PartialView();
        }

        public IActionResult DetailDoneList()
        {


            return PartialView();
        }
        public IActionResult detailList(int id)
        {
            return ViewComponent("VCdoneList",id);
        }
        public IActionResult mOrder()
        {

            return ViewComponent("VClist");
        }
        public IActionResult mFavorit()
        {

            return ViewComponent("VCmfavorite");
        }
        public IActionResult mPet(int id)
        {

            return ViewComponent("VCmpet",id);
        }
        public IActionResult mCommon()
        {

            return ViewComponent("VCmcommon");
        }
        public IActionResult mVolunteer()
        {

            return ViewComponent("VCmvolunteer");
        }  
        
        public IActionResult mName()
        {

            return ViewComponent("VCemembername");
        }
        public IActionResult mPetdetail(int id)
        {
            return ViewComponent("VCmpetdetail", id);
        }
        public IActionResult mCommondetail(int id)
        {
            Debug.WriteLine(id);
            return ViewComponent("VCcommonmodal", id);
        }
        public void EdiotmCommondetail(int id, int Rate, string Description)
        {
         MemberComment  mComm  =_context.MemberComments.FirstOrDefault(c => c.CommentId == id);
            Debug.WriteLine(mComm.Description);
            if (mComm != null)
            {
                mComm.Description = Description;
                mComm.Grade = Rate;
            }
            _context.SaveChanges();
           
        }
        public void DeleCommon(int id)
        {
            var q = _context.MemberComments.Where(m => m.CommentId == id).FirstOrDefault();

            _context.Remove(q);
            _context.SaveChanges();

        }
        public void CreatCommond(int id,string Description,int Rate) 
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);

            MemberComment comm = new MemberComment();
            comm.ProductId = id;
            comm.CommentDate = DateTime.Now;
            comm.Description = Description;
            comm.Grade = Rate;
            comm.MemberId = memberview.MemberID;

            _context.MemberComments.Add(comm);
            _context.SaveChanges();

        

        }

        public IActionResult mCreatCommondModal(int id)
        {
            return ViewComponent("VCcreatcommonmodal", id);
        }
        public IActionResult mRemove(int id)

        {
            var q = _context.MyFavorites.FirstOrDefault(p=>p.MyFavorite1==id);

            _context.Remove(q);
            _context.SaveChanges();

                return ViewComponent("VCmfavorite",id);
        }

        public IActionResult check(int id)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            if (_context.MemberComments.Where(m => m.MemberId == memberview.MemberID && m.ProductId == id).Any())
            {
                return Json("a");
            }
            return Json("");
        }
        //charles================================================================================
        public IActionResult mVactivity()
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            return ViewComponent("VCmvactivity", new { id = memberview.MemberID });
        }
        public IActionResult cancelAct(string AllowDate,string memName)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var a = _context.Volunteers.Where(x => x.MemberId == memberview.MemberID && x.AllowDate == AllowDate).ToList();
            string name = _context.Members.Where(x => x.MemberId == memberview.MemberID).Select(y => y.Name).FirstOrDefault();
            int count = 0;
            int actID = (int)a[0].ActivityId;
            if(memName == name)
            {
                foreach (var i in a)
                {
                    count++;
                    _context.Remove(i);
                }
            }
            else
            {
                count++;
                _context.Remove(a.Where(x => x.Name == memName).FirstOrDefault());
            }
            _context.SaveChanges();
            checkSpace(count, AllowDate,actID);

            return ViewComponent("VCmvactivity",new{id = memberview.MemberID});
        }
        public void checkSpace(int count ,string AllowDate,int actID)
        {
            for (int i = 0; i < count; i++)
            {
                var a = _context.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == AllowDate&& x.Waiting == true).Select(y=>y).FirstOrDefault();
                if (a != null)
                {
                    string str = (DateTime.Now.AddMinutes(1420)).ToString();
                    Random ran = new Random();
                    a.Waiting = false;
                    a.CheckEmail = false;
                    a.OrderDate = str;
                    a.VerificationCode = ran.Next().ToString();
                    _context.SaveChanges();
                    VolunteerController volunteer = new VolunteerController();
                    volunteer.EmailToWaiting(a.VerificationCode, (int)a.MemberId);
                }
            }
        }
    }
}
