let timeout = 1000 * 60 * 5; //mudar para testar logout por inactividade. valor do enunciado corresponde a 300000ms 
let timer;
let timerAlert;
let timerSec;


window.onclick = (evnt) => {
    const loginModal = document.getElementById('login-modal');
    if (evnt.target == loginModal) {
        loginModal.style.display = 'none';
        clearModal();
        document.getElementById('login-form-wrapper').style.display = 'block';
        document.getElementById('register-form-wrapper').style.display = 'none';
    }
}


window.onload = () => { 
    
    //sessionInfo will serve as a cache for any information
    //to be stored later in localStorage
    sessionInfo = {};
    sessionInfo.favourites = {};
    //for controling the search aparatus to display
    //it's global because it's used on login and searchModeToggle
    let searchMode = 'simple';
    
    //show login-modal when user clicks login button
    const loginBtn = document.getElementById('login-btn');
    const loginModal = document.getElementById('login-modal');
    loginBtn.onclick = () => {
        loginModal.style.display = 'block';
    }
    
    //modal internal behaviour
    const regBtn = document.getElementById('register-btn')
    regBtn.onclick = switchModalContent;
    const backBtn = document.getElementById('back-to-login')
    backBtn.onclick = switchModalContent;
    
    //register form submission is handled in validateRegister(). if its successful return modal to login
    const registerForm = document.getElementById('reg-form');
    registerForm.onsubmit = (evnt) => {
        evnt.preventDefault();
        if (validateRegister()) {
            switchModalContent();
        }
    }
    
    //login is handled by validate login. if successful 'enters's the app and sets the session info 
    const app = document.getElementById('app');
    const loginForm = document.getElementById('login-form');
    loginForm.onsubmit = (evnt) => {
        evnt.preventDefault();
        
        if (validateLogin()) {
            //go to page proper
            loginModal.style.display = 'none';
            clearModal();
            app.style.display = 'block';
            setSearchTimeout();
            setAppHeader();
            searchMode = 'simple'
        }
    }

    //log out routine.
    const logOutBtn = document.getElementById('logout-btn')
    logOutBtn.onclick = logoutRoutine
    
    //favourite button
    const favBtn = document.getElementById('favourites-btn');
    favBtn.onclick = () => {
        displayLibrary(Object.values(sessionInfo.favourites));
    };

    //search toggle
    const searchModeToggle = document.getElementById('search-mode');
    searchModeToggle.onclick = () => {
        clearSearches();
        let advSearch = document.getElementById('advanced-search');
        let simpleSearch = document.getElementById('simple-search')

        if (searchMode == 'simple') {
            searchMode = 'advanced';
            searchModeToggle.innerText = 'Simple Search'
            advSearch.className = '';
        } else {
            searchMode = 'simple';
            searchModeToggle.innerText = 'Advanced Search'
            advSearch.className = 'collapsed';
        }
    };

    //search
    const searchForm = document.getElementById('search-form')
    searchForm.onsubmit = (evnt) => {
        resetSearchTimeout();
        evnt.preventDefault();
        library.innerHTML = '';
        let xhttp = new XMLHttpRequest();
        let url = queryBuilder(searchMode);
        

        //after dealing with search parameters do the request and process the result
        let volumes = JSON.parse(queryAPI(xhttp, url)).items
        let books = volumes
            .map(volume => {
                return {
                    id: volume.id,
                    vInfo: volume.volumeInfo
                }
            })
            .map(book => {
                return {
                    id: book.id,
                    authors: book.vInfo.authors,
                    description: book.vInfo.description,
                    imageLinks: book.vInfo.imageLinks,
                    title: book.vInfo.title,
                    industryIdentifiers: book.vInfo.industryIdentifiers
                }
            })
        displayLibrary(books);
    }

    document.querySelector('#inactivity-alert button').onclick = resetSearchTimeout;
};

function queryBuilder(searchMode) {
    const baseUrl = "https://www.googleapis.com/books/v1/volumes?maxResults=40&q=";
    let libraryHeader = document.querySelector('#library-header');
    libraryHeader.innerText = 'Showing results for ';

    let searchBarValue = document.getElementById('book-search').value;
    let searchBar = searchBarValue.trim().split(' ').join('+');
    if (searchMode == 'simple') {    
        libraryHeader.innerText = 'Showing results for "' + searchBarValue.trim() + '"';
        return baseUrl + searchBar;
    } else {
        
        let options = [...document.querySelectorAll('#advanced-search input[type="radio"]')]
        let option = options.filter(radio => radio.checked)[0]
        let language = '&langRestrict=' + document.getElementById('adv-search-language').value
        switch (option.value){
            case 'intitle:':
                libraryHeader.innerText = 'Showing book titles that have "' + searchBarValue.trim() + '"';
                break;
            case 'inauthor:':
                libraryHeader.innerText = 'Showing books (co)written by "' + searchBarValue.trim() + '"';
                break;
            case 'inpublisher:':
                libraryHeader.innerText = 'Showing books published by "' + searchBarValue.trim() + '"';
                break;
            case 'subject:':
                libraryHeader.innerText = 'Showing books that fall in "' + searchBarValue.trim() + '"';
                break;
            case 'isbn:':
                libraryHeader.innerText = 'Showing books with isbn "' + searchBarValue.trim() + '"';
                break;
        }
        return baseUrl + option.value + searchBar + language;
    }
}

