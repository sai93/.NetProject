function search() {
    var input, filter, table, tr, td, i, st;
    st = 0;
    if (document.getElementById("desc").checked == true) {
        st = 1;
    } else if (document.getElementById("time").checked == true) {
        st = 2;
    } else if (document.getElementById("log").checked == true) {
        st = 4;
    }
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("table");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[st];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function sortName() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    while (switching) {
       
        switching = false;
        rows = table.getElementsByTagName("TR");
       
        for (i = 1; i < (rows.length - 1) ; i++) {
            
            shouldSwitch = false;
            
            x = rows[i].getElementsByTagName("TD")[0];
            y = rows[i + 1].getElementsByTagName("TD")[0];
            
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
               
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function sortDes() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    
    while (switching) {
        
        switching = false;
        rows = table.getElementsByTagName("TR");
       
        for (i = 1; i < (rows.length - 1) ; i++) {
           
            shouldSwitch = false;
           
            x = rows[i].getElementsByTagName("TD")[1];
            y = rows[i + 1].getElementsByTagName("TD")[1];
           
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function sortTime() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    
    while (switching) {
        
        switching = false;
        rows = table.getElementsByTagName("TR");
        
        for (i = 1; i < (rows.length - 1) ; i++) {
            
            shouldSwitch = false;
           
            x = rows[i].getElementsByTagName("TD")[2];
            y = rows[i + 1].getElementsByTagName("TD")[2];
            
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function sortApp() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    
    while (switching) {
        
        switching = false;
        rows = table.getElementsByTagName("TR");
        
        for (i = 1; i < (rows.length - 1) ; i++) {
           
            shouldSwitch = false;
            
            x = rows[i].getElementsByTagName("TD")[3];
            y = rows[i + 1].getElementsByTagName("TD")[3];
            
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
           
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function sortLog() {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    
    while (switching) {
        
        switching = false;
        rows = table.getElementsByTagName("TR");
        
        for (i = 1; i < (rows.length - 1) ; i++) {
            
            shouldSwitch = false;
            
            x = rows[i].getElementsByTagName("TD")[4];
            y = rows[i + 1].getElementsByTagName("TD")[4];
            
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
           
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}


