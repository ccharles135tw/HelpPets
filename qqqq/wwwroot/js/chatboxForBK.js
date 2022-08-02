

$(document).ready(() =>
{
  
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
        $(".chat-btn").removeClass("breath");
        $(`.ul-client li[clientID="${$(".chat-btn").attr("clientID")}"]`).click();//打開訊息的時候模擬點擊目前的li(觸發已讀)
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
    j = JSON.parse(j);
    appendMessage(clientID, j, "my-chat");
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
    if ($(`.chats[clientID='${clientID}']`).length == 0 && clientID.includes("random"))
    {
        $(".client").after(`<div class="chats" style="display:none;" clientID="${clientID}"></div>`)
        appendMessage(clientID,j, "client-chat");
    }
    else
    {
        appendMessage(clientID,j, "client-chat");
    }


    if ($(".chat-box ").is(":hidden") == false && $(".chat-btn").attr("clientID") == clientID) //如果訊息視窗是開啟並且訊息對象正確，Invoke已讀
    {
        connection.invoke("UpIsRead", selfID, clientID);
    }
});

connection.on("UpIsRead", function (clientID)
{
    ReadMessage(clientID);
})
//點擊li切換對象
$(".ul-client").on("click", "li", function ()
{
    $(this).removeClass("blink");//已讀>>移除閃爍
    let clientID = $(this).attr("clientID");
    let clientName = $(this).text();
    ClickToClient(clientID, clientName);
    $(".chat-btn").attr("clientID", clientID);//更改目前的訊息對象
    connection.invoke("UpIsRead", selfID, clientID);
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
            for (let m of datas)
            {
                
                if (m.selfID == selfID)
                {
                    appendMessage(clientID, m, "my-chat");
                }
                else if (m.selfID == clientID)
                {
                    appendMessage(clientID, m, "client-chat");
                }
            }

        })
    }
    else
    {
        $(`.chats[clientID='${clientID}']`).show();
    }
}
function appendMessage(clientID,jMessageView,chatType)
{
    let chatbox = $(`.chats[clientID="${clientID}"] `);
    if (chatType == "my-chat")
    {
        chatbox.append(`<div class="${chatType}">${jMessageView.Message}</br>${jMessageView.MsgTime}</br>${jMessageView.IsReceiveRead}</div>`);
    }
    else
    {
        chatbox.append(`<div class="${chatType}">${jMessageView.Message}</br>${jMessageView.MsgTime}</div>`);
    }
    chatbox.scrollTop(chatbox.prop("scrollHeight"));
}
function ReadMessage(clientID)
{
    let chatbox = $(`.chats[clientID="${clientID}"] `);
    let mc = chatbox.find("div.my-chat")
    console.log(999);
    console.log(clientID);
    console.log(chatbox);
    console.log(mc);
    for (let m of mc)
    {
        m.innerHTML = m.innerHTML.replace("未讀", "已讀");
    }
}