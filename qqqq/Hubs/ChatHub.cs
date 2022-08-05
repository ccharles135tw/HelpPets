using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qqqq.ViewModels;
using System.Diagnostics;

namespace qqqq.Hubs
{
    public class ChatHub : Hub
    {
        // 用戶連線 ID 列表
        //key為member/1或employee/1，value為ConnectionID
        public static Dictionary<string, string> ConnDict = new Dictionary<string, string>();
        我救浪Context db = new 我救浪Context();

        /// 連線事件
        public override async Task OnConnectedAsync()
        {

            //if (ConnDict.ContainsKey(Context.ConnectionId)==false)
            //{
            //    ConnDict.Add(Context.ConnectionId, null);
            //}

            // 更新連線 ID 狀態  其他人會看到他上線
            //string jsonString = JsonConvert.SerializeObject(ConnIDList);
            //await Clients.All.SendAsync("UpdList", jsonString);

            // 更新個人 ID
            //await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfConnectionID", Context.ConnectionId);

            await base.OnConnectedAsync();
        }
        public async void  AddConnDist(string selfID)
        {
            if(ConnDict.ContainsKey(selfID)==false)
            {
                ConnDict[selfID] = Context.ConnectionId;
                string j = System.Text.Json.JsonSerializer.Serialize(ConnDict.Keys.ToArray());
                await Clients.Client(Context.ConnectionId).SendAsync("UpdSelf",j);
                await Clients.All.SendAsync("UpListOn", selfID);
            }
            else
            {
                Debug.WriteLine("ConnDist已有此selfID");

                ConnDict.Remove(selfID);
                ConnDict.Add(selfID,Context.ConnectionId);
                
                //await Clients.Client(Context.ConnectionId).SendAsync("RepeatLogin");
                //throw new Exception("重複登入ㄌ87");
            }
        }
        /// <summary>
                /// 離線事件
                /// </summary>
                /// <param name="ex"></param>
                /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
           if(ConnDict.ContainsValue(Context.ConnectionId))
            {
                
                string key = ConnDict.Where(cd => cd.Value == Context.ConnectionId).FirstOrDefault().Key;
                ConnDict.Remove(key);
                await Clients.All.SendAsync("UpListOff", key);
                
            }
           else
            {
                throw new Exception();
            }
            // 更新連線 ID 狀態  其他人會看到他下線
            //string jsonString = JsonConvert.SerializeObject(ConnIDList);
            //await Clients.All.SendAsync("UpdList", jsonString);


            await base.OnDisconnectedAsync(ex);
        }

        /// <summary>
                /// 傳遞訊息
                /// </summary>
                /// <param name="user"></param>
                /// <param name="message"></param>
                /// <param name="id"></param>
                /// <returns></returns>
        public async Task SendMessage(string selfID, string message, string sendToID)
        {
            if (string.IsNullOrEmpty(sendToID))
            {
                throw new Exception("sendToID為空") ;
            }
            else
            {
                MessageView msgV = IntoMessageView(selfID, message, sendToID);
                string j = System.Text.Json.JsonSerializer.Serialize(msgV);
                // 發送人
                await Clients.Client(ConnDict[selfID]).SendAsync("SendMessage", sendToID, j);
                if (ConnDict.ContainsKey(sendToID))
                {
                    // 接收人
                    await Clients.Client(ConnDict[sendToID]).SendAsync("ReceiveMessage", selfID, j);
                }
                //存入資料庫
                if(selfID.Contains("random")==false&& sendToID.Contains("random")==false)
                {
                    SaveMseeage(selfID, message, sendToID);
                }

            }
        }
        public async Task UpIsRead(string selfID,string SendID)
        {
            int sfID = int.Parse(selfID.Split('/')[1]);
            int sdID = int.Parse(SendID.Split('/')[1]);
            if (selfID.Contains("member") || SendID.Contains("member"))
            {
                if(selfID.Contains("member"))
                {
                    var q = db.MsgEmpAndMems.Where(m => m.IsMemSend == false && m.MemberId == sfID && m.EmployeeId == sdID&&m.IsReceiveRead==false).ToList();
                    foreach(var m in q)
                    {
                        m.IsReceiveRead = true;
                    }
                }
                else
                {
                    var q = db.MsgEmpAndMems.Where(m => m.IsMemSend == true && m.MemberId == sdID && m.EmployeeId == sfID && m.IsReceiveRead == false).ToList();
                    foreach (var m in q)
                    {
                        m.IsReceiveRead = true;
                    }
                }
            }
            else
            {
                var q = db.MsgEmpToEmps.Where(m => m.IsReceiveRead == false && m.EmpSendId ==sdID && m.EmpReceiveId == sfID).ToList();
                foreach (var m in q) m.IsReceiveRead = true;
            }
            db.SaveChanges();
            await Clients.Clients(ConnDict[SendID]).SendAsync("UpIsRead", selfID);
        }
        public MessageView IntoMessageView(string selfID, string message, string sendToID)
        {
            if (selfID.Contains("member") || sendToID.Contains("member"))
            {
                MsgEmpAndMem meam = new MsgEmpAndMem();
                meam.Mseeage = message;
                meam.MsgTime = DateTime.Now;
                meam.IsReceiveRead = false;
                if (selfID.Contains("member"))
                {
                    meam.IsMemSend = true;
                    meam.MemberId = int.Parse(selfID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(sendToID.Split('/')[1]);
                    return new MessageView(meam);
                }
                else
                {
                    meam.IsMemSend = false;
                    meam.MemberId = int.Parse(sendToID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(selfID.Split('/')[1]);
                    return new MessageView(meam);
                }

            }
            else
            {
                MsgEmpToEmp mete = new MsgEmpToEmp();
                mete.Message = message;
                mete.MsgTime = DateTime.Now;
                mete.EmpSendId = int.Parse(selfID.Split('/')[1]);
                mete.EmpReceiveId = int.Parse(sendToID.Split('/')[1]);
                mete.IsReceiveRead = false;
                return new MessageView(mete);
            }
        }
        public void SaveMseeage(string selfID, string message, string sendToID)
        {

            if(selfID.Contains("member")|| sendToID.Contains("member"))
            {
                MsgEmpAndMem meam = new MsgEmpAndMem();
                meam.Mseeage = message;
                meam.MsgTime = DateTime.Now;
                meam.IsReceiveRead = false;
                if (selfID.Contains("member"))
                {
                    meam.IsMemSend = true;
                    meam.MemberId = int.Parse(selfID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(sendToID.Split('/')[1]);
                }
                else if (sendToID.Contains("member"))
                {
                    meam.IsMemSend = false;
                    meam.MemberId = int.Parse(sendToID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(selfID.Split('/')[1]);
                }
                
                db.MsgEmpAndMems.Add(meam);
            }
            else
            {
                MsgEmpToEmp mete = new MsgEmpToEmp();
                mete.Message = message;
                mete.MsgTime = DateTime.Now;
                mete.EmpSendId = int.Parse(selfID.Split('/')[1]);
                mete.EmpReceiveId = int.Parse(sendToID.Split('/')[1]);
                mete.IsReceiveRead = false;
                db.MsgEmpToEmps.Add(mete);
            }
            db.SaveChanges();
        }

    }
}
