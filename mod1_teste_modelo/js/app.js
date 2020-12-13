let numClicks = localStorage.getItem('numClicks')

if (!numClicks){
    localStorage.setItem('numClicks', 0)
}

window.onload = () => {
    const images = document.querySelectorAll('img');
    images.forEach( elmt => { elmt.onclick = swapCircle });
}

const swapCircle = (evnt) => {
    numClicks++;
    if (numClicks % 10 == 0 && numClicks <= 100){
        alert(`Número de clicks é multiplo de 10! Neste momento é ${numClicks}`);
    }
    localStorage.setItem('numClicks', numClicks);


    const left = document.getElementById('img1');
    const right = document.getElementById('img2');

    if (left.getAttribute('src') == './img/fig1.png'){
        left.setAttribute('src', './img/fig2.png')
        right.setAttribute('src', './img/fig1.png')
    } else {
        left.setAttribute('src', './img/fig1.png')
        right.setAttribute('src', './img/fig2.png')
    }
}

