﻿using Microsoft.EntityFrameworkCore;
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
    public class VCmfavorite : ViewComponent
    {
        我救浪Context _context = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var sUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            CLoginViewModel memberview = JsonSerializer.Deserialize<CLoginViewModel>(sUser);

            var q = await _context.Products.Where(p => p.MyFavorites.First().MemberId == memberview.MemberID).ToListAsync();

            return View();
        }
    }
}

