

$(document).ready(() =>
{
  
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
        $(".chat-btn").removeClass("breath");
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
    if ($(`.ul-client li[clientID="${clientID}"]`).length == 0)//ul沒有就加入
    {
        $.post("/Product/GetNameForChatBox", { userString: clientID }, function (data)
        {
            $(".ul-client").append(`<li clientID="${clientID}" class="blink">${data}</li>`);
            $(`.ul-client li[clientID="${clientID}"]`).addClass("blink");
        })
    }
    if ($(".chat-btn").attr("clientID") != clientID)//判斷目前的訊息對象是否為傳訊息的
    {
        $(`.ul-client li[clientID="${clientID}"]`).addClass("blink");//目前的訊息對象並非現在傳訊息的，加入閃爍效果
    }
    if ($(".chat-box ").is(":hidden"))
    {
        $(".chat-btn").addClass("breath");
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
    $(this).removeClass("blink");//已讀>>移除閃爍
    let clientID = $(this).attr("clientID");
    let clientName = $(this).text();
    ClickToClient(clientID, clientName);
    $(".chat-btn").attr("clientID", clientID);//更改目前的訊息對象
})

function ClickToClient(clientID,clientName)
{
    console.log(1);
    $(".chats").hide();
    $("#clientName").text(`TO:${clientName}`);
    if ($(`.chats[clientID='${clientID}']`).length == 0)
    {
        console.log(2);
        $(".client").after(`<div class="chats" clientID="${clientID}"></div>`)
        $.post("/Product/GetMessageForChat",{ selfID: selfID, clientID: clientID }, function (datas)
        {
            datas = JSON.parse(datas);
            console.log(datas)
            let chatbox = $(`.chats[clientID="${clientID}"] `);
            for (let m of datas)
            {
                if (m.selfID == selfID)
                {
                    chatbox.append(`<div class="my-chat">${m.Message}</br>${m.MsgTime}</div>`);
                }
                else if (m.selfID == clientID)
                {
                    chatbox.append(`<div class="client-chat">${m.Message}</br>${m.MsgTime}</div>`);
                }
            }
        })
    }
    else
    {
        console.log(3);
        $(`.chats[clientID='${clientID}']`).show();
    }
}