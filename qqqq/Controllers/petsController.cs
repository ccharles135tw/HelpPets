using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjHomeLess.ViewModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class petsController : Controller
    {
        我救浪Context db = new 我救浪Context();
        public IWebHostEnvironment _enviroment;
        public petsController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }
        public IActionResult petsList()
        {
            return View();
        }
        public IActionResult pets()
        {
            return View();
        }
        public IActionResult showpets()
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
    }
}

