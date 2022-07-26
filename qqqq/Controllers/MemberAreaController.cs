﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pet.ViewModels;
using prjMVCDemo.vModel;
using qqqq.Models;
using System;
using System.Collections.Generic;
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
            mem.Photo = pName;

            if (Vmem != null)
            {

             

                _context.SaveChanges();
            }

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
        public IActionResult detailList()
        {
            return ViewComponent("VCdoneList");
        }
        public IActionResult mOrder()
        {

            return ViewComponent("VClist");
        }
        public IActionResult mFavorit()
        {

            return ViewComponent("VCmfavorite");
        }
        public IActionResult mPet()
        {

            return ViewComponent("VCmpet");
        }
        public IActionResult mCommon()
        {

            return ViewComponent("VCmcommon");
        }
        public IActionResult mVolunteer()
        {

            return ViewComponent("VCmvolunteer");
        }
    }
}
