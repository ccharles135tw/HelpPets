using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CMemvoltView
    {
        Volunteer _vou;

    public    CMemvoltView() {
            _vou = new Volunteer();
        
        }
        public CMemvoltView(Volunteer v)
        {
            _vou = v;
        }
        static public List<CMemvoltView> CMemvoltViews(List<Volunteer> list_v)
        {
            List<CMemvoltView> list = new List<CMemvoltView>();
            foreach (var v in list_v)
            {
                CMemvoltView cMemvoltView = new CMemvoltView(v);
                list.Add(cMemvoltView);
            
            }
            return list;
        }

     

    }
}
