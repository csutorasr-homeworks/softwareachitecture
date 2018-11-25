"use strict";


function clearMessageList() {
    var element = document.getElementById("messagesList");
    while (element.firstChild) { element.firstChild.remove(); }
}

connection.onRecieveMessage(function (user, message) {
    var encodedMsg = user + ": " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    li.scrollIntoView();
});

var dummyGame = {


};

var firstUsers = [ {
        Id: "User1",
        userName: "Almafa"
}];
var secondUsers = [ {
        Id: "User1",
        userName: "Almafa"
    }, {
        Id: "User2",
        userName: "Kortefa"
    }];

var thirdUsers = [{
        Id: "User1",
        userName: "Almafa"
    },{
        Id: "User2",
        userName: "Kortefa"
    }, {
        Id: "User3",
        userName: "Szilvafa"
    }
];


var scores1 = [{
    userId: thirdUsers[0].Id,
    Points: 0
},
{
    userId: thirdUsers[1].Id,
    Points: 0
},
{
    userId: thirdUsers[2].Id,
    Points: 0
    }];

var scores2 = [{
    userId: thirdUsers[0].Id,
    Points: 0
},
{
    userId: thirdUsers[1].Id,
    Points: 0
},
{
    userId: thirdUsers[2].Id,
    Points: 0
    }];
var scores3 = [{
    userId: thirdUsers[0].Id,
    Points: 5
},
{
    userId: thirdUsers[1].Id,
    Points: 0
},
{
    userId: thirdUsers[2].Id,
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


var simulate = false;

class CreateGameViewModel {
    constructor(connection) {
        this.nrOfPlayers = ko.observable(4);
        this.nrOfQuestions = ko.observable(5);
        this.playerNumbers = [];
        var i;
        for (i = 2; i < 5; i++) {
            this.playerNumbers.push(i);
        }
        this.questionNumbers = [];
        for (i = 2; i < 10; i++) {
            this.questionNumbers.push(i);
        }
        this.connection = connection;
    }

    createGame(){
        clearMessageList();
        this.connection.invokeCreateGame("almafa", this.nrOfPlayers(), this.nrOfQuestions()).catch(function (err) {
            try {
                return console.error(err.toString());
            } catch (ex) {
                console.error(x);
            }
        });
    } 
};

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
    vm.createGameVm = new CreateGameViewModel(connection);
    vm.game = ko.observable(null);
    vm.gameState = ko.observable();

    vm.joinGame = function () {
        var code = document.getElementById('codeInput').value;
        clearMessageList();
        connection.invokeJoinGame(code).catch(vm.onCreateOrJoinFailiure);
    };

    
    vm.games = ko.observableArray().extend({ rateLimit: 500 });

    vm.onGameListUpdate = function (data) {
        vm.games.removeAll();
        data.games.forEach(function (game) {
            vm.games.push({
                gameId: game.gameId,
                joinGame: function () {
                    connection.invokeJoinGame(game.gameId).catch(vm.onCreateOrJoinFailiure);
                }
            });
        });
    };

    vm.connect = function () {
        connection.start().then(function () {
            vm.state("lobby");
            connection.invokeReconnect();
            connection.invokeGetGames();
        }).catch(function (err) {
            vm.state("failetoconnect");
            return console.error(err.toString());
        });
    };

    vm.gotoLobby = function () {
        connection.invokeGetGames();
        vm.players.removeAll();
        vm.state("lobby");
    };

    vm.sendMessage = function () {
        var element = document.getElementById("messageInput");
        var message = element.value;
        element.disabled = true;
        connection.sendMessage(message).then(function () {
            element.value = "";
            element.disabled = false;
            element.focus();
        }).catch(function (err) {
            element.disabled = false;
            element.focus();
            return console.error(err.toString());
        });
        event.preventDefault();
    };

    vm.currentUser = {
        userId: "9ec96fb2-1a0c-4c5b-9680-101a39162650",
        userName: "zongorla@gmail.com"
    };
    vm.connectNr = null;
    vm.onPlayersConnected = function callback(data) {
        var connectNr = data.connectNr, users = data.users, gameCanStart = data.gameCanStart, gameStarted = data.gameStarted;
        console.log(arguments);
        if (vm.connectNr === null || vm.connectNr <= connectNr || true) {
            vm.connectNr = connectNr;
          
            vm.players.removeAll();
            for (var i = 0; i < users.length; i++) {
                let user = users[i];
                vm.players.push(new Player(user, vm.colors[i], user.userId === vm.currentUser.userId));
            }
            vm.canStart(gameCanStart);
            if (vm.state() === "lobby") {
                vm.state("waiting");
            }
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
        vm.guessVMs.removeAll();
        vm.optionVMs.removeAll();
        question.answers.forEach(function (answer,index) {
            var status;
            if (question.correctAnswerId === null) {
                status = "answer-unknown";
            } else {
                if (answer.id === question.correctAnswerId) {
                    status = "answer-correct";
                } else {
                    status = "answer-wrong";
                }
            }

            var quesses = [];
            for (var userId in answer.userIdsSelected) {
                quesses.push({
                    color: vm.players().find(x => x.user.userId === userId).color.class,
                    width: "" + 100 / vm.players().length - 1 + "%"
                });
            }
            var optionVM = {
                text: answer.text,
                guesses: ko.observableArray(quesses),
                status: status,
                sendGuess: function () {
                    vm.sendGuess(answer.id);
                }
            };
            vm.optionVMs.push(optionVM);
        });
        vm.question(question);
        //vm.onScoreRecieved(scores);
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
                        setTimeout(function () {
                            vm.onGameEnded(scores3);
                        }, 3000);
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
                userId: player.user.userId,
                color: player.color.class,
                name: player.user.userName
            };
            vm.scoreVMs.push(scoreVM);
        });
    };
    
    vm.onScoreRecieved = function (scores) {
        vm.scoreVMs().forEach(function (score) {
            score.points(scores.find(x => x.userId === score.userId).points);
        });
    };

    vm.sendGuess = function (id) {
        connection.invokeSendGuess(id);
    };
    vm.gameResults = ko.observableArray();
    vm.onGameEnded = function (results) {
        var scores = [];
        for (var userId in results.sumPointsByUser) {
            scores.push({
                userId: userId,
                points: results.sumPointsByUser[userId]
            });
        }
        vm.gameResults.removeAll();
        scores.sort(function (a, b) {
            return b.points - a.points;
        });
        var lastScore = -1;
        var place = 1;
        for (var i = 0; i < scores.length; i++) {
            var gameResultVM = {};
            var score = scores[i];
            var player = vm.players().find(x => x.user.userId === score.userId);
            gameResultVM.color = player.color.class;
            gameResultVM.points = score.points;
            gameResultVM.startText = "";
            gameResultVM.endText = ".";
            if (player.user.Id === vm.currentUser.Id) {
                gameResultVM.startText = "You scored ";
                if (place !== 1) {
                    gameResultVM.endText = ". Better luck next time! ";
                } else {
                    gameResultVM.endText = ". Congratulations you won! ";
                }
            } else {
                gameResultVM.startText = player.user.userName + " scored";
            }
            gameResultVM.place = place;
            if (lastScore !== score.points) {
                place++;
                lastScore = score.points;
            }
            vm.gameResults.push(gameResultVM);
        }
        vm.state("ended");
    };

    connection.onPlayerConnected(vm.onPlayersConnected);
    connection.onQuestionsRecieved(vm.onQuestionRecieved);
    connection.onGameEnded(vm.onGameEnded);
    connection.onGameListUpdate(vm.onGameListUpdate);

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