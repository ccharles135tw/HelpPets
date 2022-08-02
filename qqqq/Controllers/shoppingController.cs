using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using prjMVCDemo.vModel;

namespace qqqq.Controllers
{
    public class shoppingController : Controller
    {
        private bool array = true;
        我救浪Context db = new 我救浪Context();
        public IActionResult shopping()
        {

            List<Category> list = db.Categories.Where(p => p.IsPet == false).ToList();
            return View(list);
        }
        public IActionResult shoppingRow(CShoppingKeyword key)
        {

            if (key.ParentCategory == 0 && key.Category == 0 && key.SubCategory == null && key.Keyword == null)
            {

                var datas = db.Products.Where(p => p.IsPet == false).ToList();
                List<CProductShow> list = new List<CProductShow>();
                foreach (Product p in datas)
                {
                    CProductShow cprod = new CProductShow();
                    cprod.product = p;
                    if (p.Photos.Any())
                        cprod.Photos = p.Photos.ToList();
                    list.Add(cprod);
                }
                return PartialView(list);
            }
            else if (key.Search == false)
            {
                var list = db.Products.Where(p => p.IsPet == false).ToList();
                if (key.Category != 0)
                {
                    list = list.Where(p => p.SubCategory.CategoryId == key.Category).ToList();
                }
                if (key.SubCategory != null)
                {
                    foreach (var n in key.SubCategory)
                    {
                        list = list.Where(p => p.SubCategoryId == n).ToList();

                    }
                }
                if (key.Keyword != null)
                {
                    if (!string.IsNullOrEmpty(key.Keyword.Trim()))
                    {
                        list = list.Where(p => p.ProductName.Contains(key.Keyword.Trim())).ToList();
                    }
                }
                List<CProductShow> data = new List<CProductShow>();
                foreach (var p in list)
                {
                    CProductShow cPro = new CProductShow();
                    cPro.product = p;
                    if (p.Photos.Any())
                        cPro.Photos = p.Photos.ToList();
                    data.Add(cPro);
                }
                return PartialView(data);
            }
            else
            {
                List<CProductShow> list = new List<CProductShow>();
                //if (key.ParentCategory != 0)
                //{
                //    var parCate = db.Products.Where(p => p.SubCategory.Category.ParentCategory == key.ParentCategory && p.IsPet == false).ToList();
                //    foreach (var p in parCate)
                //    {
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                //if (key.Category != 0)
                //{
                //    var parCate = db.Products.Where(p => p.SubCategory.CategoryId == key.Category && p.IsPet == false).ToList();
                //    foreach (var p in parCate)
                //    {
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                if (key.SubCategory != null)
                {
                    foreach (var s in key.SubCategory)
                    {
                        var subCate = db.Products.Where(p => p.SubCategoryId == s && p.IsPet == false).ToList();
                        foreach (var p in subCate)
                        {
                            CProductShow Cpro = new CProductShow();
                            Cpro.product = p;
                            if (p.Photos.Any())
                                Cpro.Photos = p.Photos.ToList();
                            //if (!list.Contains(Cpro))
                            list.Add(Cpro);
                        }
                    }
                }
                //if (key.Keyword != null)
                //{
                //    var parCate = db.Products.Where(p => p.ProductName.Contains(key.Keyword) && p.IsPet == false).ToList();
                //    foreach (var p in parCate)
                //    {
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                //}
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                if (key.Keyword != null)
                {
                    var parCate = db.Products.Where(p => p.ProductName.Contains(key.Keyword) && p.IsPet == false).ToList();
                    foreach (var p in parCate)
                    {
                        CProductShow Cpro = new CProductShow();
                        Cpro.product = p;
                        if (p.Photos.Any())
                            Cpro.Photos = p.Photos.ToList();
                        if (!list.Contains(Cpro))
                            list.Add(Cpro);
                    }
                }

                return PartialView(list);
            }
        }


