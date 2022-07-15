using qqqq.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pet.ViewModels;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace final_test.Controllers
{
    public class VolunteerController : Controller
    {
        List<VActivityViewModel> list = new List<VActivityViewModel>();
        我救浪Context db = new 我救浪Context();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("{id}")]
        public string GetCategoryName(int? id)  
        {
            return db.VactivityCategories.Where(x => x.ActivityCategoryId == id).Select(y => y.CategoryName).FirstOrDefault().ToString();
        }
        public IActionResult Info(int? id)
        {
            
            int ID = id.Value;
            var a = db.Vactivities.Where(x => x.ActivityId == id).Select(y => y).FirstOrDefault();
            VActivityViewModel v = new VActivityViewModel();
            v.vactivity = a;
            v.ActivityCategoryName = GetCategoryName(a.ActivityCategoryId);
            return View(v);
        }
        public IActionResult SignUp(int? id)
        {
            
            int ID = id.Value;
            var a = db.Vactivities.Where(x => x.ActivityId == id).Select(y => y).FirstOrDefault();
            VActivityViewModel v = new VActivityViewModel();
            v.vactivity = a;
            v.ActivityCategoryName = GetCategoryName(a.ActivityCategoryId);
            //System.Diagnostics.Debug.WriteLine(a.ActivityCategoryName.ToString());
            return View(v);
        }
    }
}
