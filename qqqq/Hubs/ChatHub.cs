using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void AddConnDist(string selfID)
        {
            if(ConnDict.ContainsKey(selfID)==false)
            {
                ConnDict[selfID] = Context.ConnectionId;
            }
            else
            {
                throw new Exception("ConnDist已有此selfID");
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

                // 發送人
                await Clients.Client(ConnDict[selfID]).SendAsync("SendMessage", sendToID, message);
                if (ConnDict.ContainsKey(sendToID))
                {
                    // 接收人
                    await Clients.Client(ConnDict[sendToID]).SendAsync("ReceiveMessage", selfID, message);
                }
                //存入資料庫
            }
        }
        public void SaveMseeage(string selfID, string message, string sendToID)
        {

            if(selfID.Contains("member")|| sendToID.Contains("member"))
            {
                MsgEmpAndMem meam = new MsgEmpAndMem();
                meam.Mseeage = message;
                meam.MsgTime = DateTime.Now;
                if (selfID.Contains("member"))
                {

                    meam.IsMemSend = true;
                    meam.MemberId = int.Parse(selfID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(sendToID.Split('/')[1]);
                }
                else if (sendToID.Contains("member"))
                {
                    meam.IsMemSend = false;
                    meam.MemberId = int.Parse(selfID.Split('/')[1]);
                    meam.EmployeeId = int.Parse(sendToID.Split('/')[1]);
                }
            }
            else
            {
                MsgEmpToEmp mete = new MsgEmpToEmp();
                mete.Message = message;
                mete.MsgTime = DateTime.Now;
                
            }
        }

    }
}
