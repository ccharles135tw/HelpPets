using Microsoft.AspNetCore.Mvc;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace qqqq.Controllers
{
    public class shoppingController : Controller
    {

        public IActionResult shopping()
        {
            var db = new 我救浪Context();
            List<Category> list = db.Categories.Where(p => p.IsPet == false).ToList();
            return View(list);
        }
        public IActionResult shoppingRow(CShoppingKeyword key)
        {
            我救浪Context db = new 我救浪Context();
            if (key.ParentCategory == 0 && key.Category == 0 && key.SubCategory == 0 && key.Keyword ==null)
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
                var list= db.Products.Where(p => p.IsPet == false).ToList();
                if (key.Category != 0)
                {
                    list = list.Where(p => p.SubCategory.CategoryId == key.Category).ToList();
                }
                if (key.SubCategory != 0)
                {
                    list = list.Where(p => p.SubCategoryId == key.SubCategory).ToList();
                }
                if (!string.IsNullOrEmpty(key.Keyword.Trim()))
                {
                    list= list.Where(p => p.ProductName.Contains(key.Keyword)).ToList();
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
                if (key.ParentCategory != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategory.Category.ParentCategory == key.ParentCategory && p.IsPet == false).ToList();
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
                if (key.Category != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategory.CategoryId == key.Category && p.IsPet == false).ToList();
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
                if (key.SubCategory != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategoryId == key.SubCategory && p.IsPet == false).ToList();
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

            我救浪Context db = new 我救浪Context();
            if (key.ParentCategory == 0 && key.Category == 0 && key.SubCategory == 0 && key.Keyword == null)
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
                if (key.SubCategory != 0)
                {
                    list = list.Where(p => p.SubCategoryId == key.SubCategory).ToList();
                }
                if (!string.IsNullOrEmpty(key.Keyword.Trim()))
                {
                    list=  list.Where(p => p.ProductName.Contains(key.Keyword)).ToList();
                }
                List<CProductShow> data = new List<CProductShow>();
                foreach(var p in list)
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
                if (key.ParentCategory != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategory.Category.ParentCategory == key.ParentCategory && p.IsPet == false).ToList();
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
                if (key.Category != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategory.CategoryId == key.Category && p.IsPet == false).ToList();
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
                if (key.SubCategory != 0)
                {
                    var parCate = db.Products.Where(p => p.SubCategoryId == key.SubCategory && p.IsPet == false).ToList();
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
            //CShoppingCart item = new CShoppingCart()
            //{
            //    CartId = vModel.CartId,
            //    CartName = Prod.ProductName,
            //    CartPhoto = Cprod.Photos[0].PictureName,
            //    CartCount = vModel.CartCount,
            //    CartPrice = (decimal)Prod.Price
            //};
            list.Add(vModel);
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(
                CDictionary.SK_購物車商品列表, jsonCart);


            return View(Cprod);
        }
        public IActionResult CartView()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_購物車商品列表))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_購物車商品列表);

                List<CShoppingCart> cart = JsonSerializer.Deserialize<List<CShoppingCart>>(jsonCart);
                return View(cart);
            }
            else
                return RedirectToAction("Shopping");
        }
        public IActionResult Pay(int? price)
        {
            if (price != null)
            {
                return View((int)price);
            }
            else
                return RedirectToAction("CartView");
        }
        public IActionResult SubCategory(int id)
        { 
            var db = new 我救浪Context();
            if (id ==0)
            {
                var list = db.SubCategories.Where(p=>p.Category.IsPet==false).ToList();
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
        
            else {        
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
                return Json(jsonList); }
        }
    }
}
