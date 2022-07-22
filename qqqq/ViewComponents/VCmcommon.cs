using Microsoft.EntityFrameworkCore;
using qqqq.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Pet.ViewModels;

namespace qqqq.ViewComponents
{
    public class VCmcommon : ViewComponent
    {
        我救浪Context db = new 我救浪Context();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {


            return View();
        }
    }
}

