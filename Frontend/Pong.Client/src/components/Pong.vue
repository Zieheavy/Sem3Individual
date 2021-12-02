<script setup>
import { ref } from 'vue'


defineProps({
  msg: String
})

</script>

<template>
  <div>
    <button v-on:click="UnityTest()">Unity Test</button>
    <div v-if="!GameJoined"  class="tempMenu">
      <label>Game Id </label><input v-model="GameNameInput" type="number" name="id" placeholder="ID">
      <button v-on:click="CreateGame()">Create Game</button>
      <button v-on:click="StartGame()">Start Game</button>
      <ul class="gameList">
        <li v-for="game in CreatedGames" :key="game.id" 
            v-on:click="JoinGame(game)" 
            v-bind:class="{ empty: game.p1Id == null && game.p2Id == null,
                            full: game.p1Id != null && game.p2Id != null,
                            half: game.p1Id != null || game.p2Id != null  }"
        >{{game.gameName}}</li>
      </ul>
    </div>
    <div v-if="GameJoined" class="gameContainer">
      <canvas id="myCanvas" v-bind:width="canvasWidth" v-bind:height="canvasHeight" style="width: 50vw;"></canvas>
    </div>

  </div>
</template>


<script>
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import axios from 'axios'

// setTimeout(function(){}, 3000)


const gameInfo = {
    balX: 12,
    balY: 12,
    p1Pos: 12,
    p2Pos: 12,
    GameStarted: false,
    GroupName: "12",
};

let connection = "";
 

