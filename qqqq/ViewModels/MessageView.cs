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
        public string MsgTime { get { return meam != null ? ((DateTime)meam.MsgTime).ToString("yyyy/MM/dd/HH:mm:ss") : ((DateTime)mete.MsgTime).ToString("yyyy/MM/dd/HH:mm:ss"); } }
        public string IsReceiveRead { get { 
                if(meam!=null)
                {
                    return meam.IsReceiveRead == true ? "已讀" : "未讀";
                }
                else
                {
                    return mete.IsReceiveRead == true ? "已讀" : "未讀";
                }
            } }
    }
}
