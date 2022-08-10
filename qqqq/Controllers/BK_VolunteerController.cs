using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class BK_VolunteerController : Controller
    {
        private IWebHostEnvironment _environment;
        public BK_VolunteerController(IWebHostEnvironment p)
        {
            _environment = p;
        }
        我救浪Context db = new 我救浪Context();
        public IActionResult BK_VolunteerActivity()
        {
            var a = db.Vactivities.Select(n => n);
            List<BK_VActivityViewModel> list = new List<BK_VActivityViewModel>();
            foreach (Vactivity i in a)
            {
                BK_VActivityViewModel Vm = new BK_VActivityViewModel();
                Vm.vactivity = i;
                list.Add(Vm);
            }
            foreach (BK_VActivityViewModel i in list)
            {
                var b = db.VactivityCategories.Where(x => x.ActivityCategoryId == i.ActivityCategoryId).Select(y => y.CategoryName).FirstOrDefault();
                i.ActivityCategoryName = b;
            }
            return View(list);
        }
        public IActionResult getCategoryName()
        {
            var cateList = db.VactivityCategories.Select(x => x.CategoryName);
            return Json(cateList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BK_VActivityViewModel model)
        {
            string picName = "Demo.jpg";
            if (model.photo != null)
            {
                picName = Guid.NewGuid().ToString() + ".jpg";
                model.photo.CopyTo(new FileStream(_environment.WebRootPath + "/img/volunteer/" + picName, FileMode.Create));
            }
            Vactivity activity = new Vactivity()
            {
                ActivityId = 0,
                Title = model.Title,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ActivityCategoryId = db.VactivityCategories.Where(x => x.CategoryName == model.ActivityCategoryName).Select(y => y.ActivityCategoryId).FirstOrDefault(),
                PeopleInNeed = model.PeopleInNeed,
                ActivityPhoto = picName,
                Description = model.Description
            };

            db.Vactivities.Add(activity);
            db.SaveChanges();
            return RedirectToAction("BK_VolunteerActivity");
        }
        public IActionResult Delete(int? id)
        {
            var a = db.Vactivities.FirstOrDefault(x => x.ActivityId == id);
            if (a != null)
            {
                db.Vactivities.Remove(a);
                db.SaveChanges();
            }
            return RedirectToAction("BK_VolunteerActivity");
        }
        public IActionResult Edit(int? id)
        {
            var a = db.Vactivities.FirstOrDefault(x => x.ActivityId == id);
            if (a == null)
            {
                return RedirectToAction("BK_VolunteerActivity");
            }
            BK_VActivityViewModel model = new BK_VActivityViewModel()
            {
                vactivity = a,
                ActivityCategoryName = db.VactivityCategories.Where(x => x.ActivityCategoryId == a.ActivityCategoryId).Select(y => y.CategoryName).FirstOrDefault()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BK_VActivityViewModel model)
        {

            Vactivity a = db.Vactivities.FirstOrDefault(x => x.ActivityId == model.ActivityId);
            if (a != null)
            {
                if (model.photo != null)
                {
                    string picName = Guid.NewGuid().ToString() + ".jpg";
                    model.photo.CopyTo(new FileStream(_environment.WebRootPath + "/img/volunteer/" + picName, FileMode.Create));
                    a.ActivityPhoto = picName;
                }
                a.ActivityId = model.ActivityId;
                a.Title = model.Title;
                a.StartDate = model.StartDate;
                a.EndDate = model.EndDate;
                a.ActivityCategoryId = db.VactivityCategories.Where(x => x.CategoryName == model.ActivityCategoryName).Select(y => y.ActivityCategoryId).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine(model.ActivityCategoryName);
                a.PeopleInNeed = model.PeopleInNeed;
                a.Description = model.Description;
            }
            db.SaveChanges();
            return RedirectToAction("BK_VolunteerActivity");
        }
        public IActionResult actList()
        {
            return View();
        }
        public class Vlist
        {
            public string actName;
            public string name;
            public string introducer;
            public string time;
            public string phone;
            public string ver;
        }
        public IActionResult showResult(string date)
        {
            List<Vlist> list = new List<Vlist>();
            var a = db.Volunteers.Where(x => x.AllowDate == date && x.Waiting == false && x.CheckEmail == true).Select(y => y).OrderBy(z=>z.ActivityId).ThenBy(w=>w.AllowTimeId).ToList();
            foreach(var i in a)
            {
                Random ran = new Random(Guid.NewGuid().GetHashCode());
                i.VerificationCode = ran.Next().ToString();
                list.Add(new Vlist
                {
                    actName = db.Vactivities.Where(x=>x.ActivityId == i.ActivityId).Select(y=>y.Title).FirstOrDefault(),
                    name = i.Name,
                    introducer = db.Members.Where(x=>x.MemberId == i.MemberId).Select(y=>y.Name).FirstOrDefault(),
                    time = db.VallowTimes.Where(x=>x.AllowTimeId == i.AllowTimeId).Select(y=>y.TimeRange).FirstOrDefault(),
                    phone = i.Phone,
                    ver = i.VerificationCode
                });
            }
            db.SaveChanges();
            return Json(list.Select(x=> new { x.actName,x.name,x.time,x.introducer,x.phone,x.ver }));
        }
        public IActionResult register(string ver)
        {
            if (db.Volunteers.Where(y => y.VerificationCode==ver && y.VstatusId == 1).Count() != 0)
            {
                var a = db.Volunteers.Where(x => x.VerificationCode == ver).Select(y => y.Name).FirstOrDefault();
                return Content($"<h2>{a}，報到已完成，如有疑問請詢問現場工作人員。</h2>", "text/html", System.Text.Encoding.UTF8);
            }
            if (db.Volunteers.Select(y=>y.VerificationCode).Contains(ver))
            {
                var a = db.Volunteers.Where(x => x.VerificationCode == ver).Select(y => y).FirstOrDefault();
                a.VstatusId = 1;
                db.SaveChanges();
                return Content($"<h2>{a.Name}，報到成功。</h2>", "text/html", System.Text.Encoding.UTF8);
            }
            return Content("失敗??");
        }
        public void toPDF(string htmlString)
        {

            Debug.WriteLine(htmlString);
            StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Test.html"));
            sw.WriteLine("<style>table,td,th {border: 1px solid #ddd;text-align: left;}th {background-color: darkgray;} table {border-collapse: collapse;width: 100 %;}th, td {padding: 15px;}</style>");
            sw.WriteLine(htmlString);
            sw.Close();

            var url = Path.Combine(Environment.CurrentDirectory, "Test.html");
            var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            //var chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            //var chromePath = Path.Combine(Environment.CurrentDirectory, "chromedriver.exe");
            var output = Path.Combine(Environment.CurrentDirectory, "printout.pdf");
            using (var p = new Process())
            {
                p.StartInfo.FileName = chromePath;
                p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} {url}";
                p.Start();
                p.WaitForExit();
            }

            //WebClient wc = new WebClient();
            //wc.DownloadFile(Path.Combine(Environment.CurrentDirectory, "printout.pdf"), "C:\\new\\printout.pdf");
        }
        public FileResult download()
        {
            //string fileName = "printout.pdf";
            //string filePath = "C:\\Users\\Charles\\Desktop\\qqqq\\qqqq" + "\\" + fileName;
            //var fileExists = System.IO.File.Exists(Path.Combine(Environment.CurrentDirectory, "printout.pdf"));
            FileStream fs = System.IO.File.OpenRead(Path.Combine(Environment.CurrentDirectory, "printout.pdf"));
            return File(fs, "application/pdf", "printout.pdf");
        }
    }
}

