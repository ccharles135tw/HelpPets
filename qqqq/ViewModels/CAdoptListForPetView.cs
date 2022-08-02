using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using qqqq.Models;

namespace qqqq.ViewModels
{
    public class CAdoptListForPetView
    {
        public Order order;
        public CAdoptListForPetView(Order o)
        {
            order = o;
        }
        static public List<CAdoptListForPetView> CAdoptListForPetViews(List<Order> list_o)
        {
            List<CAdoptListForPetView> list = new List<CAdoptListForPetView>();
            foreach (var o in list_o)
            {
                CAdoptListForPetView cAdoptListForPetView = new CAdoptListForPetView(o);
                list.Add(cAdoptListForPetView);
            }
            return list;
        }
        [DisplayName("會員名稱")]
        public string MemberName { get { return order.Member.Name; } }
        [DisplayName("手機號碼與信箱")]
        public string MemberContact { get { return order.Member.MemberPhone + "\n" + order.Member.Email; } }
        public string MemberPhone { get { return order.Member.MemberPhone; } }
        public string MemberEmail { get { return order.Member.Email; } }
        public int MemeberID { get { return (int)order.MemberId; } }

    }
}
