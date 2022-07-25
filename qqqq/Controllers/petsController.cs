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
            var a = db.Products.Where(x => x.IsPet == true);
            
            ////a.FirstOrDefault().SubCategory.SubCategoryName;
            foreach (var i in a)
            {
                CAdoptView adoptView = new CAdoptView();
                adoptView._prod = i;
                list.Add(adoptView);
            }
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
           foreach(var i  in a)
            {
                CAdoptView adoptView = new CAdoptView();
                adoptView._prod = i;
                list.Add(adoptView);
            }
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
                foreach (var i in a)
                {
                    CAdoptView adoptView = new CAdoptView();
                    adoptView._prod = i;
                    list.Add(adoptView);
                }
                Debug.WriteLine(list[0].GenderType);
                return PartialView("petsPhoto", list);
            }
            
        }
        public IActionResult petsDetial(int id)
        {
          
            var a = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            
            //CProductShow CprodShow = new CProductShow();
            //Product cProd = (new 我救浪Context()).Products.FirstOrDefault(p => p.ProductId == id);
            //CprodShow.product = cProd;
            //if (cProd.Photos.Any())
            //    CprodShow.Photos = cProd.Photos.ToList();
            //return View(CprodShow);
            return View(a);
        }

        //    public IActionResult petsPhoto()
        //    {
        //        我救浪Context db = new 我救浪Context();
        //        var datas = db.Products.Where(p => p.IsPet == true).ToList();

        //        //foreach (Product p in datas)
        //        //{
        //        //    CPet cprod = new CPet();
        //        //    cprod._prod = p;
        //        //    if (p.Photos.Any())
        //        //        cprod.Photos = p.Photos.ToList();
        //        //    list.Add(cprod);
        //        //}
        //        return PartialView(list);
        //    }
        //    public IActionResult petsPhotoRow()
        //    {
        //        我救浪Context db = new 我救浪Context();
        //        var datas = db.Products.Where(p => p.IsPet == true).ToList();
        //        List<CProductShow> list = new List<CProductShow>();
        //        foreach (Product p in datas)
        //        {
        //            CProductShow cprod = new CProductShow();
        //            cprod.product = p;
        //            if (p.Photos.Any())
        //                cprod.Photos = p.Photos.ToList();
        //            list.Add(cprod);
        //        }
        //        return PartialView(list);
        //    }
        //    public IActionResult petsAdopt()
        //    {
        //        return View();
        //    }
        //    public IActionResult petsMatch()
        //    {
        //        我救浪Context db = new 我救浪Context();
        //        var datas = db.Products.Where(p => p.IsPet == true).ToList();
        //        List<CProductShow> list = new List<CProductShow>();
        //        foreach (Product p in datas)
        //        {
        //            CProductShow cprod = new CProductShow();
        //            cprod.product = p;
        //            if (p.Photos.Any())
        //                cprod.Photos = p.Photos.ToList();
        //            list.Add(cprod);
        //        }
        //        return View(list);
        //    }
    }
}

