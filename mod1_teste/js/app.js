//GLOBALS
let activePlayer;
let playerAScore = 0;
let playerBScore = 0;
let playerALives = 9;
let playerBLives = 9;
let numPlays = 0;

let record = localStorage.getItem('record');
if (!record) {
    localStorage.setItem('record', 0);
}

window.onload = () => {
    document.querySelector('h4').innerText = record || 'Record';
    initGameAreas();
}

function random(min, max) {
    return Math.floor(Math.random() * max + min);
}

function initGameAreas(){
    const ladoA = document.getElementById('lado-a');
    const ladoB = document.getElementById('lado-b');

    ladoA.appendChild(createTable())
    let button = document.createElement('button');
    button.innerText = 'Jogada';
    button.className = 'play-btn'
    button.onclick = play;
    ladoA.appendChild(button)
    
    ladoB.appendChild(createTable());
    let buttonB = button.cloneNode(true);
    buttonB.onclick = play;
    ladoB.appendChild(buttonB);

    //esconder um dos jogadores aleatóriamente. 
    let randomStart;
    if (Math.random() < .5){
        randomStart = '#lado-a button';
        activePlayer = 'B';
    } else {
        activePlayer = 'A';
        randomStart = '#lado-b button';
    } 
    document.querySelector(randomStart).className ='hidden';

    //iniciar contadores a 0
    document.getElementById('score-a').innerText = 0;
    document.getElementById('score-b').innerText = 0;

}

function createTable( rows = 3, cells = 3 ) {
    const table = document.createElement('table');

    for(let i = 0; i < rows; i++){
        const tr = document.createElement('tr');
        for (let j = 0; j < cells; j++) {
            const td = document.createElement('td');
            td.innerText = random(1, 20);
            tr.appendChild(td);
        }
        table.appendChild(tr);
    }

    return table;
}

function play(evnt, debug) {
    numPlays++;
    //gerar um número aleatório
    const randomNum = debug || random(1, 20);
    alert(`Joganda nº ${numPlays}. Número é : ${randomNum}!`);
    //buscar campos adversários

    let targetFieldQuery = activePlayer == 'A' ? '#lado-b td' : '#lado-a td';
    const targetFields = document.querySelectorAll(targetFieldQuery);

    targetFields.forEach( field => {
        if (field.innerText == randomNum) {
            if (activePlayer == 'A') {
                playerAScore += randomNum;
                playerBLives--; 
                
            } else {
                playerBScore += randomNum;
                playerALives--;
            }
            field.innerText = 'Destroyed';
        }
    })

    if(randomNum == 13) {
        activePlayer == 'A' ? playerBScore *= 2 : playerAScore *=2;
    }
    
    document.getElementById('score-a').innerText = playerAScore;
    document.getElementById('score-b').innerText = playerBScore;

    if (playerAScore > record || playerBScore > record) {
        record = playerAScore > playerBScore ? playerAScore : playerBScore;
        localStorage.setItem('record', record)
    }

    const btnA = document.querySelector('#lado-a button');
    const btnB = document.querySelector('#lado-b button');
    document.querySelector('h4').innerText = record;

    if (numPlays >= 30 || playerALives == 0 || playerBLives == 0) {
        let winner = playerALives == 0 ? 'Jogador B' : 'Jogador A';
        let winnerScore;
        let loserScore;
        if (winner == 'Jogador A') {
            winnerScore = playerAScore;
            loserScore = playerBScore;
        } else {
            winnerScore = playerBScore;
            loserScore = playerAScore;
        }
        alert(`Fim do jogo. ${winner} vence com ${winnerScore} contra ${loserScore}`);
    } else {
   
        if (activePlayer == 'A') {
            activePlayer = 'B';
            btnA.className = 'hidden';
            btnB.className = '';
        } else {
            activePlayer = 'A';
            btnA.className = '';
            btnB.className = 'hidden';
        }
    }
}

function playDebug(arr) {
    arr.forEach( num => {  play(); play(num);});
}