﻿
$(document).ready(() =>
{
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
        $(".chat-btn").removeClass("breath");
    })
})

var selfID = $("#divMemString").text();
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//與Server建立連線
connection.start().then(function ()
{
    connection.invoke("AddConnDist", selfID).then(() => { console.log("Hub 連線完成"); });

}).catch(function (err)
{
    alert('連線錯誤: ' + err.toString());
});

connection.on("UpdSelf", function (jsonList)
{
    jsonList = JSON.parse(jsonList);
    for (let j of jsonList)
    {
        $(`.ul-client li[clientID="${j}"] `).addClass("online")
    }
});
connection.on("UpListOn", function (id)
{
    $(`.ul-client li[clientID="${id}"] `).addClass("online")
});
connection.on("UpListOff", function (id)
{

    $(`.ul-client li[clientID="${id}"] `).removeClass("online")
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
connection.on("SendMessage", function (clientID, j)
{
    j = JSON.parse(j)
    let chatbox = $(`.chats[clientID="${clientID}"] `);
    $(`.chats[clientID='${clientID}']`).eq(0).append(`<div class="my-chat">${j.Message}</br>${j.MsgTime}`);
    chatbox.scrollTop(chatbox.prop("scrollHeight"));
});
// 接受訊息事件
connection.on("ReceiveMessage", function (clientID, j)
{
    j = JSON.parse(j)
    if ($(`.ul-client li[clientID="${clientID}"]`).length == 0)//ul沒有就加入
    {
        $.post("/Product/GetNameForChatBox", { userString: clientID }, function (data)
        {
            $(".ul-client").append(`<li clientID="${clientID}" class="blink online">${data}</li>`);
          //  $(`.ul-client li[clientID="${clientID}"]`).addClass("blink");
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
    if ($(`.chats[clientID='${clientID}']`).length> 0)
    {
        let chatbox = $(`.chats[clientID="${clientID}"] `);
        chatbox.eq(0).append(`<div class="client-chat">${j.Message}</br>${j.MsgTime}</div>`);
        chatbox.scrollTop(chatbox.prop("scrollHeight"));
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

function ClickToClient(clientID, clientName)
{

    $(".chats").hide();
    $("#clientName").text(`TO:${clientName}`);
    if ($(`.chats[clientID='${clientID}']`).length == 0)
    {
        $(".client").after(`<div class="chats" clientID="${clientID}"></div>`)
        if (selfID.includes("random"))
        {
            return;
        }
        $.post("/Product/GetMessageForChat", { selfID: selfID, clientID: clientID }, function (datas)
        {
            datas = JSON.parse(datas);
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
                chatbox.scrollTop(chatbox.prop("scrollHeight"));
            }
        })
    }
    else
    {
        $(`.chats[clientID='${clientID}']`).show();
    }
}