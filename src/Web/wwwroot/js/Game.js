"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();

function clearMessageList() {
    var element = document.getElementById("messagesList");
    while (element.firstChild) { element.firstChild.remove(); }
}

function joinGame(game) {
    var createJoin = document.getElementById("createJoin");
    var startGame = document.getElementById("startGame");
    createJoin.style.display = "none";
    startGame.style.display = "block";
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
    var code = document.getElementById('codeInput').value;
    clearMessageList();
    connection.invoke("CreateGame", code).then(function (game) {
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("join-game").addEventListener("click", function (event) {
    var code = document.getElementById('codeInput').value;
    clearMessageList();
    connection.invoke("JoinGame", code).then(function (game) {
        joinGame(game);
    }).catch(function (err) {
        return console.error(err.toString());
    });
});
