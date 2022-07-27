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
                var a = db.Products.Where(p => p.IsPet == true && (p.ProductName.Contains(keyword) || p.SubCategory.SubCategoryName.Contains(keyword) || p.SubCategory.Category.CategoryName.Contains(keyword) || p.Description.Contains(keyword))).ToList();
                var list = CAdoptView.CAdoptViews(a);
                Debug.WriteLine(list[0].GenderType);
                return PartialView("petsPhoto", list);
            }
            
        }
        public IActionResult petsDetial(int id)
        {
          
            var a = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            return View(a);
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
    }
}