export default {
name: 'Pong',
data: () => ({
      // canvas: '',
      CreatedGames: [],
      GameJoined: false,
      GameNameInput: 1,

      pHeight: 45,
      pWidth: 6,
      pMargin: 6,
      pSpeed: 4,
      bRadius: 6,
      canvasWidth: 500,
      canvasHeight: 250,

      selectedGameId: 12,
      selectedPlayer:1,
    }),

    methods: {
      //handles drawing information on the canvas
      drawCanvas() {
        if(this.canvas != null){
          var ctx = this.canvas.getContext("2d");

          ctx.fillStyle = 'white';
          ctx.strokeStyle = ctx.fillStyle;

          ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
          ctx.beginPath();

          //draw player 1
          ctx.fillRect(this.pMargin, gameInfo.p1Pos, this.pWidth, this.pHeight);

          //draw player 2
          ctx.fillRect(this.canvas.width-this.pWidth-this.pMargin, gameInfo.p2Pos, this.pWidth, this.pHeight);

          //draws the ball
          ctx.arc(gameInfo.balX, gameInfo.balY, this.bRadius, 0, 360, false);
          ctx.fill();

          ctx.stroke();
        }
      },
      //globaly handles all keyinputs on the site
      handleGlobalKeyDown(e) {
        var keyCode = e.keyCode
        if(this.GameJoined){
          //checks if W is pressed
          if(keyCode == 87||keyCode ==119){
            if(this.selectedPlayer == 1){
              gameInfo.p1Pos -= this.pSpeed;
            }
            else{
              gameInfo.p2Pos -= this.pSpeed;
            }

            this.sendCommunication();
          }
          //checks if S is pressed
          if(keyCode == 83||keyCode ==115){
            if(this.selectedPlayer == 1){
              gameInfo.p1Pos += this.pSpeed;
            }
            else{
              gameInfo.p2Pos += this.pSpeed;
            }

            this.sendCommunication();
          }

          this.drawCanvas();
        }
      }, 
      sendCommunication(){
        var position = 0;
        if(this.selectedPlayer == 1){
          position = gameInfo.p1Pos;
        }
        else{
          position = gameInfo.p2Pos;
        }

        const PlayerPositions =
        {
            GameName: gameInfo.GroupName,
            Position: position,
            PlayerType: this.selectedPlayer 
        }
        
        connection.send('SendPlayerPosition', PlayerPositions)
      },
      UnityTest(){
        console.log("Semd Message");
        connection.send("TestFuncion", JSON.stringify({message: "Frontend Message"}))
      },
      receivePlayerInformation(message){
        if(message.gameName == gameInfo.GroupName){
          if(this.selectedPlayer == 1){
            gameInfo.p2Pos = message.p2Pos;
          }
          else{
            gameInfo.p1Pos = message.p1Pos;
          }

          this.drawCanvas();
        }
      },
      receiveBalInformation(message){
        if(message.gameName == gameInfo.GroupName){
          gameInfo.balX = message.balX;
          gameInfo.balY = message.balY;
        }

        this.drawCanvas();
      },
      //opens the websocket with the server
      setCommunication(){
        connection = new HubConnectionBuilder()
          .withUrl('http://localhost:5000/hubs/pong')
          .configureLogging(LogLevel.Information)
          .build()
        
        let startedPromise = null
        function start (connection) {
          startedPromise = connection.start().catch(err => {
            console.error('Failed to connect with hub', err)
            return new Promise((resolve, reject) => 
              setTimeout(() => start().then(resolve).catch(reject), 1000))
          })
          return startedPromise
        }
        connection.onclose(() => start())

        connection.on('ReceivePlayerPosition', (message) => {
          console.log("Receive Player Position");
          this.receivePlayerInformation(message);
        })
        
        connection.on('ReceiveBallPosition', (message) => {
          this.receiveBalInformation(message);          
        })

        start(connection);
      },
      CreateGame(){
        var exists = false;
        this.CreatedGames.forEach(element => {
          if(element.gameName == this.GameNameInput){
            exists = true;
          }
        });
        if(!exists){
          axios({
            method: "POST",
            url: "http://localhost:5000/Game?gameId="+this.GameNameInput,
          })
          .then(response => {
            this.GetActiveGames();
          })
          .catch(error => {
            console.log(error);
          });
        }
        else{
          alert("Game already exists");
        }
      },
      JoinGame(game){
        console.log(game);
        var player;
        if(game.p1Id == null){
          player = 1;
        }
        else if(game.p2Id == null){
          player = 2;
        }
        else{
          alert("GAME IS FULL");
          return;
        }

        this.selectedPlayer = player;
        gameInfo.GroupName = game.gameName;

        connection.send('JoingGame', game.gameName,this.selectedPlayer)
          .then(response => {
            this.GameJoined = true;
            this.GetCanvas();
            this.StartGame();
          })
      },  
      GetActiveGames(){
        axios({
          method: "GET",
          url: "http://localhost:5000/Game",
        })
        .then(response => {
          this.CreatedGames = response.data;
        })
        .catch(error => {
          console.log(error)
        });
      },
      GetCanvas(){
        this.$nextTick(function () {
          this.canvas = document.getElementById("myCanvas");
          if(this.canvas != null){
            gameInfo.balX = (this.canvas.width / 2) - (this.bRadius / 2);
            gameInfo.balY = (this.canvas.height / 2) - (this.bRadius / 2);

            this.drawCanvas();
          }
        })
      },
      StartGame(){
        let _gameName = gameInfo.GroupName;

        function myLoop() {         
          setTimeout(function() {   
            connection.send("CalculateBallPos", _gameName)

            myLoop();
          }, 100)
        }

        if(this.selectedPlayer == 1){
          myLoop()
        }
      }
    },
    beforeMount(){
      this.setCommunication();
      this.GetActiveGames();
      this.GetCanvas();
    },
    created() {
      window.addEventListener("onbeforeunload", this.LeaveGame);
      window.addEventListener('keypress', this.handleGlobalKeyDown);
    },
    beforeDestroy() {
      // this.LeaveGame();
      window.removeEventListener('keypress', this.handleGlobalKeyDown);
      window.removeEventListener("onbeforeunload", this.LeaveGame);
    }
}

</script>

<style lang="scss">
canvas{
  border-style: solid;
  border-color: white;
}

.gameList{
  .empty{
    background: green;
  }
  .half{
    background: orange;
  }
  .full{
    background: red;
  }
}
</style>
