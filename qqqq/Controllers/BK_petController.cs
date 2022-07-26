using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHomeLess.ViewModel;
using qqqq.Models;
using qqqq.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.Controllers
{
    public class BK_petController : Controller
    {

        public IWebHostEnvironment _environment;
        public BK_petController(IWebHostEnvironment pet)
        {
            _environment = pet;
        }

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
                        where p.IsPet == true && p.Continued == true
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
        //已下架浪浪
        public IActionResult DiscountinueList()
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
                        where p.IsPet == true && p.Continued == false
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

      


        //新增浪浪
        //todo Demo
        public IActionResult NewCreate()
        {
            return View();
        }
        [HttpPost]

        public IActionResult NewCreate(Product pro, PetDetail detail, IFormFile file)
        {
            //try
            //{
            我救浪Context db = new 我救浪Context();

            pro.Price = 0;
            pro.SupplierId = 1;
            pro.IsPet = true;
            pro.UnitsInStock = 0;
            pro.Continued = true;
            pro.Cost = 0;
            db.Products.Add(pro);
            db.SaveChanges();

            //System.Diagnostics.Debug.WriteLine(p.ColorName);
            //System.Diagnostics.Debug.WriteLine(db.Colors.Where(x => x.ColorName == p.ColorName).Select(y => y.ColorId).FirstOrDefault());
           
            
            //detail.ProductId = db.Products.Select(x => x.ProductId).ToList().Last();
            detail.ProductId = pro.ProductId;
            
            db.PetDetails.Add(detail);
            db.SaveChanges();

            //System.Diagnostics.Debug.WriteLine(detail.ProductId.ToString(), detail.YearCost.ToString(), detail.AccompanyTimeWeek.ToString());
            //db.Sizes.Add(size);


            //照片
            if (file != null)
            {
                Photo photo = new Photo();
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                file.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                photo.PictureName = photoName;
                photo.ProductId = pro.ProductId;
                photo.IsDefault = true;
                db.Photos.Add(photo);
                db.SaveChanges();
            }
            return RedirectToAction("NewPetList");
            //}
            //catch (Exception)
            //{
            //return RedirectToAction("NewCreate");
            //}

        }

        //刪除浪浪
        //todo 刪除前面會有衝突
        public IActionResult Delete(int? id)
        {
            try
            {
                我救浪Context db = new 我救浪Context();
                Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == id);
                PetDetail petDetail = db.PetDetails.FirstOrDefault(p => p.ProductId == id);
                Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null && petDetail != null && photo != null)
                {
                    db.Photos.Remove(photo);
                    db.SaveChanges();

                    db.PetDetails.Remove(petDetail);
                    db.SaveChanges();

                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                return RedirectToAction("NewPetList");
            }
            catch (Exception)
            {
                return RedirectToAction("NewPetList");
            }

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
            Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == id);
            cPet._photo = photo;
            if (product == null || petDetail == null || photo == null)
                return RedirectToAction("NewPetList");
            return View(cPet);
        }
        [HttpPost]
        public IActionResult NewEdit(Product pro,PetDetail detail,IFormFile file)
        {
            我救浪Context db = new 我救浪Context();
            //pet = new CPet();
            var q = db.PetDetails.FirstOrDefault(p => p.ProductId == detail.ProductId);
            if (q != null)
            {
                //detail.ProductId = db.Products.Select(x => x.ProductId).ToList().Last();
                q.Description = detail.Description;
                q.YearCost = detail.YearCost;
                q.Space = detail.Space;
                q.AccompanyTimeWeek = detail.AccompanyTimeWeek;
                q.CityId = detail.CityId;
                q.ColorId = detail.ColorId;
                q.GenderId = detail.GenderId;
                q.SizeId = detail.SizeId;
                q.LigationId = detail.LigationId;
                q.AgeId = detail.AgeId;
                db.SaveChanges();
            }

            var qpro = db.Products.FirstOrDefault(p => p.ProductId == pro.ProductId);
            if (qpro != null)
            {
                //pro.SubCategoryId = int.Parse(cPet.SubCategoryName);
                //System.Diagnostics.Debug.WriteLine("TEST");
                //System.Diagnostics.Debug.WriteLine(cPet.SubCategoryName);
                //pro.SubCategory.SubCategoryName = cPet.SubCategoryName;
                //product.SubCategoryId = pp.SubCategoryId;
                qpro.SubCategoryId = pro.SubCategoryId;
                //qpro.SubCategory.SubCategoryName = pro.SubCategory.SubCategoryName;

                qpro.ProductName = pro.ProductName;
                qpro.Description = pro.Description;
                db.SaveChanges();
            }

            Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == pro.ProductId);
            if (photo != null && file != null)
            {
                Photo ph = new Photo();
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                file.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                ph.PictureName = photoName;
                ph.ProductId = pro.ProductId;
                ph.IsDefault = true;
                db.Photos.Remove(db.Photos.Where(ph => ph.ProductId == pro.ProductId).FirstOrDefault());
                db.Photos.Add(ph);
                db.SaveChanges();
            }
            return RedirectToAction("NewPetList");
        }



        //下架浪浪
        public IActionResult Discontinued(int? id)
        {

            我救浪Context db = new 我救浪Context();
            Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == id);
            PetDetail petDetail = db.PetDetails.FirstOrDefault(p => p.ProductId == id);
            Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null && petDetail != null && photo != null)
            {
                product.Continued = false;
                db.SaveChanges();
            }
            return RedirectToAction("NewPetList");
        }


        //上架浪浪
        public IActionResult Continued(int? id)
        {

            我救浪Context db = new 我救浪Context();
            Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == id);
            PetDetail petDetail = db.PetDetails.FirstOrDefault(p => p.ProductId == id);
            Product product = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null && petDetail != null && photo != null)
            {
                product.Continued = true;
                db.SaveChanges();
            }
            return RedirectToAction("DiscountinueList");
        }

        //關鍵字
        public IActionResult KeyWord(string keyword)
        {
            我救浪Context db = new 我救浪Context();
            List<CPet> datas = null;
            ViewBag.keyword = keyword;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = (from p in db.Products
                         where p.IsPet == true && p.Continued == true
                         select new CPet(p)).ToList();
            }
            else
            {
                datas = (db.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).
                    Where(p => p.IsPet == true
                    && p.Continued==true&&(p.ProductName.Contains(keyword) ||
                      (p.IsPet == true && p.SubCategory.SubCategoryName.Contains(keyword)) ||
                     (p.IsPet == true && p.SubCategory.Category.CategoryName.Contains(keyword)))
                ).Select(p => new CPet(p))).ToList();
            }

            return View("NewPetList", datas);
        }
    }


}

