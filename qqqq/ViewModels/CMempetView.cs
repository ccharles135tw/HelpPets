using qqqq.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjHomeLess.ViewModel
{
    public class CMempetView
    {
        public Order _order;


        public CMempetView()
        {
            _order = new Order();
        }
        public CMempetView(Order o)
        {
            _order = o;
        }
        static public List<CMempetView> CMempetViews(List<Order> list_o)
        {
            List < CMempetView > list= new List<CMempetView>();
            foreach(var o in list_o)
            {
                CMempetView cMempetView = new CMempetView(o);
                list.Add(cMempetView);
            }
            return list;
        }

        public int OrderId { get { return _order.OrderId; } }

        public string PictureName { get { return _order.OrderDetails.FirstOrDefault().Product.Photos.Where(ph => ph.IsDefault == true).Count() > 0 ? _order.OrderDetails.FirstOrDefault().Product.Photos.Where(ph => ph.IsDefault == true).FirstOrDefault().PictureName : null; } }

        public string ProductName{get{ return _order.OrderDetails.Count>0? _order.OrderDetails.FirstOrDefault().Product.ProductName:null; } }

        public string Gender { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Gender.GenderType; } }

        public string Lagition { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Ligation.LigationType; } }

        public string OrderStaus { get { return _order.OrderStatus.StatusType; } }

        public string OrderDate { get { return ((DateTime)_order.OrderDate).ToString("yyyy/MM/dd"); } }

        public int ProductId { get { return _order.OrderDetails.FirstOrDefault().ProductId; } }
        public string Color { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Color.ColorName; } }
        public int YearCost { get { return (int)_order.OrderDetails.FirstOrDefault().Product.PetDetail.YearCost; } }
        public int Space { get { return (int)_order.OrderDetails.FirstOrDefault().Product.PetDetail.Space; } }
        public string Size { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Size.SizeType; } }
        public string Age { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Age.AgeType; } }
        public string Description { get { return _order.OrderDetails.FirstOrDefault().Product.PetDetail.Description; } }
    }
}
