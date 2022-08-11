using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.Models;
using Pet.ViewModels;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using qqqq.ViewModels;

namespace Pet.Controllers
{
    public class ProductController : Controller
    {
        我救浪Context db = new 我救浪Context();
        public IWebHostEnvironment _enviroment;
        public ProductController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }
        static int countForChatBox = 0;
        public int GetCountForCharBox()
        {
            countForChatBox++;
            return countForChatBox;
        }
        public string GetNameForChatBox(string userString)
        {
            string[] user = userString.Split('/');
            if (user[0] == "random") return $"訪客{user[1]}號";
            if (user[0]=="member")
            {
                var q = db.Members.AsEnumerable().Where(m => m.MemberId == int.Parse(user[1])).FirstOrDefault();
                return q.Name;
            }
            else
            {
                var q = db.Employees.AsEnumerable().Where(e=>e.EmpoyeeId == int.Parse(user[1])).FirstOrDefault();
                return q.Name;
            }
        }
        public string GetMessageForChat(string selfID,string clientID)
        {
            if (selfID.Contains("random") || selfID.Contains("random"))
            {
                return null;
            }
            int sID=int.Parse(selfID.Split('/')[1]);
            int cID = int.Parse(clientID.Split('/')[1]);
            if (selfID.Contains("member"))
            {

                    var q = db.MsgEmpAndMems.Where(m =>   m.MemberId == sID && m.EmployeeId == cID).ToList();
                    var q2 = MessageView.MessageViews(q);
                    return System.Text.Json.JsonSerializer.Serialize( q2);
            }
            else if(clientID.Contains("member"))
            {
                var q = db.MsgEmpAndMems.Where(m => m.MemberId == cID && m.EmployeeId == sID).ToList();
                var q2 = MessageView.MessageViews(q);
                return System.Text.Json.JsonSerializer.Serialize(q2);
            }
            else
            {
                var q = db.MsgEmpToEmps.Where(m => 
                (m.EmpSendId == sID && m.EmpReceiveId == cID)||
                (m.EmpSendId == cID && m.EmpReceiveId == sID)
                ).ToList();
                var q2 = MessageView.MessageViews(q);
                return System.Text.Json.JsonSerializer.Serialize(q2);
            }
        }
        public IActionResult List()
        {

            //var q1 = from p in db.Products where p.IsPet == false select p;
            //Debug.WriteLine(q1.FirstOrDefault().ProductName);
            //Debug.WriteLine(new CProductView(q1.FirstOrDefault()).SubCategoryName);
            //Debug.WriteLine(q1.FirstOrDefault().SubCategory.SubCategoryName);
            var q = db.Products.Include(p=>p.MemberComments).AsEnumerable().Where(p => p.IsPet == false && p.Continued == true).Select(p => new CProductView { Product = p, Rate =p.MemberComments.Count()==0?0:(double)p.MemberComments.Average(mc => mc.Grade) }).ToList();
            ViewBag.continued = true;
            var suppliers = db.Suppliers.Select(s => s.SupplierId).ToArray<int>();
            ViewBag.suppliers = System.Text.Json.JsonSerializer.Serialize(suppliers);
            return View(q);
            //return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public int Create(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
                return p.ProductId;
            }
            catch
            {
                return -1;
            }
        }
        public ActionResult Edit(int id)
        {
            //Debug.WriteLine(id);
            var q = db.Products.FirstOrDefault(p => p.ProductId == id);
            return View(new CProductView(q));
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            Debug.WriteLine(p);
            var q = db.Products.FirstOrDefault(pp => pp.ProductId == p.ProductId);
            q.ProductName = p.ProductName;
            q.Price = p.Price;
            q.SubCategoryId = p.SubCategoryId;
            q.SupplierId=p.SupplierId;
            q.Description= p.Description;
            q.UnitsInStock=p.UnitsInStock;
            q.Cost = p.Cost;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public bool Delete(int id)
        {
            try
            {
                var q = db.Products.FirstOrDefault(p => p.ProductId == id);
                var q2 = db.Photos.Where(ph => ph.ProductId == id).ToList();
                db.Photos.RemoveRange(q2);
                db.Products.Remove(q);
                db.SaveChanges();
                foreach(var p in q2)
                {
                    System.IO.FileInfo file = new FileInfo(_enviroment.WebRootPath + "/Images/" + p.PictureName);  //參數為圖片路徑
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
              //  Debug.WriteLine($"ProductID:{id}刪除成功");
                return true;
            }
            catch
            {
                 db = new 我救浪Context();
                Debug.WriteLine($"ProductID:{id}無法直接刪除，改為下架");
                var q = db.Products.FirstOrDefault(p => p.ProductId == id);
                q.Continued = false;
                db.SaveChanges();
                return false;
            }
        }
        public bool UnDelete(int id)
        {
            try
            {
                var q = db.Products.FirstOrDefault(p => p.ProductId == id);
                q.Continued = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ActionResult EasySearch(string Keyword,decimal? PriceLow,decimal? PriceHigh,bool Continued, int[] suppliers)
        {
            Debug.WriteLine(Continued);
            string keyword=Keyword;
            if(string.IsNullOrEmpty(Keyword)==false) keyword=keyword.ToUpper();
            var q = (from p in db.Products.Include(x => x.Supplier).Include(x => x.SubCategory).ThenInclude(x => x.Category).Include(x=>x.MemberComments).AsEnumerable()
                    where p.IsPet == false 
                    && (p.Continued==Continued)
                    && ((string.IsNullOrEmpty(Keyword)) ? true : (p.ProductName.ToUpper().Contains(keyword) || p.Supplier.Name.ToUpper().Contains(keyword)|| p.SubCategory.SubCategoryName.Contains(keyword) || p.SubCategory.Category.CategoryName.Contains(keyword)))
                    && ((PriceLow==null) ? true : p.Price >= (decimal)PriceLow)
                    && ((PriceHigh==null) ? true : p.Price <= (decimal)PriceHigh)
                    && (suppliers.Length == 0 ? false : Array.Exists(suppliers,x=>x==p.SupplierId))
                  //  orderby 1
                    select new CProductView { Product = p, Rate = p.MemberComments.Count() == 0 ? 0 : (double)p.MemberComments.Average(mc => mc.Grade) }).ToList();


            ViewBag.keyword = Keyword;
            ViewBag.low = PriceLow;
            ViewBag.high = PriceHigh;
            ViewBag.continued=Continued;
            ViewBag.suppliers = System.Text.Json.JsonSerializer.Serialize(suppliers);
           // Debug.WriteLine((suppliers));
            return View("List", q);
        }
        public ActionResult Delete_Images(string Delete_Images ,int ProductId)
        {
            if(Delete_Images == null) return RedirectToAction("Edit", new { id = ProductId });
            string[] PictureIDs = Delete_Images.TrimEnd('/').Split('/');
            foreach (var d in PictureIDs)
            {
                
                var q = db.Photos.AsEnumerable().FirstOrDefault(ph => ph.PictureId == int.Parse(d));
                string photoName = q.PictureName;
                db.Photos.Remove(q);
                System.IO.FileInfo file = new FileInfo(_enviroment.WebRootPath + "/Images/" +photoName);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            db.SaveChanges();
            return RedirectToAction("Edit", new { id=ProductId});
        }

        public ActionResult Add_Image(IFormFile[] File, int ProductID)
        {

            if (File.Length==0) return RedirectToAction("Edit", new { id = ProductID });
            else
            {

                foreach(var f in File)
                {
                    Photo photo = new Photo();
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    f.CopyTo(new FileStream(_enviroment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                    photo.ProductId = ProductID;
                    photo.PictureName = photoName;
                    db.Photos.Add(photo);
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = ProductID });
            }
        }
        public bool SetDefault(int? id)
        {
            
            try
            {
                var q = db.Photos.Where(ph => ph.PictureId == (int)id).FirstOrDefault();
                int productID = (int)q.ProductId;
                var q2 = db.Photos.Where(ph => ph.ProductId == productID && ph.IsDefault == true).FirstOrDefault();
                q.IsDefault = true;
                if (q2 != null) q2.IsDefault = false;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ActionResult SearchForID(int id)
        {
            var q = db.Products.AsEnumerable().Where(p => p.IsPet == false && p.Continued == true&&p.ProductId==id).ToList();
            if (q.FirstOrDefault() != null)
            {

                return View("List", CProductView.CProductViews(q));
            }
            else return Content("找不到符合商品", "text/html", System.Text.Encoding.UTF8);
        }
        //public bool Default()
        //{
        //    try
        //    {
        //        var q = db.Products.ToList();
        //        foreach (var p in q)
        //        {
        //            var q2 = db.Photos.Where(ph => ph.ProductId == p.ProductId).FirstOrDefault();
        //            if(q2!=null)q2.IsDefault = true;

        //        }
        //        db.SaveChanges();
        //        foreach (var p in q)
        //        {
        //            var q2 = db.Photos.Where(ph => ph.ProductId == p.ProductId && ph.IsDefault != true).ToList();
        //            foreach (var q3 in q2) q3.IsDefault = false;

        //        }
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

    }
}
