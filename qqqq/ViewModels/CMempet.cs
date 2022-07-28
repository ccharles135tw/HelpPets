using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjHomeLess.ViewModel
{
    public class CMempet
    {
        public Order _order;
        public OrderDetail _orderDetail;
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
        
        
        

        public CMempet()
        {
            _order = new Order();
            _orderDetail = new OrderDetail();
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
        public CMempet(Product p)
        {
            _prod = p;

        }
        //public CPet(Product product)
        //{
        //    Product = product;
        //}

        //照片
     
        public int OrderDetail {
            get { return _orderDetail.OrderId; }
        }
        public Photo Photo
        {
            get
            {
                return _prod.Photos.FirstOrDefault();
            }
        }
        //貓或狗
        public Category Category
        {
            get { return _prod.SubCategory.Category; }
        }

        //品種
        public SubCategory SubCategory
        {
            get { return _prod.SubCategory; }
        }

        //名字
        public Product Product
        {
            get { return _prod; }
          
        }

        //毛色....等細項資料
        public PetDetail PetDetail
        {
            get { return _prod.PetDetail;}
        }

        //性別
        public Gender Gender
        {
            get { return _prod.PetDetail.Gender;}
        }

        //顏色
        public Color Color
        {
            get { return _prod.PetDetail.Color; }
        }

        //城市
        public City City
        {
            get { return _prod.PetDetail.City;}
        }
        
        //體型
        public Size Size
        {
            get { return _prod.PetDetail.Size; }
        }

        //年紀
        public Age Age
        {
            get { return _prod.PetDetail.Age; }
        }

        //結紮
        public Ligation Ligation
        {
            get { return _prod.PetDetail.Ligation; }
        }




        //photo
        public int PictureId
        {
            get { return _prod.Photos.FirstOrDefault().PictureId; }
        }

        [DisplayName("圖片")]
        public byte[] Picture
        {
            get { return _prod.Photos.FirstOrDefault().Picture; }
        }
        public string PictureName
        {
            get { return _prod.Photos.FirstOrDefault().PictureName; }
        }
        public bool? IsDefault
        {
            get { return _prod.Photos.FirstOrDefault().IsDefault; }
        }


        //category
        public int CategoryId
        {
            get { return _prod.SubCategory.Category.CategoryId; }
        }
        [DisplayName("類別")]
        public string CategoryName
        {
            get { return _prod.SubCategory.Category.CategoryName; }
        }
        public bool? IsPet
        {
            get { return _prod.SubCategory.Category.IsPet; }
        }
        //subcategory
        public int SubCategoryId
        {
            get { return _prod.SubCategory.SubCategoryId; }
        }
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
        public decimal? Price
        {
            get { return _prod.Price; }
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
        //gender
        public int? GenderId
        {
            get { return _prod.PetDetail.GenderId; }
        }
        [DisplayName("性別")]
        public string GenderType
        {
            get { return _prod.PetDetail.Gender.GenderType; }
        }
        //color
        public int? ColorId
        {
            get { return _prod.PetDetail.Color.ColorId; }
        }
        [DisplayName("毛色")]
        public string ColorName
        {
            get { return _prod.PetDetail.Color.ColorName; }
        }
        //city
        public int? CityId
        {
            get { return _prod.PetDetail.CityId; }
        }
        [DisplayName("城市")]
        public string CityName
        {
            get { return _prod.PetDetail.City.CityName; }
        }
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

        //size
        public int? SizeId
        {
            get { return _prod.PetDetail.SizeId; }
        }
        [DisplayName("體型")]
        public string SizeType
        {
            get { return _prod.PetDetail.Size.SizeType; }
        }

        //age
        public int? AgeId
        {
            get { return _prod.PetDetail.AgeId; }
        }
        [DisplayName("年紀")]
        public string AgeType
        {
            get { return _prod.PetDetail.Age.AgeType; }
        }

        [DisplayName("週陪伴時間")]
        public int? AccompanyTimeWeek
        {
            get { return _prod.PetDetail.AccompanyTimeWeek; }
        }

        //ligation
        public int? LigationId
        {
            get { return _prod.PetDetail.LigationId; }
        }
        [DisplayName("結紮")]
        public string LigationType
        {
            get { return _prod.PetDetail.Ligation.LigationType; }
        }
        public List<Photo> Photos { get; set; }




       



      

    }
}
