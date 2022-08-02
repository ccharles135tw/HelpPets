using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHomeLess.ViewModel;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewComponents
{
    public class VCmpetdetail:ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var q = await _context.Products.Where(p=>p.ProductId== id).FirstOrDefaultAsync();
            CPet cPet = new CPet(q);
          
            return View(cPet);


        }
    }
}
