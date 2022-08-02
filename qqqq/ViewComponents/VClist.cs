
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
    public class VClist : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            //var Vmem =await  _context.Orders.Where(o=>o.MemberId==memberview.MemberID).FirstOrDefaultAsync();
            var q = await _context.Orders.Where(o => o.MemberId == memberview.MemberID&&o.OrderDetails.All(od=>od.Product.IsPet==false)).ToListAsync();
  

            return View(q);
        }
    }
}
