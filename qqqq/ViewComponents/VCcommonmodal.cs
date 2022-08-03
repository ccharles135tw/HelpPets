using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjMVCDemo.vModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace qqqq.ViewComponents
{
    public class VCcommonmodal : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);

            var q = await _context.Products.Where(p => p.IsPet == false && p.MemberComments.FirstOrDefault().MemberId == memberview.MemberID).Where(p => p.MemberComments.FirstOrDefault().CommentId == id).ToListAsync();

            var list = CMemfavortView.CMemfavortViewS(q);
            return View(list);
        }
    }
}
