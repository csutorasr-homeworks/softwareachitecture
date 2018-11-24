"use strict";


function clearMessageList() {
    var element = document.getElementById("messagesList");
    while (element.firstChild) { element.firstChild.remove(); }
}

//function joinGame(game) {
//    var createJoin = document.getElementById("createJoin");
//    var startGame = document.getElementById("startGame");
//    createJoin.style.display = "none";
//    startGame.style.display = "block";
//}

connection.onRecieveMessage(function (user, message) {
    var encodedMsg = user + ": " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    li.scrollIntoView();
});


//document.getElementById("lobbyMessage").addEventListener("submit", function (event) {
//    var element = document.getElementById("messageInput");
//    var message = element.value;
//    element.disabled = true;
//    connection.invoke("SendMessage", message).then(function () {
//        element.value = "";
//        element.disabled = false;
//        element.focus();
//    }).catch(function (err) {
//        element.disabled = false;
//        element.focus();
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

//document.getElementById("create-game").addEventListener("click", function (event) {
//    var code = document.getElementById('codeInput').value;
//    clearMessageList();
//    connection.invoke("CreateGame", code).then(function (game) {
//    }).catch(function (err) {
//        return console.error(err.toString());
//    });
//});

//document.getElementById("join-game").addEventListener("click", function (event) {
//    var code = document.getElementById('codeInput').value;
//    clearMessageList();
//    connection.invoke("JoinGame", code).then(function (game) {
//        joinGame(game);
//    }).catch(function (err) {
//        return console.error(err.toString());
//    });
//}); 

var dummyGame = {


};

var firstUsers = [ {
        Id: "User1",
        UserName: "Almafa"
}];
var secondUsers = [ {
        Id: "User1",
        UserName: "Almafa"
    }, {
        Id: "User2",
        UserName: "Kortefa"
    }];

var thirdUsers = [{
        Id: "User1",
        UserName: "Almafa"
    },{
        Id: "User2",
        UserName: "Kortefa"
    }, {
        Id: "User3",
        UserName: "Szilvafa"
    }
];


var scores1 = [{
    UserId: thirdUsers[0].Id,
    Points: 0
},
{
    UserId: thirdUsers[1].Id,
    Points: 0
},
{
    UserId: thirdUsers[2].Id,
    Points: 0
    }];

var scores2 = [{
    UserId: thirdUsers[0].Id,
    Points: 0
},
{
    UserId: thirdUsers[1].Id,
    Points: 0
},
{
    UserId: thirdUsers[2].Id,
    Points: 0
    }];
var scores3 = [{
    UserId: thirdUsers[0].Id,
    Points: 5
},
{
    UserId: thirdUsers[1].Id,
    Points: 0
},
{
    UserId: thirdUsers[2].Id,
    Points: 1
}];

var question1 = {
    QuestionNr: 0,
    Options: ["Alma", "Korte", "Szilva", "Talicska"],
    QuestionText: "Milyen szinu a talicska?",
    QuestionRecievedNr: 0,
    Guesses: [{
        Id: thirdUsers[0].Id,
        Guess: 0
    }],
    Answer: null
};

var question2 = {
    QuestionNr: 0,
    Options: ["Alma", "Korte", "Szilva", "Talicska"],
    QuestionText: "Milyen szinu a talicska?",
    QuestionRecievedNr: 1,
    Guesses: [{
        Id: thirdUsers[0].Id,
        Guess: 0
    },
    {
        Id: thirdUsers[1].Id,
        Guess: 2
    },
    {
        Id: thirdUsers[2].Id,
        Guess: 0
    }],
    Answer: 2
};

var question3 = {
    QuestionNr: 1,
    Options: ["asdfasd", "asdfgafdg", "Sziadfsgafdlva", "Talsadsdsdicska"],
    QuestionText: "Masddsfa  fsdaf sdaf sda fsda ka?",
    QuestionRecievedNr: 2,
    Guesses: [],
    Answer: null
};


