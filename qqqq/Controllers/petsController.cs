using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.ViewModels;
using prjHomeLess.ViewModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class petsController : Controller
    {
        我救浪Context db = new 我救浪Context();
        public readonly IWebHostEnvironment _enviroment;
        public petsController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }

        public IActionResult petsList()
        {
            var q = db.Products.AsEnumerable().Where(p => p.IsPet == true).ToList();
            return View(CProductView.CProductViews(q));
        }
        public IActionResult petsSubcategory(int id)
        {
            var db = new 我救浪Context();
            var list = db.SubCategories.Where(p => p.CategoryId == id || p.SubCategoryName == "不限").Select(p=>new { SubCategoryId =p.SubCategoryId,SubCategoryName=p.SubCategoryName }).ToList();
            var jsonsubpet = JsonSerializer.Serialize(list);
            return Json(jsonsubpet);
        }

        public IActionResult Search(string keyword)
        {

            return View();
        }
        public IActionResult petsDetial()
        {
            return View();
        }

        public IActionResult petsPhoto()
        {
            我救浪Context db = new 我救浪Context();
            var datas = db.Products.Where(p => p.IsPet == true).ToList();
            List<CProductShow> list = new List<CProductShow>();
            foreach (Product p in datas)
            {
                CProductShow cprod = new CProductShow();
                cprod.product = p;
                if (p.Photos.Any())
                    cprod.Photos = p.Photos.ToList();
                list.Add(cprod);
            }
            return PartialView(list);
        }
        public IActionResult petsPhotoRow()
        {
            我救浪Context db = new 我救浪Context();
            var datas = db.Products.Where(p => p.IsPet == true).ToList();
            List<CProductShow> list = new List<CProductShow>();
            foreach (Product p in datas)
            {
                CProductShow cprod = new CProductShow();
                cprod.product = p;
                if (p.Photos.Any())
                    cprod.Photos = p.Photos.ToList();
                list.Add(cprod);
            }
            return PartialView(list);
        }
        public IActionResult petsAdopt()
        {
            return View();
        }
        public IActionResult petsMatch()
        {
            return View();
        }
    }   
}

