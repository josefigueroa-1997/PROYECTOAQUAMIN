document.getElementById('searchinput').addEventListener('input',function(){

    var searchtext = this.value.toLowerCase();
    var rows = document.getElementById('tablaUsuarios').getElementsByTagName('tr');

    for(var i= 0;i< rows.length;i++){
        var cells = rows[i].getElementsByTagName('td');
        var found = false;

        for(var j = 0;j<cells.length;j++){
            var cellstext = cells[j].textContent.toLocaleLowerCase();
            if(cellstext.indexOf(searchtext)>-1){
                found = true;
                break;
            }
        }
        if(found){
            rows[i].style.display = '';
        }
        else{
            rows[i].style.display= 'none';
        }
    }
});
