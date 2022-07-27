using Microsoft.EntityFrameworkCore;
using qqqq.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Pet.ViewModels;
using Microsoft.AspNetCore.Http;
using prjMVCDemo.vModel;
using System.Text.Json;

namespace qqqq.ViewComponents
{
    public class VCdoneList : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
           
            var q = await _context.OrderDetails.Where(o => o.OrderId == id).ToListAsync();

            
            return View(q);

        
        }
    }
}
