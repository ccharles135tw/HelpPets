using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CAdoptView
    {

        public Product product;
        MemberWish memberWish;




        public CAdoptView(Product p)
        {
            product = p;
        }
        public CAdoptView(Product p ,MemberWish mw)
        {
            product = p;
            memberWish = mw;

        }
        public static List<CAdoptView> CAdoptViews(List<Product> listproductuct)
        {
            List<CAdoptView> list = new List<CAdoptView>();
            foreach(var p in listproductuct)
            {
                CAdoptView cAdoptView = new CAdoptView(p);
                list.Add(cAdoptView);
            }
            return list;
        }


        //性別--
        public string GenderType
        {
            get { return product.PetDetail.Gender.GenderType; }
        }

        //顏色--
        public string ColorName
        {
            get { return product.PetDetail.Color.ColorName; }
        }

        //城市--
        public string CityName
        {
            get { return product.PetDetail.City.CityName; }
        }

        //體型 -- 
        public string SizeType
        {
            get { return product.PetDetail.Size.SizeType; }
        }

        //年紀--
        public string AgeType
        {
            get { return product.PetDetail.Age.AgeType; }
        }

        //結紮--
        public string LigationType
        {
            get { return product.PetDetail.Ligation.LigationType; }
        }



        //category --
        [DisplayName("類別")]
        public string CategoryName
        {
            get { return product.SubCategory.Category.CategoryName; }
        }
        //subcategory --
        [DisplayName("品種")]
        public string SubCategoryName
        {
            get { return product.SubCategory.SubCategoryName; }
        }
        //product
        public int ProductId
        {
            get { return product.ProductId; }
        }
        [DisplayName("名字")]
        public string ProductName
        {
            get { return product.ProductName; }
        }
        [DisplayName("描述")]
        public string Description
        {
            get { return product.Description; }
        }
        public int? UnitsInStock
        {
            get { return product.UnitsInStock; }
        }
        [DisplayName("領養狀態")]
        public bool? Continued
        {
            get { return product.Continued; }
        }
        public decimal? Cost
        {
            get { return product.Cost; }
        }
        //petdetail
        [DisplayName("年花費")]
        public decimal? YearCost
        {
            get { return product.PetDetail.YearCost; }
        }
        [DisplayName("空間")]
        public int? Space
        {
            get { return product.PetDetail.Space; }
        }
        [DisplayName("週陪伴時間")]
        public int? AccompanyTimeWeek
        {
            get { return product.PetDetail.AccompanyTimeWeek; }
        }
        public int MatchScore
        {
           get
           {
                int score = 0;
                if (product.SubCategory.CategoryId != memberWish.CategoryId && memberWish.CategoryId != 1) return 0;
                if (product.SubCategoryId == memberWish.SubCategoryId || memberWish.SubCategoryId == 1) score += 30;
                if (product.PetDetail.GenderId == memberWish.GenderId || memberWish.GenderId == 1) score += 20;
                if (product.PetDetail.ColorId == memberWish.ColorId || memberWish.ColorId == 1) score += 10;
                if (product.PetDetail.CityId == memberWish.CityId || memberWish.CityId == 1) score += 5;
                if (product.PetDetail.SizeId == memberWish.SizeId || memberWish.SizeId == 1) score += 10;
                if (product.PetDetail.AgeId == memberWish.AgeId || memberWish.AgeId == 1) score += 10;
                if (product.PetDetail.LigationId == memberWish.LigationId || memberWish.LigationId == 1) score += 15;
                //if (memberWish.YearCost >= ((decimal)product.PetDetail.YearCost) * 0.8m) score += 5;
                //if (memberWish.AccompanyTimeWeek >= (decimal)product.PetDetail.AccompanyTimeWeek * 0.8m) score += 5;
                //if (memberWish.Space >= (decimal)product.PetDetail.Space * 0.8m) score += 5;
                return score;
            }
            

        }

    }
}
