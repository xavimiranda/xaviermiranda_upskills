
let debug = true

function start() {
    ex1RandomGen() //ex1 randomnumber generation
    daysUntillXMas()
}
/**
 * 
 * HELPER FUNCTIONS
 * 
 */

/**
 * return a random integer between interval specified
 * @param {number} min 
 * @param {number} max 
 */
function randomInt(min = 0, max = 1) {
    return Math.floor(Math.random() * 10 + min)
}

function convertToCelcius(temp) {
    return Math.round((temp - 32) * (5 / 9))
}

function convertToFahrenheit(temp) {
    return Math.round(temp * (9 / 5) + 32)
}

/*Ex1
    on dom load generate a random number and store it in a hidden span. this is the target guess  
*/
function ex1GuessNumber() {
    let input = document.getElementById('ex1-input').value
    let target = document.getElementById('ex1-random-number').value
    let resultBox = document.getElementById('ex1-result')
    if (input == target) {
        resultBox.innerText = "Good Work"
        ex1RandomGen()
    } else {
        resultBox.innerText = "Not Matched"
    }
}

function ex1RandomGen() {
    document.getElementById('ex1-random-number').value = randomInt(1, 10)
    if (debug)
        console.log(`Exercise 1 random number generated: ${document.getElementById('ex1-random-number').value}`)
}
/**
 * Ex2
 * 
 */
function daysUntillXMas() {
    let today = new Date();

    let xmas = new Date(today.getFullYear(), 11, 25)
    if (today.getMonth() == 11 && today.getDate() > 25)
        xmas.setFullYear(today.getFullYear() + 1);

    let day = 1000 * 60 * 60 * 24
    let until = Math.ceil((xmas.getTime() - today.getTime()) / day)

    document.getElementById('ex2-days').innerText = until
    if (debug)
        console.log(`ex2: xmas ${xmas}`)
}

/**
 * Ex3
 */
function ex3(operation) {
    let num1 = document.getElementById('ex3-num1').value
    let num2 = document.getElementById('ex3-num2').value
    let result = num1 * num2;
    if (operation === '/')
        result = num1 / num2;
    document.getElementById('ex3-result').innerText = result
}

/**
 * ex4
 */
// let convertTemp = (evnt) => {
//     console.log(evnt)
// }

function convertTemp(conversion) {
    let temperature = document.getElementById('ex4-temp').value
    let result = document.getElementById('ex4-result')
    if (conversion === 'C')
        result.innerText = convertToCelcius(temperature)
    else if (conversion === 'F')
        result.innerText = convertToFahrenheit(temperature)

}


/**
 * EX5
 */

function ex5() {
    let input = document.getElementById('ex5-input').value
    let result = input - 13
    if (input > 13)
        result = Math.abs(result) * 2

    document.getElementById('ex5-result').innerText = result
}

/**
 * Ex6
 */

function ex6() {
    let num1 = parseInt(document.getElementById('ex6-num1').value)
    let num2 = parseInt(document.getElementById('ex6-num2').value)
    let result = num1 + num2
    if (num1 == num2)
    result *= 3
    document.getElementById('ex6-result').innerText = result
    
}
function ex7() {
    let num1 = parseInt(document.getElementById('ex7-num1').value)
    let num2 = parseInt(document.getElementById('ex7-num2').value)
    let result = num1 == 50 && num2 == 50 ? 'Both numbers are 50!' : 
    num1 == 50 ? 'Left number is 50' : 
    num2 == 50 ? 'Right number is 50':
    num1+num2 == 50 ? 'Both numbers add up to 50' : 'no 50\'s here'
    
    document.getElementById('ex7-result').innerText = result
}

function ex8() {
    let input = document.getElementById('ex8-input').value
    let result

    
    if( input < 250 && (input >= 80 && input <= 120)) { //if input is closer to 100 than to 400
       
        result = 'Number is around 100!'
    } else if ( input > 250 && ( input >= 380 && input <= 420)) {
        
        result = 'Number is around 400!'
    }
    else 
        result = 'Nothing...'
    
    document.getElementById('ex8-result').innerText = result
}

function ex9() {
    let num1 = parseInt(document.getElementById('ex9-num1').value)
    let num2 = parseInt(document.getElementById('ex9-num2').value)
    let result

    if (num1 < 0 && num2 > 0)
        result = 'left: - \t right: +'
    else if (num1 > 0 && num2 < 0)
        result = 'left: + \t right: -'
    else
        result = '...'
    document.getElementById('ex9-result').innerText = result
}

function ex10() {
    let regexp = /^Py/
    let phrase = document.getElementById('ex10-input').value
    if (! regexp.test(phrase)) {
        document.getElementById('ex10-input').value = phrase + 'py'
    }
}

function ex11() {
    let phrase = document.getElementById('ex11-input').value
    let arr = phrase.trim().split('')
    let lastArg = arr.pop()
    let firstArg = arr.shift()
    arr.unshift(lastArg)
    arr.push(firstArg)
    document.getElementById('ex11-input').value = arr.join('')
}

function ex12() {
    let num = document.getElementById('ex12-input').value
    let result
    if(num%3 == 0 || num%7 == 0)
        result = 'Yes'
    else
        result = 'No'
    
    document.getElementById('ex12-result').innerText = result
}