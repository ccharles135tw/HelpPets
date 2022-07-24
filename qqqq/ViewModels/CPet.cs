using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjHomeLess.ViewModel
{
    public class CPet
    {

        public Category _category;
        public SubCategory _subCategory;
        public Product _prod;
        public PetDetail _petDetail;
        public Gender _gender;
        public Color _color;
        public City _city;
        public Size _size;
        public Age _age;
        public Ligation _ligation;
        public Photo _photo;
        
        
        

        public CPet()
        {
            _category = new Category();
            _subCategory = new SubCategory();
            _prod = new Product();
            _petDetail = new PetDetail();
            _gender = new Gender();
            _color = new Color();
            _city = new City();
            _size = new Size();
            _age = new Age();
            _ligation = new Ligation();
            _photo = new Photo();
            
        }
        //public CPet(Product product)
        //{
        //    Product = product;
        //}


        //貓或狗
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        //品種
        public SubCategory SubCategory
        {
            get { return _subCategory; }
            set { _subCategory = value; }
        }

        //名字
        public Product Product
        {
            get { return _prod; }
            set { _prod = value; }
        }

        //毛色....等細項資料
        public PetDetail PetDetail
        {
            get { return _petDetail; }
            set { _petDetail = value; }
        }

        //性別
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        //顏色
        public Color Color
        {
            get { return _color;}
            set { _color = value;}
        }

        //城市
        public City City
        {
            get { return _city;}
            set { _city = value; }
        }
        
        //體型
        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        //年紀
        public Age Age
        {
            get { return _age; }
            set { _age = value; }
        }

        //結紮
        public Ligation Ligation
        {
            get { return _ligation; }
            set { _ligation = value; }
        }



        //category
        public int CategoryId
        {
            get { return _category.CategoryId; }
            set { _category.CategoryId = value; }
        }
        [DisplayName("類別")]
        public string CategoryName
        {
            get { return _category.CategoryName; }
            set { _category.CategoryName = value; }
        }
        public bool? IsPet
        {
            get { return _category.IsPet; }
            set { _category.IsPet = value; }
        }
        //subcategory
        public int SubCategoryId 
        {
                get { return _subCategory.SubCategoryId; }
                set { _subCategory.SubCategoryId = value; }
        }
        [DisplayName("品種")]
        public string SubCategoryName
        {
            get { return _subCategory.SubCategoryName; }
            set { _subCategory.SubCategoryName = value; }
        }
        //product
        public int ProductId
        {
            get { return _prod.ProductId; }
            set { _prod.ProductId = value; }
        }
        [DisplayName("名字")]
        public string ProductName
        {
            get { return _prod.ProductName; }
            set { _prod.ProductName = value; }
        }
        public decimal? Price
        {
            get { return _prod.Price; }
            set { _prod.Price = value; }
        }
        [DisplayName("描述")]
        public string Description
        {
            get { return _prod.Description; }
            set { _prod.Description = value; }
        }
        public int? UnitsInStock
        {
            get { return _prod.UnitsInStock; }
            set { _prod.UnitsInStock = value; }
        }
        [DisplayName("領養狀態")]
        public bool? Continued
        {
            get { return _prod.Continued; }
            set { _prod.Continued = value; }
        }
        public decimal? Cost
        {
            get { return _prod.Cost; }
            set { _prod.Cost = value; }
        }
        //petdetail
        //gender
        public int? GenderId
        {
            get { return _petDetail.GenderId; }
            set { _petDetail.GenderId = value; }
        }
        [DisplayName("性別")]
        public string GenderType
        {
            get { return _gender.GenderType; }
            set { _gender.GenderType = value; }
        }
        //color
        public int? ColorId
        {
            get { return _petDetail.ColorId; }
            set { _petDetail.ColorId = value; }
        }
        [DisplayName("毛色")]
        public string ColorName
        {
            get { return _color.ColorName; }
            set { _color.ColorName = value; }
        }
        //city
        public int? CityId
        {
            get { return _petDetail.CityId; }
            set { _petDetail.CityId = value; }
        }
        [DisplayName("城市")]
        public string CityName
        {
            get { return _city.CityName; }
            set { _city.CityName = value; }
        }
        [DisplayName("年花費")]
        public decimal? YearCost
        {
            get { return _petDetail.YearCost; }
            set { _petDetail.YearCost = value; }
        }
        [DisplayName("空間")]
        public int? Space
        {
            get { return _petDetail.Space; }
            set { _petDetail.Space = value; }
        }

        //size
        public int? SizeId
        {
            get { return _petDetail.SizeId; }
            set { _petDetail.SizeId = value; }
        }
        [DisplayName("體型")]
        public string SizeType
        {
            get { return _size.SizeType; }
            set { _size.SizeType = value; }
        }

        //age
        public int? AgeId
        {
            get { return _petDetail.AgeId; }
            set { _petDetail.AgeId = value; }
        }
        [DisplayName("年紀")]
        public string AgeType
        {
            get { return _age.AgeType; }
            set { _age.AgeType = value; }
        }

        [DisplayName("週陪伴時間")]
        public int? AccompanyTimeWeek
        {
            get { return _petDetail.AccompanyTimeWeek; }
            set { _petDetail.AccompanyTimeWeek = value; }
        }

        //ligation
        public int? LigationId
        {
            get { return _petDetail.LigationId; }
            set { _petDetail.LigationId = value; }
        }
        [DisplayName("結紮")]
        public string LigationType
        {
            get { return _ligation.LigationType; }
            set { _ligation.LigationType = value; }
        }
        public List<Photo> Photos { get; set; }




        //[DisplayName("序號")]
        //public int ProductId
        //{
        //    get { return _prod.ProductId; }
        //    set { _prod.ProductId = value; }
        //}
        //[DisplayName("名稱")]
        //public string ProductName
        //{
        //    get { return _prod.ProductName; }
        //    set { _prod.ProductName = value; }
        //}
        //public int SubCategoryId { get { return _prod.SubCategoryId; } set { _prod.SubCategoryId = value; } }
        //public decimal? Price { get { return _prod.Price; } set { _prod.Price = value; } }
        //public int? SupplierId { get { return _prod.SupplierId; } set { _prod.SupplierId = value; } }
        //public bool? IsPet { get { return _prod.IsPet; } set { _prod.IsPet = value; } }



        //[DisplayName("描述")]
        //public string Description
        //{
        //    get { return _prod.Description; }
        //    set { _prod.Description = value; }
        //}
        //public int? UnitsInStock { get { return _prod.UnitsInStock; } set { _prod.UnitsInStock = value; } }

        //[DisplayName("領養狀態")]
        //public bool? Continued
        //{
        //    get { return _prod.Continued; }
        //    set { _prod.Continued = value; }
        //}
        //public decimal? Cost { get { return _prod.Cost; } set { _prod.Cost = value; } }

        //[DisplayName("品種")]
        //public string SubCategoryName
        //{
        //    get { return _prod.SubCategory.SubCategoryName; }
        //    set { _prod.SubCategory.SubCategoryName = value; }
        //}
        //[DisplayName("類別")]
        //public string CategoryName
        //{
        //    get { return _prod.SubCategory.Category.CategoryName; }
        //    set { _prod.SubCategory.Category.CategoryName = value; }
        //}

    }
}
