using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjHomeLess_R.Controllers
{
    public class MemberAreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult MemberArea()
        {
            return View();
        }
        public IActionResult MemberEdoit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberEdoit(Member mem)
        {
            return RedirectToAction("MemberDoneList");
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
