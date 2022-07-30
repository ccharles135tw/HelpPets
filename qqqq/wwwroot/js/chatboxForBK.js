

$(document).ready(() =>
{
  
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
    })
})
var selfID = $("#divEmpString").text();
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//與Server建立連線
connection.start().then(function ()
{
    connection.invoke("AddConnDist", selfID).then(() => { console.log("Hub 連線完成"); });

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



//傳送訊息
$('.send-btn').on('click', function ()
{
    let message = $(this).siblings(":text").val();
     // let sendToID = $('.chats:visible').attr("clientID");
    let sendToID = $(".chat-btn").attr("clientID");
    $(this).siblings(":text").val("");
    connection.invoke("SendMessage", selfID, message, sendToID).catch(function (err)
    {
        alert('傳送錯誤: ' + err.toString());
    });
});
//傳送信息事件
connection.on("SendMessage", function (clientID, msg)
{
    $(`.chats[clientID='${clientID}']`).eq(0).append(`<div class="my-chat">${msg}</div>`);
});
// 接受訊息事件
connection.on("ReceiveMessage", function (clientID, msg)
{
    if ($(`.chats[clientID='${clientID}']`).length == 0)
    {

        let name = null;
        $.post("/Product/GetNameForChatBox", { userString: clientID }, function (data)
        {
            console.log(data);
            $(".ul-client").append(`<li clientID="${clientID}">${data}</li>`)
            $(".client").after(`<div class="chats" clientID="${clientID}"><div class="client-chat">${msg}</div></div>`)
         })
    }
    else
    {
        $(`.chats[clientID='${clientID}']`).eq(0).append(`<div class="client-chat">${msg}</div>`);
        //$("ul-client").append(`<li clientID="${clientID}">ChatBox</li>`)
    }
});
//點擊li切換對象
$(".ul-client").on("click", "li", function ()
{
    let clientID = $(this).attr("clientID");
    let clientName = $(this).text();
    ClickToClient(clientID, clientName);
    $(".chat-btn").attr("clientID", clientID);
})

function ClickToClient(clientID,clientName)
{
  
    $(".chats").hide();
    $("#clientName").text(`${clientName}`);
    if ($(`.chats[clientID='${clientID}']`).length == 0)
    {
        $(".client").after(`<div class="chats" clientID="${clientID}"></div>`)
    }
    else
    {
        $(`.chats[clientID='${clientID}']`).show();
    }
}