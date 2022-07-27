using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using Pet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Pet.Controllers
{
    public class OrderController : Controller
    {
        我救浪Context db = new 我救浪Context();
        public IActionResult List()
        {
            var q = db.Orders.Where(o=>o.OrderStatusId==2&&o.OrderDetails.All(od=>od.Product.IsPet==false)).ToList();//進行中的order
            var orderStatus = db.OrderStatuses.Where(os=>os.OrderStatusId==2).Select(os => os.OrderStatusId).ToArray<int>();
            ViewBag.orderStatus = System.Text.Json.JsonSerializer.Serialize(orderStatus);
            ViewBag.isPet = false;
            return View(COrderView.COrderViews(q));
        }
        public string Detail(int id)
        {
            var q = db.OrderDetails.Where(od => od.OrderId == id).ToList();
            return System.Text.Json.JsonSerializer.Serialize(COrderDetailView.COrderDetailViews(q));
        }
        public ActionResult Delete(int id)
        {
            var q = db.OrderDetails.Where(od => od.OrderId == id).ToList();
            db.OrderDetails.RemoveRange(q);
            var q2 = db.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            db.Orders.Remove(q2);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            var q = db.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            return View(new COrderView(q));
        }
        [HttpPost]
        public bool Edit(OrderDetail[] details, DateTime OrderDate, string SendAddress,int  OrderStatusId,int id)
        {
            try
            {
                var q = db.Orders.Where(o => o.OrderId == id).FirstOrDefault();
                q.OrderStatusId = OrderStatusId;
                q.OrderDate = OrderDate;
                q.SendAddress = SendAddress;

                var q2 = db.OrderDetails.Where(od => od.OrderId == id).ToList();
                db.OrderDetails.RemoveRange(q2);

                foreach (var d in details)
                {
                    db.OrderDetails.Add(d);
                }
                db.SaveChanges();
                

                db.SaveChanges();

                return true;
            }
            catch { return false;}
        }
        public string SearchFindName(int id)
        {
            var q = db.Products.Where(p => p.ProductId == id &&p.IsPet==false).FirstOrDefault();
            if (q == null) return "找不到對應商品";
            else return q.ProductName;
        }
        public ActionResult Search(string keyword, decimal? PriceLow, decimal? PriceHigh,DateTime? DateLow,DateTime? DateHigh,int[] orderStatus,bool isPet)
        {
            var q=(from o in db.Orders.Include(o=>o.Member).Include(o=>o.OrderDetails).ThenInclude(od=>od.Product).AsEnumerable()
                 where (keyword==null? true:(o.Member.Name.Contains(keyword)||o.SendAddress.Contains(keyword)||o.OrderDetails.Any(od=>od.Product.ProductName.Contains(keyword))))
                 &&(PriceHigh==null?true:PriceHigh>=o.OrderDetails.Sum(o => (decimal)(o.UnitPrice * o.Quantity)))
                 && (PriceLow == null ? true : PriceLow <= o.OrderDetails.Sum(o => (decimal)(o.UnitPrice * o.Quantity)))
                 && (DateHigh == null ? true : DateHigh >= o.OrderDate)
                 && (DateLow==null?true:DateLow<=o.OrderDate)
                 &&(Array.Exists(orderStatus,x=>x==o.OrderStatusId))
                 &&(o.OrderDetails.All(od=>od.Product.IsPet==isPet))
                   select o).ToList();
            ViewBag.keyword = keyword;
            ViewBag.low = PriceLow;
            ViewBag.high = PriceHigh;
            ViewBag.dateLow = DateLow;
            ViewBag.datehigh = DateHigh;
            ViewBag.orderStatus=System.Text.Json.JsonSerializer.Serialize(orderStatus);
            ViewBag.isPet = isPet;
            return View("List",COrderView.COrderViews(q));
        }
    }
}
