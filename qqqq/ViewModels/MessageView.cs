using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.Models;

namespace qqqq.ViewModels
{
    public class MessageView
    {
        MsgEmpAndMem meam = null;
        MsgEmpToEmp mete = null;
        public MessageView(MsgEmpAndMem m)
        {
            meam = m;
        }
        public MessageView(MsgEmpToEmp m)
        {
            mete = m;
        }
        static public List<MessageView> MessageViews(List<MsgEmpToEmp> list_m)
        {
            List<MessageView> list = new List<MessageView>();
            foreach(var m in list_m)
            {
                MessageView messageView = new MessageView(m);
                list.Add(messageView);
            }
            return list;
        }
        static public List<MessageView> MessageViews(List<MsgEmpAndMem> list_m)
        {
            List<MessageView> list = new List<MessageView>();
            foreach (var m in list_m)
            {
                MessageView messageView = new MessageView(m);
                list.Add(messageView);
            }
            return list;
        }


        public string selfID { get { 
                if(meam!=null)
                {
                    if (meam.IsMemSend == true) return $"member/{meam.MemberId}";
                    else return $"employee/{meam.EmployeeId}";
                }
                else
                {
                    return $"employee/{mete.EmpSendId}";
                }
            } }
        public string clientID { get {
                if (meam!=null)
                {
                    if (meam.IsMemSend == true) return $"employee/{meam.EmployeeId}";
                    else return $"member/{meam.MemberId}";
                }
                else
                {
                    return $"employee/{mete.EmpReceiveId}";
                }
            }
        }
    
        public string Message { get { return meam != null ? meam.Mseeage : mete.Message; } }
        public DateTime? MsgTime { get { return meam != null ? meam.MsgTime : mete.MsgTime; } }
    }
}
