

$(document).ready(() =>
{
  
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
    })
})
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//與Server建立連線
connection.start().then(function ()
{
    connection.invoke("AddValueInConnDist", $(".chat-btn").attr("selfID"));
    console.log("Hub 連線完成");
}).catch(function (err)
{
    alert('連線錯誤: ' + err.toString());
});

// 更新連線 ID 列表事件
connection.on("UpdList", function (jsonList)
{
});

// 更新用戶個人連線 ID 事件
connection.on("UpdSelfID", function (id)
{

});

// 更新聊天內容事件
connection.on("ReceiveMessage", function (clientID,msg)
{
    if ($(`.chats[clientID='${clientID}']`).length == 0)
    {
        $(".chat-box").after(`<div class="chats" clientID="${clientID}"><div class="client-chat">${msg}</div></div>`)
    }
    else
    {
        $(`.chats[clientID='${clientID}']`).eq(0).append(`<div class="client-chat">${msg}</div>`);
    }
});

//傳送訊息
$('.send-btn').on('click', function ()
{
    let selfID = $(this).attr("selfID");
    let message = $(this).siblings(":text").val();
    let sendToID = $('.chats :visible').attr("clientID");
    connection.invoke("SendMessage", selfID, message, sendToID).catch(function (err)
    {
        alert('傳送錯誤: ' + err.toString());
    });
});

//傳送信息時
connection.on("SendMessage", function (clientID, msg)
{
    $(`.chats[clientID='${clientID}']`).eq(0).append(`<div class="my-chat">${msg}</div>`);
});