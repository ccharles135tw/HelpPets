using Microsoft.EntityFrameworkCore;
using qqqq.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Pet.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using prjMVCDemo.vModel;

namespace qqqq.ViewComponents
{
    public class VCmvolunteer : ViewComponent
    {
        我救浪Context db = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);

            var q =await db.Volunteers.Where(v => v.MemberId == memberview.MemberID&&v.Email==memberview.Email).ToListAsync();

            var list = CMemvoltView.CMemvoltViews(q);
            return View(list);

        }
    }
}

