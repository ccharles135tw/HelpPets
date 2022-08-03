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

     public string ActPhotoName { get { return _vou.Activity.ActivityPhoto; }}

        public string HelpDiscription { get { return _vou.Activity.Description; } }

        public string AllowDate { get { return _vou.AllowDate; } }

        public string AllowTime { get { return _vou.AllowTime.TimeRange; } }

        public string ActTital { get { return _vou.Activity.Title; } }

        public string Vstaus { get { return _vou.Vstatus.StatusType; } }

        //public int TotalTime { get { return _vou.AllowT} }

        
    }
}
