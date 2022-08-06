using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CMemcreatcommondView
    {
        public Product _prod;

        public CMemcreatcommondView()
        {
            _prod = new Product();
        }
        public CMemcreatcommondView(Product p)
        {
            _prod = p;
        }

        static public List<CMemcreatcommondView> CMemcreatcommondViews(List<Product> list_p)
        {
            List<CMemcreatcommondView> list = new List<CMemcreatcommondView>();
            foreach (var p in list_p)
            {
                CMemcreatcommondView cMemcreatcommond = new CMemcreatcommondView(p);
                list.Add(cMemcreatcommond);
            }
            return list;
        }
       

       
        public string PictureName { get { return _prod.Photos.FirstOrDefault().PictureName; } }
        public int MyfavID { get { return _prod.MyFavorites.FirstOrDefault().MyFavorite1; } }

        public string ProductName { get { return _prod.ProductName; } }
        public int ProductID { get { return _prod.ProductId; } }
        public decimal Unprice { get { return (decimal)_prod.Price; } }

        public int Unstock { get { return (int)_prod.UnitsInStock; } }

        public string describe { get { return _prod.Description; } }

        public  string RateS { get { return (_prod.MemberComments.Count()==0?"尚未有評價":(Math.Ceiling((double)_prod.MemberComments.Average(mc => mc.Grade))).ToString()); } }

        public  double Rate { get { return ((double)_prod.MemberComments.Count() == 0 ? 0 : Math.Ceiling((double)_prod.MemberComments.Average(mc => mc.Grade))); } }
    }
}

           