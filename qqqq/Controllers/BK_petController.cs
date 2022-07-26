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
        //todo 上架問題
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
        //todo 照片新增修改
        //todo Demo
        public IActionResult NewCreate()
        {
            return View();
        }
        [HttpPost]

        public IActionResult NewCreate(CPet p, IFormFile file)
        {
            try
            {
                我救浪Context db = new 我救浪Context();
                Product pro = new Product();
                PetDetail detail = new PetDetail();

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

                detail.ProductId = pro.ProductId;
                //detail.ProductId = db.Products.Select(x => x.ProductId).ToList().Last();
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
            }
            catch (Exception)
            {
                return RedirectToAction("NewCreate");
            }

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
        public IActionResult NewEdit(CPet cPet, IFormFile file)
        {
            我救浪Context db = new 我救浪Context();
            //pet = new CPet();

            PetDetail detail = db.PetDetails.FirstOrDefault(p => p.ProductId == cPet.ProductId);
            if (detail != null)
            {
                //detail.ProductId = db.Products.Select(x => x.ProductId).ToList().Last();
                detail.Description = cPet.Description;
                detail.YearCost = cPet.YearCost;
                detail.Space = cPet.Space;
                detail.AccompanyTimeWeek = cPet.AccompanyTimeWeek;
                detail.CityId = int.Parse(cPet.CityName);
                detail.ColorId = int.Parse(cPet.ColorName);
                detail.GenderId = int.Parse(cPet.GenderType);
                detail.SizeId = int.Parse(cPet.SizeType);
                detail.LigationId = int.Parse(cPet.LigationType);
                detail.AgeId = int.Parse(cPet.AgeType);
                db.SaveChanges();
            }

            Product pro = db.Products.FirstOrDefault(p => p.ProductId == cPet.ProductId);
            if (pro != null)
            {
                //pro.SubCategoryId = int.Parse(cPet.SubCategoryName);
                System.Diagnostics.Debug.WriteLine("TEST");
                System.Diagnostics.Debug.WriteLine(cPet.SubCategoryName);
                //pro.SubCategory.SubCategoryName = cPet.SubCategoryName;
                //product.SubCategoryId = pp.SubCategoryId;
                pro.SubCategoryId = int.Parse(cPet.SubCategoryName);
                pro.ProductName = cPet.ProductName;
                pro.Description = cPet.Description;
                db.SaveChanges();
            }

            Photo photo = db.Photos.FirstOrDefault(p => p.ProductId == cPet.ProductId);
            if (photo != null && file != null)
            {
                Photo ph = new Photo();
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                file.CopyTo(new FileStream(_environment.WebRootPath + "/Images/" + photoName, FileMode.Create));
                ph.PictureName = photoName;
                ph.ProductId = pro.ProductId;
                ph.IsDefault = true;
                db.Photos.Remove(db.Photos.Where(ph => ph.ProductId == cPet.ProductId).FirstOrDefault());
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
            return RedirectToAction("NewPetList");
        }

        //關鍵字

    }

    
}

