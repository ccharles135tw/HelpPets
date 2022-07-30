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
        //key回connectionID，value為member/1或employee/1
        public static Dictionary<string, string> ConnDict = new Dictionary<string, string>();
        我救浪Context db = new 我救浪Context();

        /// 連線事件
        public override async Task OnConnectedAsync()
        {

            if (ConnDict.ContainsKey(Context.ConnectionId)==false)
            {
                ConnDict.Add(Context.ConnectionId, null);
            }

            // 更新連線 ID 狀態  其他人會看到他上線
            //string jsonString = JsonConvert.SerializeObject(ConnIDList);
            //await Clients.All.SendAsync("UpdList", jsonString);

            // 更新個人 ID
            //await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfConnectionID", Context.ConnectionId);

            await base.OnConnectedAsync();
        }
        public void AddValueInConnDist(string selfID)
        {
            if(ConnDict.ContainsKey(Context.ConnectionId))
            {
                ConnDict[Context.ConnectionId] = selfID;
            }
            else
            {
                throw new Exception("ConnDist無此ConnectionID");
            }
        }
        /// <summary>
                /// 離線事件
                /// </summary>
                /// <param name="ex"></param>
                /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
           if(ConnDict.ContainsKey(Context.ConnectionId))
            {
                ConnDict.Remove(Context.ConnectionId);
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
                if(ConnDict.ContainsValue(sendToID))
                {
                    // 接收人
                    await Clients.Client(sendToID).SendAsync("ReceiveMessage", selfID, message);

                    // 發送人
                    await Clients.Client(Context.ConnectionId).SendAsync("SendMessage",sendToID,message);
                }
                //存入資料庫
            }
        }

    }
}
