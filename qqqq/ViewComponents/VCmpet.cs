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
using prjHomeLess.ViewModel;

namespace qqqq.ViewComponents
{
    public class VCmpet : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);

            var q = await _context.Orders.Where(o => o.MemberId ==memberview.MemberID&&o.OrderDetails.All(od=>od.Product.IsPet==true)).ToListAsync();

            var list = CMempetView.CMempetViews(q);
            return View(list);
        }
    }
}

