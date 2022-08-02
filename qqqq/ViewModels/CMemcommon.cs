using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CMemcommon
    {
        public Product _prod;

        public CMemcommon()
        {
            _prod = new Product();
        }
        public CMemcommon(Product p)
        {
            _prod = p;
        }

        static public List<CMemfavortView> CMemfavortViewS(List<Product> list_p)
        {
            List<CMemfavortView> list = new List<CMemfavortView>();
            foreach (var p in list_p)
            {
                CMemfavortView cMemfavort = new CMemfavortView(p);
                list.Add(cMemfavort);
            }
            return list;
        }

        //public 
    }
}
