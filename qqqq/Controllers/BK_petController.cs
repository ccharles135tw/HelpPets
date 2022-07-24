using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHomeLess.ViewModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class BK_petController : Controller
    {
        public IActionResult NewPetList()
        {
            我救浪Context db = new 我救浪Context();
            var subcate = db.SubCategories.Select(x => x).ToList();
            var petdetail = db.PetDetails.Select(x => x).ToList();
            var cate = db.Categories.Select(x => x).ToList();
            var size = db.Sizes.Select(x => x).ToList();
            var age = db.Ages.Select(x => x).ToList();
            var city = db.Cities.Select(x => x).ToList();
            var color = db.Colors.Select(x => x).ToList();
            var gender = db.Genders.Select(x => x).ToList();
            var ligation = db.Ligations.Select(x => x).ToList();
            var datas = from p in db.Products
                        where p.IsPet == true
                        select p;
            List<CPet> list = new List<CPet>();
            foreach (var p in datas)
            {
                CPet pet = new CPet();
                pet._prod = p;
                pet._subCategory = subcate.Where(x => x.SubCategoryId == p.SubCategoryId).FirstOrDefault();
                pet._petDetail = petdetail.Where(x => x.ProductId == p.ProductId).FirstOrDefault();
                pet._category = cate.Where(x => x.CategoryId == pet._subCategory.CategoryId).FirstOrDefault();
                pet._size = size.Where(x => x.SizeId == pet.PetDetail.SizeId).FirstOrDefault();
                pet._age = age.Where(x => x.AgeId == pet.PetDetail.AgeId).FirstOrDefault();
                pet._city = city.Where(x => x.CityId == pet.PetDetail.CityId).FirstOrDefault();
                pet._color = color.Where(x => x.ColorId == pet.PetDetail.ColorId).FirstOrDefault();
                pet._gender = gender.Where(x => x.GenderId == pet.PetDetail.GenderId).FirstOrDefault();
                pet._ligation = ligation.Where(x => x.LigationId == pet.PetDetail.LigationId).FirstOrDefault();
                list.Add(pet);
            }
            return View(list);

        }

        ////主類別
        //public IActionResult petList()
        //{
        //    我救浪Context db = new 我救浪Context();
        //    var datas = from p in db.Products
        //                where p.IsPet == true
        //                select p;
        //    List<CPet> list = new List<CPet>();
        //    foreach (Product p in datas)
        //    {
        //        CPet pet = new CPet();
        //        pet.Product = p;
        //        list.Add(pet);
        //    }
        //    return View(list);
        //}

        //新增浪浪
        public IActionResult NewCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewCreate(CPet p)
        {
            try
            {
                我救浪Context db = new 我救浪Context();
                //pet = new CPet();
                Product pro = new Product();
                PetDetail detail = new PetDetail();


                //cate.CategoryId= sub.CategoryId = db.Categories.Where(x => x.CategoryName == p.CategoryName).Select(y => y.CategoryId).FirstOrDefault();

                pro.SubCategoryId = int.Parse(p.SubCategoryName);
                pro.ProductName = p.ProductName;
                pro.Price = 0;
                pro.SupplierId = 1;
                pro.IsPet = true;
                pro.Description = p.Description;
                pro.UnitsInStock = 0;
                pro.Continued = true;
                pro.Cost = 0;
                db.Products.Add(pro);
                db.SaveChanges();

                //System.Diagnostics.Debug.WriteLine(p.ColorName);
                //System.Diagnostics.Debug.WriteLine(db.Colors.Where(x => x.ColorName == p.ColorName).Select(y => y.ColorId).FirstOrDefault());

                detail.ProductId = p.ProductId;
                detail.ProductId = db.Products.Select(x => x.ProductId).ToList().Last();
                detail.Description = p.Description;
                detail.YearCost = p.YearCost;
                detail.Space = p.Space;
                detail.AccompanyTimeWeek = p.AccompanyTimeWeek;
                detail.CityId = int.Parse(p.CityName);
                detail.ColorId = int.Parse(p.ColorName);
                detail.GenderId = int.Parse(p.GenderType);
                detail.SizeId = int.Parse(p.SizeType);
                detail.LigationId = int.Parse(p.LigationType);
                detail.AgeId = int.Parse(p.AgeType);

                //System.Diagnostics.Debug.WriteLine(detail.ProductId.ToString(), detail.YearCost.ToString(), detail.AccompanyTimeWeek.ToString());
                //db.Sizes.Add(size);

                db.PetDetails.Add(detail);
                db.SaveChanges();

                //db.Products.Add(pet.Product);
                //db.SaveChanges();
                //db.PetDetails.Add(pet.PetDetail);
                //db.SaveChanges();
                return RedirectToAction("NewPetList");
            }
            catch (Exception)
            {
                return RedirectToAction("NewCreate");
            }

        }

        //刪除浪浪
        public IActionResult Delete(int? id)
        {
            我救浪Context db = new 我救浪Context();

            PetDetail petDetail = db.PetDetails.FirstOrDefault(p => p.ProductId == id);
            if (petDetail != null)
            {
                db.PetDetails.Remove(petDetail);
                db.SaveChanges();
                Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                return RedirectToAction("NewPetList");
            }
            return RedirectToAction("NewPetList");

        }


        //修改浪浪
        public IActionResult NewEdit(int? id)
        {
            CPet cPet = new CPet();
            我救浪Context db = new 我救浪Context();
            PetDetail petDetail = db.PetDetails.FirstOrDefault(p => p.ProductId == id);
            cPet._petDetail = petDetail; 
            Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            cPet._prod = product;
            if (product == null || petDetail == null)
                return RedirectToAction("NewPetList");
            return View(cPet);
        }
        [HttpPost]
        public IActionResult NewEdit(Product pd)
        {
            我救浪Context db = new 我救浪Context();
            Product product = db.Products.FirstOrDefault(p => p.ProductId == pd.ProductId);
            if (product != null)
            {
                product.ProductName = pd.ProductName;
                product.Description = pd.Description;
                db.SaveChanges();
            }
            return RedirectToAction("petList");
        }
    }
}

