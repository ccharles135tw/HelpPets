using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Pet.ViewModels;
using Microsoft.AspNetCore.Http;
using prjMVCDemo.vModel;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using Microsoft.EntityFrameworkCore;

namespace qqqq.ViewComponents
{
    public class VCemembername : ViewComponent

    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);
            var mName = await _context.Members.Where(m => m.MemberId == memberview.MemberID).FirstOrDefaultAsync();
            
            return View(mName);


   
        }
    }
}
