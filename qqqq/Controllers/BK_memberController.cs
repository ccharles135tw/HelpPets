using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.ViewModels;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class BK_memberController : Controller
    {

        public IWebHostEnvironment _environment;
        public BK_memberController(IWebHostEnvironment member)
        {
            _environment = member;
        }
        public IActionResult memList(CMemberView vModel)
        {
            //我救浪Context db = new 我救浪Context();
            //IEnumerable<Member> datas = null;
            //if(string.IsNullOrEmpty(vModel.txtKeyword))
            //{
            //    datas = from m in db.Members
            //            select m;
            //}
            //else
            //{
            //    datas = db.Members.Where(m => m.Name.Contains(vModel.txtKeyword));
            //}
            //ViewBag.keyword = vModel.txtKeyword;
            //return View(datas);



            我救浪Context db = new 我救浪Context();
            var datas = from m in db.Members
                        select m;
            List<CMemberView> list = new List<CMemberView>();
            foreach (Member m in datas)
            {
                CMemberView v = new CMemberView();
                v.Member = m;
                //v.MemberNewBirthDate =(string)m.BirthdayDate.ToString();
                list.Add(v);
            }
            return View(list);
        }

        //新增會員
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member m, IFormFile file)
        {
            我救浪Context db = new 我救浪Context();
            m.VolunteerHour = 0;
            //m.BirthdayDate =  m.BirthdayDate;


            //照片
            if (file != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                file.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                m.Photo = photoName;
            }
            db.Members.Add(m);
            db.SaveChanges();
            return RedirectToAction("memList");
        }


        //刪除會員
        public IActionResult Delete(int? id)
        {
            我救浪Context db = new 我救浪Context();
            Member mem = db.Members.FirstOrDefault(m => m.MemberId == id);
            if (mem != null)
            {
                db.Members.Remove(mem);
                db.SaveChanges();
            }
            return RedirectToAction("memList");
        }


        //修改會員
        public IActionResult Edit(int? id)
        {
            我救浪Context db = new 我救浪Context();
            Member mem = db.Members.FirstOrDefault(m => m.MemberId == id);
            if (mem == null)
                return RedirectToAction("memList");
            return View(new CMemberView(mem));
        }
        [HttpPost]
        public IActionResult Edit(Member mb, IFormFile file)
        {
            我救浪Context db = new 我救浪Context();
            var q = db.Members.FirstOrDefault(m => m.MemberId == mb.MemberId);
            if (q != null)
            {
                q.Name = mb.Name;
                q.HgenderId = mb.HgenderId;
                q.BirthdayDate = mb.BirthdayDate;
                q.MemberPhone = mb.MemberPhone;
                q.Password = mb.Password;
                q.CityId = mb.CityId;
                q.Address = mb.Address;
                q.Email = mb.Email;

                db.SaveChanges();
            }
            var qph = db.Members.FirstOrDefault(m => m.MemberId == mb.MemberId).Photo;
            if ( file != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                file.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                mb.Photo = photoName;
                db.Members.Remove(db.Members.Where(ph => ph.MemberId == mb.MemberId).FirstOrDefault());
                db.Members.Add(mb);
                db.SaveChanges();
            }
          
            return RedirectToAction("memList");
        }



        //
        public IActionResult KeyWord(string keyword)
        {
            我救浪Context db = new 我救浪Context();
            List<CMemberView> datas = null;
            ViewBag.keyword = keyword;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = (from m in db.Members
                         select new CMemberView(m)).ToList();
            }
            else
            {
                datas = db.Members.Where(m => m.Name.Contains(keyword) || m.MemberPhone.Contains(keyword) || m.Email.Contains(keyword)).Select(m => new CMemberView(m)).ToList();

            }
            return View("memList",datas);
        }
    }
}
