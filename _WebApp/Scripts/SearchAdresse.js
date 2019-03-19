import {addActive, removeActive, closeAllLists} from "./AutocompleteFunction.js";

function is_int(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}

var el = document.getElementById("Zip");
var ville = document.getElementById("Ville");
el.onkeyup = Refresh;

var droplist = document.getElementById("noms");

function Refresh() {
    if (el.value.length == 4 && is_int(el.value)) {
        
        //Enlever toutes les options précédement récupérée
        for (var i = droplist.options.length - 1 ; i >= 0 ; i--) {
            droplist.remove(i);
        }
        
        //Requete vers le controller pour récupérer la ville
        $.ajax({
            type: "GET",
            url: "../../Script/ZipVille",
            dataType: "text",
            data: { zip: $("#Zip").val() },
            success: function (data) {
                //Si il y a plusieurs ville, les séparer
                if (data.includes(";")) {
                    var noms = data.split(";")
                    //Et les afficher dans une dropdownlist
                    for (var i = 0; i < noms.length; i++) {
                        var optn = document.createElement("OPTION");
                        optn.value = noms[i];
                        optn.text = noms[i];
                        droplist.options.add(optn);
                    }
                    //Affiche la dropdownlist et selectionne le premier element
                    droplist.style.display = "block";
                    ville.style.display = "none";
                    ville.value = droplist.options[0].text;
                }
                else {
                    ville.value = data;
                    //Affiche le champ Ville prérempli
                    droplist.style.display = "none";
                    ville.style.display = "block";
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}

droplist.onchange = setVille;

function setVille() {
    ville.value = droplist.options[droplist.selectedIndex].text;
}

function autocompleteAdresse(inp) {
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        /*réduit contenu si contient rue, place, avenue, ...*/
        var search = inp.value;
        //console.log(search.toLowerCase());
        if (search.toLowerCase().includes("rue") || search.toLowerCase().includes("aux")) {
            search = search.substr(3, search.length - 3);
        }
        if (search.toLowerCase().includes("cité") || search.toLowerCase().includes("clos") || search.toLowerCase().includes("quai")) {
            search = search.substr(4, search.length - 4);
        }
        if (search.toLowerCase().includes("place") || search.toLowerCase().includes("allée") || search.toLowerCase().includes("route")) {
            search = search.substr(5, search.length - 5);
        }
        if (search.toLowerCase().includes("avenue") || search.toLowerCase().includes("chemin") || search.toLowerCase().includes("enclos") || search.toLowerCase().includes("marais") || search.toLowerCase().includes("ruelle")) {
            search = search.substr(6, search.length - 6);
        }
        if (search.toLowerCase().includes("impasse") || search.toLowerCase().includes("passage") || search.toLowerCase().includes("sentier")) {
            search = search.substr(7, search.length - 7);
        }
        if (search.toLowerCase().includes("chaussée")) {
            search = search.substr(8, search.length - 8);
        }
        if (search.toLowerCase().includes("boulevard")) {
            search = search.substr(9, search.length - 9);
        }
        closeAllLists();
        //console.log(search);
        if (search.length >= 5){
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            
            var arr = []; 
            //Requete vers le controller pour récupérer les noms de rues
            $.ajax({
                type: "GET",
                url: "../../Script/AdresseRue",
                async: false,
                dataType: "text",
                data: { input: search, zip:el.value },
                success: function (data) {
                    arr = data.split(";");
                },
                error: function (error) {
                    console.log(error);
                }
            });

            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                b.className = "autocomplete-list";
                b.innerHTML = arr[i];
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

autocompleteAdresse(document.getElementById("NomRue"));