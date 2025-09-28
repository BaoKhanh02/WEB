<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="WebApplication1.Game" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Memory Game</title>
    <style>
        body { font-family: Arial; text-align: center; }
        #game-board {
            display: grid;
            grid-template-columns: repeat(4, 100px);
            grid-gap: 10px;
            justify-content: center;
            margin-top: 20px;
        }
        .card {
            width: 100px;
            height: 100px;
            background: #3498db;
            color: white;
            font-size: 30px;
            display: flex;
            justify-content: center;
            align-items: center;
            cursor: pointer;
        }
        .matched {
            background: #2ecc71 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>🎮 Memory Game</h1>
        <button type="button" onclick="startGame()">Start Game</button>
        <p>Time: <span id="timer">0</span> seconds</p>
        <div id="game-board"></div>
        <p id="result"></p>
    </form>

    <script>
        let cards = [];
        let flipped = [];
        let matchedCount = 0;
        let time = 0;
        let timerInterval;

        function startGame() {
            document.getElementById("result").innerText = "";
            matchedCount = 0;
            time = 0;
            document.getElementById("timer").innerText = time;
            clearInterval(timerInterval);
            timerInterval = setInterval(() => {
                time++;
                document.getElementById("timer").innerText = time;
            }, 1000);

            let symbols = ["A","A","B","B","C","C","D","D","E","E","F","F","G","G","H","H"];
            symbols = symbols.sort(() => Math.random() - 0.5);

            let board = document.getElementById("game-board");
            board.innerHTML = "";
            cards = [];

            symbols.forEach((s, i) => {
                let card = document.createElement("div");
                card.className = "card";
                card.dataset.value = s;
                card.dataset.index = i;
                card.innerText = "?";
                card.onclick = () => flipCard(card);
                board.appendChild(card);
                cards.push(card);
            });
        }

        function flipCard(card) {
            if (flipped.length === 2 || card.classList.contains("matched") || card.innerText !== "?") return;

            card.innerText = card.dataset.value;
            flipped.push(card);

            if (flipped.length === 2) {
                setTimeout(checkMatch, 500);
            }
        }

        function checkMatch() {
            if (flipped[0].dataset.value === flipped[1].dataset.value) {
                flipped[0].classList.add("matched");
                flipped[1].classList.add("matched");
                matchedCount++;
                if (matchedCount === cards.length / 2) {
                    clearInterval(timerInterval);
                    document.getElementById("result").innerText =
                        "🎉 You win! Time: " + time + " seconds";
                }
            } else {
                flipped[0].innerText = "?";
                flipped[1].innerText = "?";
            }
            flipped = [];
        }
    </script>
</body>
</html>