function displayLibrary(books) {
    const library = document.getElementById('library');
    //clear library from earlier results
    library.innerHTML = '';
    books.forEach((book) => {
        try {

            //bookElement that will have all other elements as children
            let bookElement = document.createElement('div');
            bookElement.className = 'book';
            // invisible span that will serve as storage for the book info
            // to be referenced when user clicks it
            let bookInfoElement = document.createElement('span');
            bookInfoElement.className = 'hidden';
            bookInfoElement.innerText = JSON.stringify(book);
            bookElement.appendChild(bookInfoElement);

            let imgHeartCont = document.createElement('div');
            imgHeartCont.className = 'img-heart-container';
            let bookThumbnail = document.createElement('img');
            bookThumbnail.setAttribute('src', book.imageLinks.thumbnail);
            imgHeartCont.appendChild(bookThumbnail);
            //check if book exists in users favorites.
            let isFavourite = sessionInfo.favourites[book.id] ? true : false;
            let heart = document.createElement('img');
            heart.className = 'heart-icon';
            let pathToImg = isFavourite ? './img/heart_full.png' : './img/heart.png';
            heart.setAttribute('src', pathToImg);
            heart.onclick = heartOnClick;
            imgHeartCont.appendChild(heart);
            
            bookElement.appendChild(imgHeartCont);
            
            let bookTitle = document.createElement('h5');
            bookTitle.innerText = book.title;
            bookElement.appendChild(bookTitle);
            
            let authorsElement = document.createElement('ul');
            book.authors.forEach((author) => {
                let liElement = document.createElement('li');
                liElement.innerText = author;
                authorsElement.appendChild(liElement);
            });
            
            bookElement.appendChild(authorsElement);
                
            library.appendChild(bookElement);
        } catch (error) {
        }
    });
}

function heartOnClick(evnt) {
    //whick book was clicked?
    let book = evnt.target.parentNode.parentNode;
    //store its information in user favourites
    let info = JSON.parse(book.firstChild.innerText);

    let isFavourite = sessionInfo.favourites[info.id] ? true : false;
    if (isFavourite) {
        delete sessionInfo.favourites[info.id];
        evnt.target.setAttribute('src', './img/heart.png');
    } else {
        sessionInfo.favourites[info.id] = info;
        evnt.target.setAttribute('src', './img/heart_full.png');
    }
}

function queryAPI(xmlhttp, url) {
    xmlhttp.open('GET', url, false);
    xmlhttp.send();
    return xmlhttp.responseText;
}

function switchModalContent() {
    const login = document.getElementById('login-form-wrapper')
    const reg = document.getElementById('register-form-wrapper')

    if (reg.style.display == 'none') {
        login.style.display = 'none';
        clearLogin();
        reg.style.display = 'block'
    } else {
        login.style.display = 'block';
        reg.style.display = 'none';
        clearRegister();
    }
}

function validateRegister() {
    //store elements for easy reference
    const registerForm = document.getElementById('register-form');
    const username = document.getElementById('reg-user')
    const email = document.getElementById('reg-email')
    const password = document.getElementById('reg-password')
    const confirmPassword = document.getElementById('reg-confirm-password')

    //create holders for the validation result
    //userObj will hold every field do be stored as json string
    //errors will hold every error to display on screen
    let userObj = {};
    let errors = []

    //email doesn't test for real domains. only if the format is alphanumeric_string@only_lowercase.only_lowercase
    let emailRegExp = /^\w+@[a-z]+\.[a-z]{2,}$/
    if (emailRegExp.test(email.value) && !localStorage.getItem(email.value)) {
        userObj.email = email.value;
        email.style.backgroundColor = 'lightgreen'
    } else if (localStorage.getItem(email.value)) {
        errors.push('Um usuário já registou esse email!');
        email.style.backgroundColor = '#6d212a'
    } else {
        errors.push('Por favor introduza um email válido!')
        email.style.borderColor = '#6d212a'
    }

    //username should have between 3 and 20 alphanumeric chars
    let userRegExp = /^\w{3,20}$/;
    if (userRegExp.test(username.value)) {

        userObj.username = username.value;
        username.style.borderColor = 'lightgreen'

    } else {
        errors.push('O nome de utilizador deve contar apenas carateres alfanuméricos (mínimo 3 e máximo 20)')
        username.style.bordeColor = '#6d212a';
    }

    //password will pass with between 4 and 8 characters and if it matches confirm-password field
    let passRegExp = /.{4,8}/i
    if (!passRegExp.test(password.value)) {
        errors.push('Password deve ser entre 4 a 8 caracteres!')
        password.style.borderColor = '#6d212a'
    } else if (password.value != confirmPassword.value) {
        errors.push('Passwords não coincidem!');
        password.style.borderColor = '#6d212a';
        confirmPassword.style.borderColor = '#6d212a';
    } else {
        userObj.password = password.value;
        password.style.borderColor = 'ligthgreen';
    }

    //if there are any errors at this point display them
    if (errors.length > 0) {

        displayErrors(errors, 'reg-error-container');
        return false;

    } else {
        //if no errors test if the user already exists
        userObj.favourites = {};
        localStorage.setItem(userObj.email, JSON.stringify(userObj))

        //clear formatting for errors and form
        document.getElementById('reg-error-container').innerHTML = '';
        username.style.backgroundColor = 'initial';
        email.style.backgroundColor = 'initial';
        password.style.backgroundColor = 'initial';
        confirmPassword.style.backgroundColor = 'initial';
        username.value = '';
        email.value = '';
        password.value = '';
        confirmPassword.value = '';
        return true;
    }
}

