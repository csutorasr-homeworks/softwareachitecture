"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();

connection.on("ReceiveLobbyMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
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
    connection.invoke("SendLobbyMessage", message).then(function () {
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