        public IActionResult shoppingCol(CShoppingKeyword key)
        {


            if (key.ParentCategory == 0 && key.Category == 0 && key.SubCategory == null && key.Keyword == null)
            {

                var datas = db.Products.Where(p => p.IsPet == false).ToList();
                List<CProductShow> list = new List<CProductShow>();
                foreach (Product p in datas)
                {
                    CProductShow cprod = new CProductShow();
                    cprod.product = p;
                    if (p.Photos.Any())
                        cprod.Photos = p.Photos.ToList();
                    list.Add(cprod);
                }
                return PartialView(list);
            }
            else if (key.Search == false)
            {
                var list = db.Products.Where(p => p.IsPet == false).ToList();
                if (key.Category != 0)
                {
                    list = list.Where(p => p.SubCategory.CategoryId == key.Category).ToList();
                }
                if (key.SubCategory != null)
                {
                    foreach (var n in key.SubCategory)
                    {
                        list = list.Where(p => p.SubCategoryId == n).ToList();

                    }
                }
                if (key.Keyword != null)
                {
                    if (!string.IsNullOrEmpty(key.Keyword.Trim()))
                    {
                        list = list.Where(p => p.ProductName.Contains(key.Keyword.Trim())).ToList();
                    }
                }
                List<CProductShow> data = new List<CProductShow>();
                foreach (var p in list)
                {
                    CProductShow cPro = new CProductShow();
                    cPro.product = p;
                    if (p.Photos.Any())
                        cPro.Photos = p.Photos.ToList();
                    data.Add(cPro);
                }
                return PartialView(data);
            }
            else
            {
                List<CProductShow> list = new List<CProductShow>();
                //if (key.ParentCategory != 0)
                //{
                //    var parCate = db.Products.Where(p => p.SubCategory.Category.ParentCategory == key.ParentCategory && p.IsPet == false).ToList();
                //    foreach (var p in parCate)
                //    {
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                //if (key.Category != 0)
                //{
                //    var parCate = db.Products.Where(p => p.SubCategory.CategoryId == key.Category && p.IsPet == false).ToList();
                //    foreach (var p in parCate)
                //    {
                //        CProductShow Cpro = new CProductShow();
                //        Cpro.product = p;
                //        if (p.Photos.Any())
                //            Cpro.Photos = p.Photos.ToList();
                //        if (!list.Contains(Cpro))
                //            list.Add(Cpro);
                //    }
                //}
                if (key.SubCategory != null)
                {
                    foreach (var s in key.SubCategory)
                    {
                        var subCate = db.Products.Where(p => p.SubCategoryId == s && p.IsPet == false).ToList();
                        foreach (var p in subCate)
                        {
                            CProductShow Cpro = new CProductShow();
                            Cpro.product = p;
                            if (p.Photos.Any())
                                Cpro.Photos = p.Photos.ToList();
                            //if (!list.Contains(Cpro))
                            list.Add(Cpro);
                        }
                    }
                    //if (key.Keyword != null)
                    //{
                    //    var parCate = db.Products.Where(p => p.ProductName.Contains(key.Keyword) && p.IsPet == false).ToList();
                    //    foreach (var p in parCate)
                    //    {
                    //        CProductShow Cpro = new CProductShow();
                    //        Cpro.product = p;
                    //        if (p.Photos.Any())
                    //            Cpro.Photos = p.Photos.ToList();
                    //        if (!list.Contains(Cpro))
                    //            list.Add(Cpro);
                    //    }
                    //}
                }
                return PartialView(list);
            }
        }
        //public IActionResult ShowProduct1(string id)
        //{
        //    CProductShow CprodShow = new CProductShow();
        //    Product cProd = (new 我救浪Context()).Products.FirstOrDefault(p => p.ProductId == id);
        //    CprodShow.product = cProd;
        //    if (cProd.Photos.Any())
        //        CprodShow.Photos = cProd.Photos.ToList();
        //    int proId = int.Parse(id);
        //    return RedirectToAction("ShowProduct", proId);
        //}
        public IActionResult ShowProduct(int id)
        {
            CProductShow CprodShow = new CProductShow();
            Product cProd = (new 我救浪Context()).Products.FirstOrDefault(p => p.ProductId == id);
            CprodShow.product = cProd;
            if (cProd.Photos.Any())
                CprodShow.Photos = cProd.Photos.ToList();
            return View(CprodShow);
        }
        [HttpPost]
        public IActionResult ShowProduct(CShoppingCart vModel)
        {
            string jsonCart = "";
            List<CShoppingCart> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_購物車商品列表))
                list = new List<CShoppingCart>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_購物車商品列表);
                list = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);
            }
            CProductShow Cprod = new CProductShow();
            Product Prod = (new 我救浪Context()).Products.FirstOrDefault(p => p.ProductId == vModel.CartId);
            Cprod.product = Prod;
            Cprod.Photos = (new 我救浪Context()).Photos.Where(p => p.ProductId == Prod.ProductId).ToList();
            if (list.Where(p => p.CartId.Equals(vModel.CartId)).Any())
            {
                CShoppingCart item = list.Where(p => p.CartId == vModel.CartId).First();
                item.CartCount = item.CartCount + vModel.CartCount;
                item.Donate = item.Donate + vModel.Donate;
            }
            else
            {
                list.Add(vModel);
            }
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(
                CDictionary.SK_購物車商品列表, jsonCart);


            return View(Cprod);
        }
        public IActionResult CartView()
        {
            return View();
        }
        public IActionResult CartTable(string hidCartId, string hidCount, bool hidIsDonate)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_購物車商品列表))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_購物車商品列表);
                List<CShoppingCart> cart = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);

                if (!string.IsNullOrEmpty(hidCartId) && !string.IsNullOrEmpty(hidCount))
                {
                    if (hidIsDonate)
                        cart.Where(x => x.CartId.Equals(Convert.ToInt32(hidCartId))).First().Donate = Convert.ToInt32(hidCount);
                    else
                        cart.Where(x => x.CartId.Equals(Convert.ToInt32(hidCartId))).First().CartCount = Convert.ToInt32(hidCount);
                    if (cart.Where(x => x.CartId.Equals(Convert.ToInt32(hidCartId))).First().CartCount == 0 && cart.Where(x => x.CartId.Equals(Convert.ToInt32(hidCartId))).First().Donate == 0)
                    {
                        cart.Remove(cart.Where(x => x.CartId.Equals(Convert.ToInt32(hidCartId))).First());
                    }
                    HttpContext.Session.SetString(
                        CDictionary.SK_購物車商品列表, JsonSerializer.Serialize(cart));
                }


                return PartialView(cart);
            }
            else
            {
                List<CShoppingCart> cart = new List<CShoppingCart>();
                return PartialView(cart);
            }

        }
        public IActionResult Pay(List<CShoppingCart> item)
        {
            if (item.Any())
            {
                List<CShoppingCart> list = new List<CShoppingCart>();
                foreach (var p in item)
                {
                    CShoppingCart cProd = new CShoppingCart();
                    var prod = db.Products.First(c => c.ProductId == p.CartId);
                    cProd.CartCount = p.CartCount;
                    cProd.CartId = p.CartId;
                    cProd.CartPrice = (decimal)prod.Price;
                    cProd.Donate = p.Donate;
                    cProd.DonatePay = p.DonatePay;
                    cProd.CartPay = p.CartPay;
                    cProd.CartName = prod.ProductName;
                    if (db.Photos.Any(c => c.ProductId == p.CartId && c.IsDefault == true))
                    {
                        cProd.CartPhoto = db.Photos.First(c => c.ProductId == p.CartId && c.IsDefault == true).PictureName;
                    }
                    list.Add(cProd);
                }
                return View(list);
            }
            else
                return RedirectToAction("CartView");
        }
        public IActionResult SubCategory(int id)
        {
            var db = new 我救浪Context();
            if (id == 0)
            {
                var list = db.SubCategories.Where(p => p.Category.IsPet == false).ToList();
                List<CSubCategoryView> Clist = new List<CSubCategoryView>();
                foreach (var subCate in list)
                {
                    CSubCategoryView cSub = new CSubCategoryView();
                    cSub.SubCategoryName = subCate.SubCategoryName;
                    cSub.SubCategoryId = subCate.SubCategoryId;
                    Clist.Add(cSub);
                }
                var jsonList = JsonSerializer.Serialize(Clist);
                return Json(jsonList);
            }

            else
            {
                var list = db.SubCategories.Where(p => p.CategoryId == id).ToList();
                List<CSubCategoryView> Clist = new List<CSubCategoryView>();
                foreach (var subCate in list)
                {
                    CSubCategoryView cSub = new CSubCategoryView();
                    cSub.SubCategoryName = subCate.SubCategoryName;
                    cSub.SubCategoryId = subCate.SubCategoryId;
                    Clist.Add(cSub);
                }
                var jsonList = JsonSerializer.Serialize(Clist);
                return Json(jsonList);
            }
        }
        public IActionResult Favorite(int ProductId)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                string jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
                if (jsonUser != null)
                {
                    CLoginViewModel cart = JsonSerializer.Deserialize<CLoginViewModel>(jsonUser);
                    Member mem = db.Members.FirstOrDefault(m => m.HgenderId == cart.MemberID);
                    var favorites = db.MyFavorites.ToList();
                    if (!db.MyFavorites.Where(p => p.ProductId == ProductId && p.MemberId == cart.MemberID).Any())
                    {
                        MyFavorite myFav = new MyFavorite();
                        myFav.MemberId = cart.MemberID;
                        myFav.ProductId = ProductId;
                        db.MyFavorites.Add(myFav);
                        db.SaveChanges();
                        return Content("已新增產品至收藏清單");
                    }
                    else
                    {
                        return Content("產品已在收藏清單中");
                    }

                }
                else
                {
                    return Content("請先登入會員");
                }

            }
            else
            {
                return Content("請先登入會員");
            }
        }
        public IActionResult FavoriteList()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                var jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
                if (jsonUser != null)
                {
                    var User = JsonSerializer.Deserialize<CLoginViewModel>(jsonUser);
                    var list = db.Products.Where(p => p.IsPet == false && p.MyFavorites.Where(f => f.MemberId == User.MemberID).Any()).ToList();
                    List<CProductShow> data = new List<CProductShow>();
                    foreach (var p in list)
                    {
                        CProductShow cProd = new CProductShow();
                        cProd.product = p;
                        if (db.Photos.Where(c => c.ProductId == p.ProductId).Any())
                        {
                            cProd.Photos = db.Photos.Where(c => c.ProductId == p.ProductId).ToList();
                        }
                        data.Add(cProd);
                    }
                    return View(data);
                }
                return Content("請先登入會員");
            }
            else
            {
                return Content("請先登入會員");
            }

        }
        public IActionResult createOrder(string address)
        {
            var jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            var user = JsonSerializer.Deserialize<CLoginViewModel>(jsonUser);
            Order or = new Order();
            or.EmployeeId = 1;
            or.MemberId = user.MemberID;
            or.OrderDate = DateTime.Now.Date;
            or.SendAddress = address;
            or.OrderStatusId = 2;
            db.Orders.Add(or);
            db.SaveChanges();
            var id = db.Orders.First(p => p.OrderDate == or.OrderDate && p.MemberId == or.MemberId && p.SendAddress == or.SendAddress).OrderId;
            return Content(or.OrderId.ToString());
        }
        public IActionResult createOrderDetail(string orderId, List<CShoppingCart> data)
        {
            foreach (var p in data)
            {
                if (p.CartPay == true)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId =int.Parse( orderId);
                    od.ProductId = p.CartId;
                    od.Quantity = p.CartCount;
                    od.UnitPrice = db.Products.First(c => c.ProductId == p.CartId).Price;
                    od.IsDonate = false;
                    db.OrderDetails.Add(od);
                }
                if (p.DonatePay == true)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = int.Parse(orderId);
                    od.ProductId = p.CartId;
                    od.Quantity = p.Donate;
                    od.UnitPrice = db.Products.First(c => c.ProductId == p.CartId).Price;
                    od.IsDonate = true;
                    db.OrderDetails.Add(od);
                }
               
            } 
            db.SaveChanges();
                return Content("訂單已成立");
        }
    }
}