function validateLogin() {
    //store elements for easy reference
    const email = document.getElementById('login-email');
    const password = document.getElementById('login-password');

    let errors = [];

    //check for user match on database
    if (localStorage.getItem(email.value)) {

        let user = JSON.parse(localStorage.getItem(email.value));
        if (password.value == user.password) {
            email.value = '';
            password.value = '';
            document.getElementById('login-error-container').innerHTML = '';
            sessionInfo = { ...user };
            return true;
        } else {
            errors.push('A sua password está incorrecta')
        }
    }
    else
        errors.push('Esse usuário não existe. Verifique os seus dados ou faça um registo.');

    if (errors.length > 0) {
        displayErrors(errors, 'login-error-container');
    }
}

function displayErrors(errors, container) {
    const errorContainer = document.getElementById(container)
    errorContainer.innerHTML = ''
    let ul = document.createElement('ul')
    errors.forEach((error) => {
        let li = document.createElement('li')
        li.innerText = error
        ul.appendChild(li)
    });
    errorContainer.appendChild(ul);
}

function setAppHeader() {
    const userElmt = document.getElementById('user');
    const lastLoginElmt = document.getElementById('last-login');
    const thisLoginElmt = document.getElementById('this-login');

    let now = new Date();

    userElmt.innerText = sessionInfo.username;
    //check if first login

    let lastLoginDate = sessionInfo.lastLogin ? new Date(sessionInfo.lastLogin) : now; //check if first login
    lastLoginElmt.innerText = lastLoginDate.toUTCString();
    thisLoginElmt.innerText = now.toUTCString();

    sessionInfo.lastLogin = now; //update last login to this login
}
function clearSearches() {

    //clear search bars
    document.getElementById('book-search').value = '';

}

function clearLogin() {

    //fields
    [...document.querySelectorAll("#login-form input")].forEach(elm => elm.value = '');

    //errors
    document.getElementById('login-error-container').innerHTML = '';
}

function clearRegister() {
    //fields
    [...document.querySelectorAll('#reg-form input')]
        .forEach(elm => {
            elm.value = '';
            elm.style.backgroundColor = 'initial';
        });
    //errors
    document.getElementById('reg-error-container').innerHTML = '';
}

function clearModal() {
    clearLogin();
    clearRegister();
}

function clearFormFields(formId){
    let listaInputs = document.querySelectorAll('#' + formId + ' input');
    listaInputs.forEach( (item) => {
        item.value = '';
    }) 
}

function logoutRoutine() {

    localStorage.setItem(sessionInfo.email, JSON.stringify(sessionInfo));
    document.getElementById('app').style.display = 'none';

    //clear library
    document.getElementById('library').innerHTML = '';
    clearSearches();
    window.clearTimeout(timer);
    window.clearTimeout(timerAlert);
    window.clearInterval(timerSec);
}

function setSearchTimeout() {

    timer = window.setTimeout(logoutRoutine, timeout)
    timerAlert = window.setTimeout(timeoutWarning, timeout - (timeout*.10))

    let alert = document.getElementById('inactivity-alert')
    alert.className = 'alert-out'
    let countdown = document.querySelector('#inactivity-alert #countdown')
    countdown.innerText = (timeout*.10 )/1000;
}

function resetSearchTimeout() {
    window.clearTimeout(timer);
    window.clearTimeout(timerAlert);
    window.clearInterval(timerSec);
    setSearchTimeout();
}

function timeoutWarning() {
    let alert = document.getElementById('inactivity-alert')
    alert.className = ''
    let countdown = document.querySelector('#inactivity-alert #countdown')
    timerSec = window.setInterval(() => {
        countdown.innerText = parseInt(countdown.innerText) - 1;
        console.log('tic')
    }, 1000);
}

