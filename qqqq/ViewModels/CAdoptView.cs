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




        public CAdoptView()
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


        //性別--
        public string GenderType
        {
            get { return _prod.PetDetail.Gender.GenderType; }
        }

        //顏色--
        public string ColorName
        {
            get { return _prod.PetDetail.Color.ColorName; }
        }

        //城市--
        public string CityName
        {
            get { return _prod.PetDetail.City.CityName; }
        }

        //體型 -- 
        public string SizeType
        {
            get { return _prod.PetDetail.Size.SizeType; }
        }

        //年紀--
        public string AgeType
        {
            get { return _prod.PetDetail.Age.AgeType; }
        }

        //結紮--
        public string LigationType
        {
            get { return _prod.PetDetail.Ligation.LigationType; }
        }



        //category --
        [DisplayName("類別")]
        public string CategoryName
        {
            get { return _prod.SubCategory.Category.CategoryName; }
        }
        //subcategory --
        [DisplayName("品種")]
        public string SubCategoryName
        {
            get { return _prod.SubCategory.SubCategoryName; }
        }
        //product
        public int ProductId
        {
            get { return _prod.ProductId; }
        }
        [DisplayName("名字")]
        public string ProductName
        {
            get { return _prod.ProductName; }
        }
        [DisplayName("描述")]
        public string Description
        {
            get { return _prod.Description; }
        }
        public int? UnitsInStock
        {
            get { return _prod.UnitsInStock; }
        }
        [DisplayName("領養狀態")]
        public bool? Continued
        {
            get { return _prod.Continued; }
        }
        public decimal? Cost
        {
            get { return _prod.Cost; }
        }
        //petdetail
        [DisplayName("年花費")]
        public decimal? YearCost
        {
            get { return _prod.PetDetail.YearCost; }
        }
        [DisplayName("空間")]
        public int? Space
        {
            get { return _prod.PetDetail.Space; }
        }
        [DisplayName("週陪伴時間")]
        public int? AccompanyTimeWeek
        {
            get { return _prod.PetDetail.AccompanyTimeWeek; }
        }

        public List<Photo> Photos { get; set; }
    }
}
