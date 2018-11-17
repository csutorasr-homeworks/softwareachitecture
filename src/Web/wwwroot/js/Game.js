"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();

function clearMessageList() {
    var element = document.getElementById("messagesList");
    element.children.forEach(x => x.remove());
}

connection.on("ReceiveMessage", function (user, message) {
    var encodedMsg = user + ": " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    li.scrollIntoView();
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("lobbyMessage").addEventListener("submit", function (event) {
    var element = document.getElementById("messageInput");
    var message = element.value;
    element.disabled = true;
    connection.invoke("SendMessage", message).then(function () {
        element.value = "";
        element.disabled = false;
        element.focus();
    }).catch(function (err) {
        element.disabled = false;
        element.focus();
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("create-game").addEventListener("click", function (event) {
    clearMessageList();
    // TODO: create game
});

document.getElementById("join-game").addEventListener("click", function (event) {
    clearMessageList();
    // TODO: join game by code
});
