window.onload = () => {
    /*
        {
        marca: 'BMW',
        modelo: 'Serie 3',
        year: 2020,
        preco: 30000,
        portas: 4,
        cor: 'Branco',
        transmissao: 'automatica'
        },
    */

    //initialize years options
    let year = document.getElementById('year');
    for(let i = 2020; i >= 2015; i--){
        let elmt = document.createElement('option');
        elmt.value = i;
        elmt.innerText = i;
        year.appendChild(elmt);
    }

    //show all results
    updateResults(autos);
    
    //assign the same onchange behaviour to all fields
    let filterFields = Array.from(document.querySelectorAll('select'))
    filterFields.map(field => field.onchange = changeResults);
    
}

function changeResults(evnt) {
    //What filters are to applied?
    const filters = getFilters();
    console.log(filters)
    
    
    let filteredResults = autos.filter( auto => {
        //it any filter doesn't match don't pass this element to filteredResults
        for (filter in filters) {
            //price behaviour
            if (filter == 'minimo') {
                if( auto.preco < filters['minimo'])
                    return false;
            } else if (filter == 'maximo'){
                if (auto.preco > filters['maximo']) {
                    return false;
                }
            } else {
                if( auto[filter] != filters[filter])
                    return false;
            }
        }
        return true;
    });
    updateResults(filteredResults);
}

function updateResults( autoList ) {
    const resutado = document.getElementById('resultado');
    resultado.innerHTML = '';
    autoList.forEach( auto => {
        let elemt = document.createElement('p');
        //TODO: acertar a formatação dos resultados
        elemt.innerText = Object.values(auto).join(' - ');
        resultado.appendChild(elemt);
    });
}

/**
 * Returns an object with every filter to be applied in the database.
 */
function getFilters() {
    let filters = {};
    filters['marca'] = document.getElementById('marca').value;
    filters['year']  = document.getElementById('year').value;
    filters['minimo']  = document.getElementById('minimo').value;
    filters['maximo']  = document.getElementById('maximo').value;
    filters['portas']  = document.getElementById('portas').value;
    filters['transmissao']  = document.getElementById('transmissao').value;
    filters['cor']  = document.getElementById('cor').value;

    for(filter in filters) {
        if ( filters[filter] == '') {
            delete filters[filter];
        }
    }

    return filters;
}