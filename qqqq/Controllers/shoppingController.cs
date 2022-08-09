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
using Pet.ViewModels;
using System.Collections;
using AllPay.Payment.Integration;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Security.Cryptography;

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

                var datas = db.Products.Where(p => p.IsPet == false&&p.Continued==true).ToList();
                List<CProductShow> list = new List<CProductShow>();
                foreach (Product p in datas)
                {
                    CProductShow cprod = new CProductShow();
                    cprod.product = p;
                    if (p.Photos.Any())
                        cprod.Photos = p.Photos.ToList();
                    list.Add(cprod);
                }
                return PartialView(list.OrderByDescending(f=>f.ProductId));
            }
            else if (key.Search == false)
            {
                var list = db.Products.Where(p => p.IsPet == false && p.Continued == true).ToList();
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
                return PartialView(data.OrderByDescending(f => f.ProductId));
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
                        var subCate = db.Products.Where(p => p.SubCategoryId == s && p.IsPet == false && p.Continued == true).ToList();
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
                    var parCate = db.Products.Where(p => p.ProductName.Contains(key.Keyword) && p.IsPet == false && p.Continued == true).ToList();
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

                return PartialView(list.OrderByDescending(f => f.ProductId));
            }
        }


        public IActionResult shoppingCol(CShoppingKeyword key)
        {


            if (key.ParentCategory == 0 && key.Category == 0 && key.SubCategory == null && key.Keyword == null)
            {

                var datas = db.Products.Where(p => p.IsPet == false && p.Continued == true).ToList();
                List<CProductShow> list = new List<CProductShow>();
                foreach (Product p in datas)
                {
                    CProductShow cprod = new CProductShow();
                    cprod.product = p;
                    if (p.Photos.Any())
                        cprod.Photos = p.Photos.ToList();
                    list.Add(cprod);
                }
                return PartialView(list.OrderByDescending(f => f.ProductId));
            }
            else if (key.Search == false)
            {
                var list = db.Products.Where(p => p.IsPet == false && p.Continued == true).ToList();
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
                return PartialView(data.OrderByDescending(f => f.ProductId));
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
                        var subCate = db.Products.Where(p => p.SubCategoryId == s && p.IsPet == false && p.Continued == true).ToList();
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
                return PartialView(list.OrderByDescending(f => f.ProductId));
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
                var jsonList = JsonSerializer.Serialize(list);
                HttpContext.Session.SetString(
              CDictionary.SK_結帳商品, jsonList);
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
                    od.OrderId = int.Parse(orderId);
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

        public IActionResult CartCount()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_購物車商品列表))
            {
                var jsonCart = HttpContext.Session.GetString(CDictionary.SK_購物車商品列表);
                var cart = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);
                return Content(cart.Count().ToString());
            }
            else
            {
                return Content("0");
            }
        }
        public IActionResult getMember()
        {
            var jsonUser = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            var user = JsonSerializer.Deserialize<CLoginViewModel>(jsonUser);
            var member = db.Members.First(p => p.MemberId == user.MemberID && p.Email == user.Email);
            CMemberView cMember = new CMemberView();
            cMember.Member = member;
            var data = JsonSerializer.Serialize(cMember);
            return Json(data);
        }

        public ActionResult AllPay()
        {

            string jsonCart = HttpContext.Session.GetString(CDictionary.SK_結帳商品);
            List<CShoppingCart> cart = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);

            return View(cart);
        }

        public ActionResult CleanCart()
        {
            string jsonPay = HttpContext.Session.GetString(CDictionary.SK_結帳商品);
            List<CShoppingCart> pay = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonPay);
            string jsonCart = HttpContext.Session.GetString(CDictionary.SK_購物車商品列表);
            List<CShoppingCart> cart = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);
            foreach (var p in pay)
            {
                if (p.CartPay && p.DonatePay)
                {
                    var delete = cart.First(c => c.CartId == p.CartId);
                    cart.Remove(delete);
                }
                else
                {
                    var set0 = cart.First(c => c.CartId == p.CartId);
                    if (p.CartPay)
                    {
                        set0.CartCount = 0;
                        set0.CartPay = false;
                    }
                    if (p.DonatePay)
                    {
                        set0.Donate = 0;
                        set0.DonatePay = false;
                    }
                    if (!set0.CartPay && !set0.DonatePay)
                    {
                        cart.Remove(set0);
                    }
                }
            }
            jsonCart = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CDictionary.SK_購物車商品列表, jsonCart);
            HttpContext.Session.Remove(CDictionary.SK_結帳商品);
            return Ok();
        }
        //public ActionResult AllPayConfirm()
        //{
        //    var result = RequestLinePayAsync(1);

        //    if (result.returnCode == "0000")
        //    {
        //        return Redirect(result.info.paymentUrl.web);
        //    }
        //    else
        //    {
        //        return Content(result.returnMessage);
        //    }

        //}


        //public async Task RequestLinePayAsync(int amount)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var channelId = "1657365624";
        //        var channelSecret = "f40f1a66398283c11759b9f66f4b3f18";
        //        var baseUri = @"https://sandbox-api-pay.line.me";
        //        var apiUrl = @"/v3/payments/request";
        //        string nonce = Guid.NewGuid().ToString();
        //        var requestBody = new LinePayRequest()
        //        {
        //            amount = amount,
        //            currency = "TWD",
        //            orderId = Guid.NewGuid().ToString(),
        //            redirectUrls = new Redirecturls()
        //            {
        //                confirmUrl = "https://6ddcf789.ngrok.io/confitmUrl",
        //                //cancelUrl = "https://6ddcf789.ngrok.io/cancelUrl"
        //            },
        //            packages = new List<Package>()
        //            {
        //                new Package()
        //                {
        //                    id = "package-1",
        //                    name = "DOG",
        //                    amount = amount,
        //                    products = new List<ViewModels.LineProduct>()
        //                    {
        //                        new ViewModels.LineProduct()
        //                        {
        //                            id = "prod-1",
        //                            name = "Dog",
        //                            quantity = 1,
        //                            price = amount
        //                        }
        //                    }
        //                }
        //            }
        //        };
        //        var body = JsonSerializer.Serialize(requestBody);

        //        string Signature = HashLinePayRequest(channelSecret, apiUrl, body, nonce, channelSecret);

        //        //var request = (HttpWebRequest)WebRequest.Create(baseUri + apiUrl);
        //        //request.Method = "POST";
        //        //request.ContentType = "application/json";
        //        httpClient.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
        //        httpClient.DefaultRequestHeaders.Add("X-LINE-ChannelSecret", channelSecret);
        //        httpClient.DefaultRequestHeaders.Add("X-LINE-Authorization-Nonce", nonce);
        //        httpClient.DefaultRequestHeaders.Add("X-LINE-Authorization", Signature);

        //        var content = new StringContent(body, Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync(baseUri + apiUrl, content);
        //        var result = await response.Content.ReadAsStringAsync();
        //        //var response = (HttpWebResponse)request.GetResponse();
        //        //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //      JsonSerializer.Deserialize<LinePayResponse>(result);
        //    }
        //}
        //public async Task<IActionResult> OnPostSubmitAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    var result=await RequestLinePayAsync(this.am)
        //}


        //private static string HashLinePayRequest(string channelSecret, string apiUrl, string body, string orderId, string key)

        //{

        //    var request = channelSecret + apiUrl + body + orderId;

        //    key = key ?? "";

        //    var encoding = new System.Text.UTF8Encoding();

        //    byte[] keyByte = encoding.GetBytes(key);

        //    byte[] messageBytes = encoding.GetBytes(request);

        //    using (var hmacsha256 = new HMACSHA256(keyByte))

        //    {

        //        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

        //        return Convert.ToBase64String(hashmessage);
        //    }
        //}
    }
}

