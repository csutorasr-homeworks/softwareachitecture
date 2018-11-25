"use strict";

window.connection = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/game").build();
    var connectionWrapper = {

        start: function () {
            return connection.start();
        },

        onRecieveMessage: function (callBack) {
            connection.on("RecieveMessage", callBack);
        },

        /* Csatlakoztatja a usert egy játékhoz 
         * Arguments:
         *   String code: a játék kódja
         * Ha sikeresen csatlakozott akkor küld egy PlayerConnected üzenetet neki
         * Returns:
         *  Boolean success: - ha sikerult csatlakozni true (ha a játék már elkezdődőtt , vagy vége is lett  vagy nem létezik false)
         *  String errorMessage: - hibauzenet ha success false
         *  GameData game: - informacio a játékról(egyelore nincs szükség rá :))
         */
        invokeJoinGame: function (code) {
            return connection.invoke.call(connection, "JoinGame", code);
        },

        /* Létrehoz egy játékot majd csatlakoztatja
         * A válasz ugyanaz mint a JoinGame         
         */
        invokeCreateGame: function (code) {
            return connection.invoke.call(connection, "CreateGame", code);
        },

        /* 
         * Megkapja: minden játékhoz csatlakozott felhasználó
         * Mikor: - minden alkalommal amikor valaki csatlakozik a játékhoz JoinGame-el vagy CreateGame-el
         *        - utoljara ha meghívták a StartGame -et hogy elküldje a végleges listát  és a gameStarted változót
         * A játékhoz csatlakozott userek listáját küldi el valamint azt hogy elkezdődhet e és hogy elkezdődött e a játék.
         * Arguments:
         * Integer connectNr - PlayerConnected uzenetek azonosítója, ez alapján döntjuk el hogy újabb e az üzenet  egy játékon belül arra az esetre ha OutOf sync jönnek az üzenetek.  
                             - Erre alternativa úgy döntjuk el hogy újabb e az üzenet hogy megnézzük növekedett e az userek száma, vagy valtoztak a a gameStart es a gameStarted
         * List<User> users - lista az osszes csatlakozott User-el 
         * Boolean gameCanStart - megNyomhatjuk e a gombot (arra gondoltam hogy ha legalabb 2 játékos van akkor lehet true)
         * Boolean gameStarted - true lesz ha meghivták a StartGame-et valaki a start game gombot(limitalhatjuk hogy csak a room creator csinalhatja)
       */
        onPlayerConnected:function(callBack) {
            connection.on("PlayerConnected", callBack);
        },

        //ha a felhasználó megnyomja a gombot már nem lehet csatlakozni
        //veglegesiti a jatekosok listajat, ezutan egy utolso playerconnected callt kuld a backend, a veso listaval es isGameReady true val
        //nem tudom hogy szukseg van e valami infora itt
        invokeStartGame: function () {
            return connection.invoke("StartGame");
        },

        /*  visszateriti a kérdés pillanatnyi állapotát
          *  Argument:
          *  question : a kérdés az előző válasz a helyes válasszal és azzal az információval 
          *     QuestionNr: A kérdés sorszáma
          *     QuestionRecievedNr: A kérdés info küldésének sorszáma : ugyanaz a szerepe mint a connectNr-nak
          *     Guesses: lista arrol ki mit válaszolt (userId és válasz száma az options listaban
          *     QuestionText: a kérdés szövege
          *     Options: lista a lehetseges valaszokkal egyszeru sztring lista is lehet
          *     Answer: A helyes válasz száma  // csak akkor küldjuk el ha mindenki válaszolt a kérdésre egyebkent null
          *  scores - lista a jatekosok pontainval (UserId,Points)
          *  A játék indításakor küldi először (itt varhat a szerver par masodpercet az utolso playersconnected kiküldése után)
          *  minden alkalommal amikor valaki válaszol a kérdésre újraküldi
          *  Ha answerrel egyutt küldi már nem lehet válaszolni , és megmutatjuk a helyes válasz
          *  Ezután ha egy következő questionNr-rel küldi akkor megjelenitjuk azt (ezzel a küldéssel varhat a szerver 3 masodperced a helyes válazs kiküldése utan)
          */
        onQuestionsRecieved: function (callback) {
            connection.on("QuestionsRecieved", callback);
        },
        /*lekuldi a tippjet a usernek , ilyenkor a backend QuestionsRecieved-et kuld az uj allapottal
         * returns:
         * Boolean success;
         */
        invokeSendGuess: function (optionNr) {
            return connection.invoke.call(connection, "SendAnswer", optionNr);
        },
        /*Amikor az utolso kérdést is megválaszolták elküldi ezt az üzenetet
         ez tartalmazza a 
         */
        onGameEnded: function (callback) {
            connection.on("GameEnded", callback);
        },
        onGameListUpdate: function (callback) {
            connection.on("GameListUpdate", callback);
        },

        invokeGetGames: function () {
            return connection.invoke("GetGames");
        }

    };
    return connectionWrapper;
}())