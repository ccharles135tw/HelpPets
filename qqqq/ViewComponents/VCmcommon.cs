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
    public class VCmcommon : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var mName = _context.Members.Where(m => m.Email == memberview.Email).FirstOrDefault();
         
            return View();

         
        }
    }
}

