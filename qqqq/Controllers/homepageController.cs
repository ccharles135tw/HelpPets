using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class homepageController : Controller
    {
        private readonly ILogger<homepageController> _logger;

        public homepageController(ILogger<homepageController> logger)
        {
            _logger = logger;
        }

        public IActionResult homepage()
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
            return View(list);
        }
        public IActionResult enterpage()
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
    }
}
