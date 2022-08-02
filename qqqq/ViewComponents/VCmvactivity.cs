using Microsoft.EntityFrameworkCore;
using qqqq.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Pet.ViewModels;
using prjMVCDemo.vModel;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace qqqq.ViewComponents
{
    public class VCmvactivity : ViewComponent
    {
        我救浪Context db = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(user);
            var a = await db.Volunteers.Where(x => x.MemberId == id).ToListAsync();

            return View(a);
        }
    }
}

