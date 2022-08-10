using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHomeLess_R.Controllers;
using prjMVCDemo.vModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace final_test.Controllers
{
    public class VolunteerController : Controller
    {

        我救浪Context db = new 我救浪Context();
        List<VActivityViewModel> list = new List<VActivityViewModel>();
        
        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate("del",()=>autoDelete(), Cron.Minutely);
            return View();
        }
        public IActionResult SignUp(int? id)
        {
            VActivityViewModel v = new VActivityViewModel();
            var a = db.Vactivities.Where(x => x.ActivityId == id).Include(y => y.ActivityCategory).AsSplitQuery().FirstOrDefault();

            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                var b = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
                CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(b);
                var c = db.Members.Where(x => x.Email == memberview.Email).FirstOrDefault();
                v.MemberID = c.MemberId;
                v.MemberAddress = c.Address;
                v.MemberEmail = c.Email;
                v.MemberName = c.Name;
                v.MemberPhone = c.MemberPhone;
            }

            v.vactivity = a;
            v.ActivityCategoryName = a.ActivityCategory.CategoryName;
            return View(v);
        }
        public IActionResult loadCheckBox()
        {
            var a = db.VactivityCategories.Select(x => new { x.ActivityCategoryId, x.CategoryName }).ToList();
            return Json(a);
            
        }
        public IActionResult loadList(int[] intarr,DateTime date,string keyString)
        {
            list.Clear();
            int year = date.Year, month = date.Month, day = date.Day;
            if(keyString == "")
            {
                keyString = null;
            }
            IEnumerable<Vactivity> a = db.Vactivities.Include(x => x.ActivityCategory).AsSplitQuery().ToList();
            foreach (Vactivity j in a)
            {
                VActivityViewModel v = new VActivityViewModel()
                {
                    vactivity = j,
                    ActivityCategoryName = j.ActivityCategory.CategoryName,
                };
                list.Add(v);
            }

            List<VActivityViewModel> resultList = new List<VActivityViewModel>();
            if (intarr.Length == 0  && year == 1 && keyString == null)
            {
                foreach (VActivityViewModel j in list)
                {
                    resultList.Add(j);
                }
            }
            else if (intarr.Length != 0 && year == 1 && keyString==null)
            {
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            resultList.Add(j);
                        }
                    }
                }
            }
            else if (intarr.Length == 0 && year != 1 && keyString == null)
            {
                foreach (var i in list)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if(year <= int.Parse(endarr[0]) )
                    {
                        if(month>= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if(day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                resultList.Add(i);
                            }
                        }
                    }
                }
            }
            else if (intarr.Length == 0 && year == 1 && keyString != null)
            {
                foreach (var i in list)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else if(intarr.Length != 0 && year != 1 && keyString == null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                resultList.Add(i);
                            }
                        }
                    }
                }
            }
            else if (intarr.Length != 0 && year == 1 && keyString != null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else if (intarr.Length == 0 && year != 1 && keyString != null)
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                foreach (var i in list)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                localList.Add(i);
                            }
                        }
                    }
                }
                foreach (var i in localList)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            else
            {
                List<VActivityViewModel> localList = new List<VActivityViewModel>();
                List<VActivityViewModel> localList2 = new List<VActivityViewModel>();
                foreach (int i in intarr)
                {
                    foreach (VActivityViewModel j in list)
                    {
                        if (j.ActivityCategoryID == i)
                        {
                            localList.Add(j);
                        }
                    }
                }
                foreach (var i in localList)
                {
                    string[] startarr = i.StartDate.Split('-');
                    string[] endarr = i.EndDate.Split('-');
                    if (year <= int.Parse(endarr[0]))
                    {
                        if (month >= int.Parse(startarr[1]) && month <= int.Parse(endarr[1]))
                        {
                            if (day >= int.Parse(startarr[2]) && day <= int.Parse(endarr[2]))
                            {
                                localList2.Add(i);
                            }
                        }
                    }
                }
                foreach (var i in localList2)
                {
                    if (i.Description.Contains(keyString) || i.Title.Contains(keyString))
                    {
                        resultList.Add(i);
                    }
                }
            }
            return Json(resultList.Select(x => new { x.ActivityCategoryID, x.Title, x.PeopleInNeed, x.ActivityCategoryName, x.ActivityID, x.ActivityPhoto, x.Description, x.EndDate, x.StartDate }));

        }

        //A:正取 B:候補
        public IActionResult getBySession(DateTime dateSelected, int actID)
        {
            string dateFormat = $"{dateSelected.Year}-{String.Format("{0:00}", dateSelected.Month)}-{String.Format("{0:00}", dateSelected.Day)}";
            int morningCountA = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 2) && x.Waiting == false).ToList().Count;
            int morningCountB = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 2) && x.Waiting == true).ToList().Count;
            int afternoonCountA = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 3) && x.Waiting == false).ToList().Count;
            int afternoonCountB = db.Volunteers.Where(x => x.ActivityId == actID && x.AllowDate == dateFormat && (x.AllowTimeId == 1 || x.AllowTimeId == 3) && x.Waiting == true).ToList().Count;

            return Json(new
            {
                morningCountA = morningCountA,
                morningCountB = morningCountB,
                afternoonCountA = afternoonCountA,
                afternoonCountB = afternoonCountB
            });
        }

        public void saveToDB(string random, int MemID,int actID,string actDate,string actTime,string Name,string Phone,string Email ,string Status)
        {
            Volunteer v = new Volunteer();
            v.MemberId = MemID;
            v.ActivityId = actID;
            var date = actDate.Split('/');
            v.AllowDate = $"{date[0]}-{date[1].PadLeft(2,'0')}-{date[2].PadLeft(2, '0')}";
            v.AllowTimeId = db.VallowTimes.Where(x => x.TimeRange == actTime).Select(y => y.AllowTimeId).FirstOrDefault();
            v.Name = Name;
            v.Phone = Phone;
            v.Email = Email;
            v.CheckEmail = false;
            if (Status == "候補")
            {
                v.Waiting = true;
            }
            else
            {
                v.Waiting = false;
            }
            v.OrderDate = DateTime.Now.ToString();
            v.VerificationCode = random;
            db.Volunteers.Add(v);
            db.SaveChanges();
        }
       public IActionResult EmailTest(string random)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("helppetqqq@gmail.com");
            //mail.From = new MailAddress("billsagi0002@gmail.com");
            Debug.WriteLine(random);
            var b = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(b);
            var list = db.Volunteers.Where(x => x.VerificationCode == random).Select(y=>y).ToList();
            mail.To.Add(memberview.Email);
            //主旨
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "我救浪-志工活動驗證";
            //內文
            string title = db.Vactivities.Where(x => x.ActivityId == list[0].ActivityId).Select(y => y.Title).FirstOrDefault();
            
            string body = "<html><head><style>tr,td,th,tbody,thead,table{border:solid 1px black;border-collapse: collapse;}</style></head>";
            body += $"<body><h1>郵件送出時間{DateTime.Now}<br>確認以下資料，並於20分鐘內完成驗證</h1>";
            body += $"<h3>{title}</h3>";
            body += "<table><thead><tr><th>日期</th><th>時段</th><th>狀態</th><th>姓名</th><th>電話</th><th>電子信箱</th></tr></thead><tbody>";
            foreach(var i in list)
            {
                string status = "";
                if (i.Waiting == true)
                {
                    status = "候補";
                }
                else
                {
                    status = "正取";
                }
                body += $"<tr><td>{i.AllowDate}</td><td>{db.VallowTimes.Where(x=>x.AllowTimeId==i.AllowTimeId).Select(y=>y.TimeRange).FirstOrDefault()}</td><td>{status}</td><td>{i.Name}</td><td>{i.Phone}</td><td>{i.Email}</td></tr>";
            }
            body += $"</tbody></table><br><br>確認後請點此<a href='http://192.168.21.37/Volunteer/Verification?ver={random}'>連結</a></body></html>";
            //body += $"</tbody></table><br><br>確認後請點此<a href='https://localhost:44318/Volunteer/Verification?ver={random}'>連結</a></body></html>";
            mail.Body = body;

            //內文是否為html
            mail.IsBodyHtml = true;
            //優先權
            mail.Priority = MailPriority.Normal;
            //設定smtpclient
            SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("billsagi0002@gmail.com", "mobhanhugmtoswkr");
            client.Credentials = new NetworkCredential("helppetqqq@gmail.com", "mzlytybmvfbzskan");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            //client.Port = 25;
            client.EnableSsl = true;
            //client.UseDefaultCredentials = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            client.Dispose();
            return RedirectToAction("successPage",new { id = random });
            //todo email
        }
        public IActionResult Verification(string ver) {
            var a = db.Volunteers.Where(x => x.VerificationCode == ver).ToList();
            if(a!= null)
            {
                var time = DateTime.Parse(a.Select(x => x.OrderDate).FirstOrDefault());
                if (time.AddMinutes(20) < DateTime.Now)
                {
                    return Content("<h2>驗證失敗，超過時間。</h2>", "text/html",System.Text.Encoding.UTF8);
                }
                foreach (var i in a)
                {
                    i.CheckEmail = true;
                    i.VstatusId = 2;
                }
                db.SaveChanges();
                return Content("<h2>信箱驗證成功，你已完成報名。</h2>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content("<h2>查無此預約。</h2>", "text/html", System.Text.Encoding.UTF8);
            }
        }
        public IActionResult successPage(string id) {
            var a = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(a);
            ViewBag.Email = memberview.Email;
            ViewBag.ver = id;
            return View();
        }
        public IActionResult checkDate(string dateSelected,int actID,int memID)
        {
            var a = db.Volunteers.Where(x => x.MemberId == memID && x.AllowDate == dateSelected && x.ActivityId == actID).ToList().Count;
            if (a > 0)
            {
                return Json("exist");
            }
            return Json("");
        }
        public void autoDelete()
        {
            Random ran = new Random();
            var a = db.Volunteers.Where(x =>x.CheckEmail == false).Select(y => y).ToList();
            if(a!= null)
            {
                foreach (var i in a.Where(x => DateTime.Parse(x.OrderDate).AddSeconds(30) < DateTime.Now && x.CheckEmail == false))
                {
                    db.Remove(i);
                    db.SaveChanges();
                    if (i.AllowTimeId == 1)
                    {
                        var b = db.Volunteers.Where(x => x.CheckEmail == true && x.ActivityId == i.ActivityId && x.AllowDate == i.AllowDate && x.Waiting == true && (x.AllowTimeId == 2 || x.AllowTimeId == 1)).Select(y => y).FirstOrDefault();
                        if (b != null)
                        {
                            string str = (DateTime.Now.AddMinutes(1420)).ToString();
                            b.Waiting = false;
                            b.CheckEmail = false;
                            b.OrderDate = str;
                            b.VerificationCode = ran.Next().ToString();
                            db.SaveChanges();
                            EmailToWaiting(b.VerificationCode, (int)b.MemberId);
                        }
                        var c = db.Volunteers.Where(x => x.CheckEmail == true && x.ActivityId == i.ActivityId && x.AllowDate == i.AllowDate && x.Waiting == true && (x.AllowTimeId == 3 || x.AllowTimeId == 1)).Select(y => y).FirstOrDefault();
                        if (c != null)
                        {
                            string str = (DateTime.Now.AddMinutes(1420)).ToString();
                            c.Waiting = false;
                            c.CheckEmail = false;
                            c.OrderDate = str;
                            c.VerificationCode = ran.Next().ToString();
                            db.SaveChanges();
                            EmailToWaiting(c.VerificationCode, (int)c.MemberId);
                        }
                    }
                    else if(i.AllowTimeId == 2)
                    {
                        int need = (int)db.Vactivities.Where(x => x.ActivityId == i.ActivityId).Select(y => y.PeopleInNeed).FirstOrDefault();
                        int afternoon = db.Volunteers.Where(x => x.ActivityId == i.ActivityId && x.AllowTimeId == 3 && x.AllowDate == i.AllowDate).Count();
                        if(need - afternoon > 0)
                        {
                            var b = db.Volunteers.Where(x => x.CheckEmail == true && x.ActivityId == i.ActivityId && x.AllowDate == i.AllowDate && (x.AllowTimeId == 1 || x.AllowTimeId == 2) && x.Waiting == true).Select(y => y).FirstOrDefault();
                            if (b != null)
                            {
                                string str = (DateTime.Now.AddMinutes(1420)).ToString();
                                b.Waiting = false;
                                b.CheckEmail = false;
                                b.OrderDate = str;
                                b.VerificationCode = ran.Next().ToString();
                            }
                            db.SaveChanges();
                            EmailToWaiting(b.VerificationCode, (int)b.MemberId);
                        }
                    }
                    else if (i.AllowTimeId == 3)
                    {
                        int need = (int)db.Vactivities.Where(x => x.ActivityId == i.ActivityId).Select(y => y.PeopleInNeed).FirstOrDefault();
                        int morning = db.Volunteers.Where(x => x.ActivityId == i.ActivityId && x.AllowTimeId == 2 && x.AllowDate == i.AllowDate).Count();
                        if (need - morning > 0)
                        {
                            var b = db.Volunteers.Where(x => x.CheckEmail == true && x.ActivityId == i.ActivityId && x.AllowDate == i.AllowDate && (x.AllowTimeId == 1 || x.AllowTimeId == 3) && x.Waiting == true).Select(y => y).FirstOrDefault();
                            if (b != null)
                            {
                                string str = (DateTime.Now.AddMinutes(1420)).ToString();
                                b.Waiting = false;
                                b.CheckEmail = false;
                                b.OrderDate = str;
                                b.VerificationCode = ran.Next().ToString();
                            }
                            db.SaveChanges();
                            EmailToWaiting(b.VerificationCode, (int)b.MemberId);
                        }
                    }
                }
            }
        }

        public void EmailToWaiting(string random, int memID)
        {
            Debug.WriteLine("test");
            MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress("helppetqqq@gmail.com");
            mail.From = new MailAddress("billsagi0002@gmail.com");
            var a = db.Volunteers.Where(x => x.VerificationCode == random).FirstOrDefault();
            var email = db.Volunteers.Where(x => x.MemberId == a.MemberId).Select(y => y.Email).FirstOrDefault();
            mail.To.Add(email);
            Debug.WriteLine(email);
            //主旨
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "我救浪-志工活動驗證(正取通知)";
            //內文
            string title = db.Vactivities.Where(x => x.ActivityId == a.ActivityId).Select(y => y.Title).FirstOrDefault();
            string status = "";
            if (a.Waiting == true)
            {
                status = "候補";
            }
            else
            {
                status = "正取";
            }
            string body = "<html><head><style>tr,td,th,tbody,thead,table{border:solid 1px black;border-collapse: collapse;}</style></head>";
            body += $"<body><h1>郵件送出時間{DateTime.Now}<br>確認以下資料，並於{DateTime.Now.AddDays(1)}內完成驗證</h1>";
            body += $"<h3>{title}</h3><div style='color:red;'>";
            body += "<table><thead><tr><th>日期</th><th>時段</th><th>狀態</th><th>姓名</th><th>電話</th><th>電子信箱</th></tr></thead><tbody>";
            body += $"<tr><td>{a.AllowDate}</td><td>{db.VallowTimes.Where(x => x.AllowTimeId == a.AllowTimeId).Select(y => y.TimeRange).FirstOrDefault()}</td><td>{status}</td><td>{a.Name}</td><td>{a.Phone}</td><td>{a.Email}</td></tr>";

            body += $"</tbody></table><br><br>確認後請點此<a href='http://192.168.21.37/Volunteer/Verification?ver={random}'>連結</a></body></html>";
            //body += $"</tbody></table><br><br>確認後請點此<a href='https://localhost:44318/Volunteer/Verification?ver={random}'>連結</a></body></html>";
            mail.Body = body;

            //內文是否為html
            mail.IsBodyHtml = true;
            //優先權
            mail.Priority = MailPriority.Normal;
            //設定smtpclient
            SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("billsagi0002@gmail.com", "mobhanhugmtoswkr");
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
        }
    }
}
