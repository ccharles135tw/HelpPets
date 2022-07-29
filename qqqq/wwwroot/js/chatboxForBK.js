

$(document).ready(() =>
{
  
    $(".chat-btn").click(() => {
        $(".chat-box").slideToggle("slow")
    })
})
/*$("body").on("click", "img", () => alert("test"));*/

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//與Server建立連線
connection.start().then(function ()
{
    console.log("Hub 連線完成");
}).catch(function (err)
{
    alert('連線錯誤: ' + err.toString());
});

// 更新連線 ID 列表事件
connection.on("UpdList", function (jsonList)
{
    var list = JSON.parse(jsonList);
    $("#IDList li").remove();
    for (i = 0; i < list.length; i++)
    {
        $("#IDList").append($("<li></li>").attr("class", "list-group-item").text(list[i]));
    }
});

// 更新用戶個人連線 ID 事件
connection.on("UpdSelfID", function (id)
{
    $('#SelfID').html(id);
});

// 更新聊天內容事件
connection.on("UpdContent", function (msg)
{
    $("#Content").append($("<li></li>").attr("class", "list-group-item").text(msg));
});

//傳送訊息
$('#sendButton').on('click', function ()
{
    let selfID = $('#SelfID').html();
    let message = $('#message').val();
    let sendToID = $('#sendToID').val();
    connection.invoke("SendMessage", selfID, message, sendToID).catch(function (err)
    {
        alert('傳送錯誤: ' + err.toString());
    });
});
