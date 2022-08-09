using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.ViewModels;
using prjHomeLess.ViewModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prjMVCDemo.vModel;
using System.Net.Mail;
using System.Net;

namespace qqqq.Controllers
{
    public class petsController : Controller
    {
        我救浪Context db = new 我救浪Context();
        List<CAdoptView> list = new List<CAdoptView>();
        public readonly IWebHostEnvironment _enviroment;
        public petsController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }

        public IActionResult petsList()
        {
            //var a = db.Products.Where(x => x.IsPet == true);
            //List<CAdoptView> list = new List<CAdoptView>();
            //////a.FirstOrDefault().SubCategory.SubCategoryName;
            //foreach (var i in a)
            //{
            //    CAdoptView adoptView = new CAdoptView();
            //    adoptView._prod = i;
            //    list.Add(adoptView);
            //}
            //Debug.WriteLine(list[0]._prod.Photos.FirstOrDefault().PictureName);
            return View(/*list*/);
        }
        public IActionResult petsPhoto()
        {
            var a = db.Products.Where(x => x.IsPet == true).ToList();

            ////a.FirstOrDefault().SubCategory.SubCategoryName;
            var list = CAdoptView.CAdoptViews(a);
            return PartialView("petsPhoto",list);
        }
        public IActionResult petsSubcategory(int id)
        {
            var list = db.SubCategories.Where(p => p.CategoryId == id || p.SubCategoryName == "不限").Select(p => new { SubCategoryId = p.SubCategoryId, SubCategoryName = p.SubCategoryName }).ToList();
            var jsonsubpet = JsonSerializer.Serialize(list);
            return Json(jsonsubpet);
        }
        public IActionResult SearchForPet(PetDetail petDetail,Product product,int categoryid)
        {
            var a = db.Products.Where(p => (product.SubCategoryId == 1 ? true : p.SubCategoryId == product.SubCategoryId) &&
                                    (p.IsPet==true)&&
                                    (categoryid==1?true:p.SubCategory.CategoryId==categoryid)&&
                                    (petDetail.GenderId == 1 ? true : p.PetDetail.GenderId == petDetail.GenderId) &&
                                    (petDetail.SizeId == 1 ? true : p.PetDetail.SizeId == petDetail.SizeId)).ToList();
            var list = CAdoptView.CAdoptViews(a);
            return PartialView("petsPhoto", list);
        }
        public IActionResult SearchForKeyword(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return petsPhoto();
            }
            else
            {
                var a = db.Products.Where(p => p.IsPet == true && (p.ProductName.Contains(keyword) || p.SubCategory.SubCategoryName.Contains(keyword) || p.SubCategory.Category.CategoryName.Contains(keyword))).ToList();
                var list = CAdoptView.CAdoptViews(a);
               //Debug.WriteLine(list[0].GenderType);
                return PartialView("petsPhoto", list);
            }
            
        }
        public IActionResult petsDetialone(int id)
        {
          
            var a = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            return View(a);
        }
        public IActionResult petsDetial2(int id, int memberid)
        {

            var a = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            ViewBag.memberid = memberid;
            return PartialView("petsDetial", a);
        }

        public IActionResult petsAdopt(int id)
        {
            var a = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            return View(new CAdoptView(a));
        }
        public IActionResult petsMatch(MemberWish memberWish)
        {
            我救浪Context db = new 我救浪Context();
            var datas = db.Products.Include(p=>p.SubCategory).AsEnumerable().Where( p => 
                (p.IsPet == true) && 
               ( p.Continued == true)&&
               (p.SubCategory.CategoryId==memberWish.CategoryId||memberWish.CategoryId==1))
                .Select(p => new CAdoptView(p, memberWish)).ToList();
            datas=datas.Where(p=>p.MatchScore>=55).OrderByDescending(p => p.MatchScore).ToList();


            var q = db.MemberWishes.Where(mw => mw.MemberId == memberWish.MemberId).FirstOrDefault();
            if(q==null)
            {
                q = new MemberWish();
                q.MemberId=memberWish.MemberId;
                q.AgeId = memberWish.AgeId;
                q.SizeId = memberWish.SizeId;
                q.CategoryId = memberWish.CategoryId;
                q.SubCategoryId = memberWish.SubCategoryId;
                q.GenderId = memberWish.GenderId;
                q.CityId = memberWish.CityId;
                q.ColorId = memberWish.ColorId;
                q.LigationId = memberWish.LigationId;
                db.MemberWishes.Add(q);
            }
            else
            {
                q.AgeId = memberWish.AgeId;
                q.SizeId = memberWish.SizeId;
                q.CategoryId = memberWish.CategoryId;
                q.SubCategoryId = memberWish.SubCategoryId;
                q.GenderId = memberWish.GenderId;
                q.CityId = memberWish.CityId;
                q.ColorId = memberWish.ColorId;
                q.LigationId = memberWish.LigationId;
            }
            db.SaveChanges();
            return View(datas);
        }
        public void WishSaveChange(PetDetail petDetail, Product product, int categoryid)
        {
            CLoginViewModel memberview = null;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                var b = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
                memberview = JsonSerializer.Deserialize<CLoginViewModel>(b);
            }
            var a = db.MemberWishes.Where(x => x.MemberId == memberview.MemberID).FirstOrDefault();
            a.CategoryId = categoryid;
            a.SubCategoryId = product.SubCategoryId;
            a.GenderId = petDetail.GenderId;
            a.ColorId = petDetail.ColorId;
            a.LigationId = petDetail.LigationId;
            a.SizeId = petDetail.SizeId;
            a.AgeId = petDetail.AgeId;
            a.CityId = petDetail.CityId;
            db.SaveChanges();

        }
        public void AdoptOrder(int memID , int productID)
        {
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                MemberId = memID,
                EmployeeId = 1,
                SendAddress = db.Members.Where(x => x.MemberId == memID).Select(y => y.Address).FirstOrDefault(),
                OrderStatusId = 2
            };
            db.Orders.Add(order);
            db.SaveChanges();

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = db.Orders.Select(x => x.OrderId).ToList().LastOrDefault(),
                ProductId = productID,
                UnitPrice = 0,
                Quantity = 1,
                IsDonate = false
            };
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();


        }
        public void EmailTest()
        {
            //Debug.WriteLine("123");
            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("helppetqqq@gmail.com");
            mail.From = new MailAddress("billsagi0002@gmail.com");
            //Debug.WriteLine(random);
            var b = HttpContext.Session.GetString((CDictionary.SK_LOGIN_USER));
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(b);
            //var list = db.Volunteers.Where(x => x.VerificationCode == random).Select(y => y).ToList();
            mail.To.Add(memberview.Email);
            //主旨
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "我救浪-寵物領養評估驗證";
            //內文
            //string title = db.Vactivities.Where(x => x.ActivityId == list[0].ActivityId).Select(y => y.Title).FirstOrDefault();

            //string body = "<html><head><style>tr,td,th,tbody,thead,table{border:solid 1px black;border-collapse: collapse;}</style></head>";
            //body += $"<body><h1>郵件送出時間{DateTime.Now}<br>確認以下資料，並於20分鐘內完成驗證</h1>";
            //body += $"<h3>{title}</h3>";
            //body += "<table><thead><tr><th>日期</th><th>時段</th><th>狀態</th><th>姓名</th><th>電話</th><th>電子信箱</th></tr></thead><tbody>";
            //foreach (var i in list)
            //{
            //    string status = "";
            //    if (i.Waiting == true)
            //    {
            //        status = "候補";
            //    }
            //    else
            //    {
            //        status = "正取";
            //    }
            //    body += $"<tr><td>{i.AllowDate}</td><td>{db.VallowTimes.Where(x => x.AllowTimeId == i.AllowTimeId).Select(y => y.TimeRange).FirstOrDefault()}</td><td>{status}</td><td>{i.Name}</td><td>{i.Phone}</td><td>{i.Email}</td></tr>";
            //}

            //body += $"</tbody></table><br><br>確認後請點此<a href='https://localhost:44318/Volunteer/Verification?ver={random}'>連結</a></body></html>";
            mail.Body = "<h3>您好:</h3></br><p>已收到您的領養申請,請等待進一步連絡,謝謝您</p></br><p>祝您順心</p></br><h3>我救浪團隊敬上</h3>";

            //內文是否為html
            mail.IsBodyHtml = true;
            //優先權
            mail.Priority = MailPriority.Normal;
            //設定smtpclient
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("helppetqqq@gmail.com", "mzlytybmvfbzskan");
            //client.Credentials = new NetworkCredential("billsagi0002@gmail.com", "deodtmemzhjqqcox");
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
            //return RedirectToAction("successPage", new { id = 1 });
            //todo email
        }
        public IActionResult petCompare()
        {
            return View();
        }
    }
}