var simulate = true;



var gameviewmodell = (function () {
    var vm = {};

    vm.colors = [
        {
            id: 0,
            class: "red",
            displayname: "red"
        }, {
            id: 0,
            class: "green",
            displayname: "green"
        }, {
            id: 0,
            class: "blue",
            displayname: "blue"
        }, {
            id: 0,
            class: "purple",
            displayname: "purple"
        }, {
            id: 0,
            class: "black",
            displayname: "black"
        }
    ]
    //connecting
    //failedtoconnect
    //lobby
    //waiting
    //arrived
    //started
    //ended
    vm.state = ko.observable("connecting");
    vm.connected = ko.computed(function () {
        return vm.state() !== "connecting" && vm.state() !== "failedtoconnect";
    });
    vm.connectingVisible = ko.computed(function () {
        return vm.state() === "connecting";
    });
    vm.failedToConnectVisible = ko.computed(function () {
        return vm.state() === "failedtoconnect";
    });
    vm.lobbyVisible = ko.computed(function () {
        return vm.state() === "lobby";
    });
    vm.waitingVisible = ko.computed(function () {
        return vm.state() === "waiting";
    });
    vm.arrivedVisible = ko.computed(function () {
        return vm.state() === "arrived";
    });
    vm.gameVisible = ko.computed(function () {
        return vm.state() === "started";
    });
    vm.endedVisible = ko.computed(function () {
        return vm.state() === "ended";
    });

    vm.game = ko.observable(null);
    vm.gameState = ko.observable();

    vm.joinGame = function () {
        var code = document.getElementById('codeInput').value;
        clearMessageList();
        connection.invokeJoinGame(code).then(vm.onCreateOrJoinSuccess).catch(vm.onCreateOrJoinFailiure);
    };

    vm.createGame = function () {
        var code = document.getElementById('codeInput').value;
        clearMessageList();
        connection.invokeCreateGame(code).then(vm.onCreateOrJoinSuccess).catch(vm.onCreateOrJoinFailiure);
    };
    vm.onCreateOrJoinSuccess = function (success,errorMessage,game) {
        if (success) {
            vm.game(game);
            simulateOnPlayersConnected();
            vm.state("waiting");
        } else {
            vm.showErrorMessage(errorMessage);
        }
    };
    vm.onCreateOrJoinFailiure = function (err) {
        if (simulate) {
            vm.onCreateOrJoinSuccess(true, null, {

            });
        }
        return console.error(err.toString());
    };

    vm.connect = function () {
        connection.start().then(function () {
            vm.state("lobby");
        }).catch(function (err) {
            vm.state("failetoconnect");
            return console.error(err.toString());
        });
    };

    vm.gotoLobby = function () {
        vm.state("lobby");
    };

    vm.currentUser = {
        Id: "User1",
        UserNama: "Almafa"
    };
    vm.connectNr = null;
    vm.onPlayersConnected = function callback(connectNr, users, gameCanStart, gameStarted) {
        if (vm.connectNr === null || vm.connectNr <= connectNr) {
            vm.connectNr = connectNr;
          
            vm.players.removeAll();
            for (var i = 0; i < users.length; i++) {
                let user = users[i];
                vm.players.push(new Player(user, vm.colors[i], user.Id === vm.currentUser.Id));
            }
            vm.canStart(gameCanStart);
            if (gameStarted) {
                if (vm.question() === null) {
                    var sub = vm.question.subscribe(function () {
                        vm.state("started");
                        sub.dispose();
                    });
                } else {
                    vm.state("started");
                }
                vm.createScoreVMs();
                vm.simulateQuestionsRecieved();
            }
        }
      
    };
    function simulateOnPlayersConnected() {
        if (simulate) {
            setTimeout(function () {
                    vm.onPlayersConnected(0,firstUsers, false,false);
                    setTimeout(function () {
                        vm.onPlayersConnected(1,secondUsers, true,false);
                        setTimeout(function () {
                            vm.onPlayersConnected(2,thirdUsers, true,false);
                        }, 2000);
                     }, 1000);
           }, 10);
        }
    }
    vm.players = ko.observableArray();
    vm.players.extend({ rateLimit: 50 });
    vm.canStart = ko.observable(false);
    vm.canStart.extend({ rateLimit: 50 });

    vm.startPressed = function () {
      
        connection.invokeStartGame().catch(function (err) {
            return console.error(err.toString());
        });
        if (simulate) {
            setTimeout(function () {
                vm.onPlayersConnected(4,thirdUsers, true,true);
            }, 1000);
        }
    };

    vm.onQuestionRecieved = function (question,scores) {
        var current = vm.question();
        if (current === null || question.QuestionRecievedNr > current.QuestionRecievedNr) {
            vm.guessVMs.removeAll();
            vm.optionVMs.removeAll();
            question.Guesses.forEach(function (guess, index) {
                var guessVM = {
                    guess: guess.Guess,
                    color: vm.players().find(x => x.user.Id === guess.Id).color.class,
                    width: "" + 100/ vm.players().length -1+ "%"
                };
                vm.guessVMs.push(guessVM);
            });
            question.Options.forEach(function (option, index) {
                var status;
                if (question.Answer === null) {
                    status = "answer-unknown";
                } else {
                    if (index === question.Answer) {
                        status = "answer-correct";
                    } else {
                        status = "answer-wrong";
                    }
                }
                var optionVM = {
                    text: option,
                    guesses: ko.observableArray(vm.guessVMs().filter(x => x.guess === index)),
                    status: status,
                    sendGuess :function () {
                        vm.sendGuess(index);
                    }
                };
                vm.optionVMs.push(optionVM);
            });
            vm.question(question);
            vm.onScoreRecieved(scores);
        }
    };


    vm.question = ko.observable(null);
    vm.optionVMs = ko.observableArray().extend({ rateLimit: 50 });
    vm.guessVMs = ko.observableArray().extend({ rateLimit: 50 });
    vm.scoreVM = ko.observableArray();
    vm.simulateQuestionsRecieved = function () {
        if (simulate) {
            setTimeout(function () {
                vm.onQuestionRecieved(question1, scores1);
                setTimeout(function () {
                    vm.onQuestionRecieved(question2, scores2);
                    setTimeout(function () {
                        vm.onQuestionRecieved(question3, scores3);
                    }, 3000);
                }, 2000);
            }, 1000);
        }
    };
    vm.scoreVMs = ko.observableArray();
    vm.createScoreVMs = function () {
        vm.scoreVMs.removeAll();
        vm.players().forEach(function (player) {
            var scoreVM = {
                points: ko.observable(0),
                userId: player.user.Id,
                color: player.color.class,
                name: player.user.UserName
            };
            vm.scoreVMs.push(scoreVM);
        });
    };
    
    vm.onScoreRecieved = function (scores) {
        vm.scoreVMs().forEach(function (score) {
            score.points(scores.find(x => x.UserId === score.userId).Points);
        });
    };

    vm.sendGuess = function (id) {
        connection.invokeSendGuess(id);
    };
    vm.onGameEnded = function() {

    }

    connection.onPlayerConnected(vm.onPlayersConnected);
    connection.onQuestionsRecieved(vm.onQuestionRecieved);
    connection.onGameEnded(vm.onGameEnded);

    vm.showErrorMessag = function (message) {
        console.log(message);
    };
    return vm;
}());

class Player{
    constructor(user, color,iscurrent) {
        this.user = user;
        this.color = color;
        this.iscurrent = iscurrent;
    }
}

gameviewmodell.connect();
ko.applyBindings({
   gamevm: gameviewmodell
}, document.getElementById("gamearea"));