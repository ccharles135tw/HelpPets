using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;

using Pet.ViewModels;

using prjMVCDemo.vModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjHomeLess_R.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private 我救浪Context _context = new 我救浪Context();
        private IWebHostEnvironment _environment;
        //注入IWebHostEnvironment




        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment p)
        {
            _logger = logger;
            _environment = p;

        }
        public IActionResult empLogin()
        {

            return View();
                }
        [HttpPost]
        public IActionResult empLogin(CLoginAccountViewModel vModel)
        {
            Employee emp = _context.Employees.FirstOrDefault(e => e.Phone == vModel.txtAccount);

            if (emp != null)
            {
                if (emp.Password.Equals(vModel.txtPassword))
                {
                    CEmp eV = new CEmp();

                    eV.EmpoyeeId = emp.EmpoyeeId;
                    eV.Phone = emp.Phone;
                    eV.Name = emp.Name;

                    string jsonUser = JsonSerializer.Serialize(eV);
                    HttpContext.Session.SetString(CDictionary.SK_LOGIN_EMP, jsonUser);
                    return RedirectToAction("BK_employee", "BK_employee");
                }
            }
    
            return View();

        }

    
        //=====================================
        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))

            {
                return RedirectToAction("Login");
            }

          var sUser=HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var mName=_context.Members.Where(m => m.Email == memberview.Email).FirstOrDefault();
 
            return View();

        }

        public IActionResult LogOut()
        {
       
            HttpContext.Session.Remove(CDictionary.SK_LOGIN_USER);
            return RedirectToAction("Login");
        }    

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Login(CLoginAccountViewModel vModel)
        {
            //資料庫檔案 ==================================================================
            Member mem = _context.Members.FirstOrDefault(m => m.Email.Equals(vModel.txtAccount));

            if (mem != null)
            {
                if (mem.Password.Equals(vModel.txtPassword))
                {
                    CLoginViewModel cm = new CLoginViewModel();
                  
                    cm .MemberID= mem.MemberId;
                    cm.Email = mem.Email;
                 
                    
                    string jsonUser = JsonSerializer.Serialize(cm);
                    HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, jsonUser);
                    

                    return RedirectToAction("homepage", "homepage");
                }
            }

            return View();

        }
        public IActionResult City()
        {
            var city = _context.Cities.Select(c=>new {cityName=c.CityName,cityId=c.CityId }).Distinct();
            //return Json(city);
            var JsonCity= JsonSerializer.Serialize(city);

            return Json(JsonCity);
        }
       

        public IActionResult Register()
        {

            return View();

        }

        [HttpPost]
        //==========================================================


        //public IActionResult Register(CLoginView Vmodel, IFormFile File)
        //{

        //    Member mem = new Member();

        //    mem = Vmodel._Member;

        //    if (Vmodel.Photo == null)
        //    {


        //        string pName = "mem" + mem.MemberId + ".jpg";
        //        File.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));
        //        Vmodel.Photo = pName;

        //            }


        //    _context.Members.Add(mem);
        //    _context.SaveChanges();



        //    return View(Vmodel);
        //    //todo
        //}
        

        public IActionResult Register(Member mem, IFormFile File)
        {

                string pName = Guid.NewGuid().ToString() + ".jpg";
                File.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + pName, FileMode.Create));
                mem.Photo = pName;

            _context.Members.Add(mem);
            _context.SaveChanges();

            return View("Login");

        }
        public IActionResult checkPsw(string email, string pwd1, string pwd2)
        {
            if(pwd1 != pwd2)
            {
                return Json("n");
            }
            else
            {
                var q = _context.Members.Where(m => m.Email == email && m.Password == pwd1).FirstOrDefault();
                if (q != null)
                {
                    return Json("q");
                }
                else
                {
                    return Json("");
                }//todo
            }
          

          
        }
        public IActionResult sendMail(string email)
        {
            var a = _context.Members.Where(x => x.Email == email).Select(y => y.Email).FirstOrDefault();
            if(a != null)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("helppetqqq@gmail.com");
                mail.To.Add(a);
                //主旨
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = "我就浪忘記密碼密碼";
                //內文
                //mail.Body = $"<html><h1>忘記密碼</h1><h4>請點擊以下連結以重新設定密碼</h4><body><a href='https://localhost:44318/home/forgetPwd?id={email}'>https://localhost:44318/home/forgetPwd</a></body></html>";
               mail.Body = $"<html><h1>忘記密碼</h1><h4>請點擊以下連結以重新設定密碼</h4><body><a href='http://192.168.21.37/home/forgetPwd?id={email}'>http://192.168.21.37/home/forgetPwd</a></body></html>";

                //內文是否為html
                mail.IsBodyHtml = true;
                //優先權
                mail.Priority = MailPriority.Normal;
                //設定smtpclient
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("helppetqqq@gmail.com", "mzlytybmvfbzskan");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;

                try
                {

                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                client.Dispose();


                return Json("a");
            }
            else
            {
                return Json("");
            }

        }

        public IActionResult forgetPwd(string id)
        {
            ViewBag.mail = id;
            return View();
        }
        public IActionResult forgetPwdlogined()
        {
            return View();
        }
        public IActionResult setPwd(string email,string pwd)
        {

            Debug.WriteLine(email);
            Debug.WriteLine(pwd);
            var q = _context.Members.Where(m => m.Email == email).FirstOrDefault();
                q.Password = pwd;
                _context.SaveChanges();

            return RedirectToAction("Login");
        
        }
     


        public IActionResult Registertry()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult homepage()
        {
            return View();
        }
    }
}
