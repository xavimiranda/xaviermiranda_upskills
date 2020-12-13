const cart = [];

window.onload = () => {
    let addToCartBtns = document.querySelectorAll('.adicionar-carrinho');
    addToCartBtns.forEach(btn => btn.onclick = addToCart)

    document.getElementById('limpar-carrinho').onclick = cleanCart;
}

function addToCart(evnt){
    evnt.preventDefault();
    let item = getItem(evnt.target.parentElement);
    if( cart.find(cartItem => cartItem.nome == item.nome)) {
        let i = cart.findIndex(cartItem => cartItem.nome == item.nome);
        cart[i].quantity++;
    } else {
        item.quantity = 1;
        cart.push(item)
    }
    cartToHTML();    
}
/**
 * returns an object with all the information of the html element .info-card
*/
function getItem(elmt){
    let item = {}
    item.foto = elmt.previousElementSibling.src;
    item.nome = elmt.querySelector('h4').innerText;
    item.preco = elmt.querySelector('p.preco span').innerText;
    return item;
}

function cartToHTML() {
    //clean cart table to update results
    const cartElmt = document.querySelector('#lista-carrinho tbody');
    cartElmt.innerHTML = '';
    //lets iterate the cart
    cart.forEach(item => {
        //each item is a table row in #lista-carrinho
        let tr = document.createElement('tr');
        //each item property is a table cell in that row
        for( value in item) {
            let td = document.createElement('td');
            if(value == 'foto') {
                let img = document.createElement('img');
                img.src = item[value];
                td.appendChild(img);
            } else {
                td.innerText = item[value];
            }
            tr.appendChild(td);
        }
        //before appending the row add a remove btn with an onclick event handler
        let td = document.createElement('td');
        let removeBtn = document.createElement('button');
        removeBtn.className = 'apagar-curso';
        removeBtn.innerText = 'X';
        removeBtn.onclick = removeFromCart;
        td.appendChild(removeBtn);
        tr.appendChild(td);
        //finanlly append the row to the table body
        cartElmt.appendChild(tr);
    });
}

function removeFromCart(evnt) {
    //get the title of the course
    let title = evnt.target.parentElement.parentElement.firstChild.nextSibling.innerText;
    //remove it from the cart
    cart.splice(cart.findIndex(item => item.nome == title) , 1);
    //update cart
    cartToHTML();
}

function cleanCart() {
    cart.splice(0, cart.length);
    cartToHTML();
}
