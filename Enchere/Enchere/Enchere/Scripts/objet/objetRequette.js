$(function () {
    setCategorie();

    $(document).on("click", "a.menuItem", function () {
        alert("bbb");
        var n = "item" + this.id.toString();
        $("li").removeClass("active");
        $("li#" + n).addClass("active");
        lireObjet(this.id);
        //alert(this.id);
        //if (this.id == 0) {
        //    afficherFilm('gestionFilm/filmRequette.php', '0');
        //} else {
        //    afficherFilm('gestionFilm/filmRequette.php', this.id);
        //}
    });



    //////////////// Set category data  ///////////////////////////////////////
    function setCategorie() {
        //$.getJSON('gestionFilm/lireCategorie.php', function(data) {
        $.getJSON('/Objet/lireCategorie', function (data) {
            alert(data[0].Nom);
            $("#categorieMenu").empty();
            $("#categorieMenu").append("<li id='item0' class='active'><a href='#'  class='menuItem' id='0'>Tous les objets</a></li>");
            $.each(data, function (index, item) {
                $("#categorieMenu").append("<li id='item" + (index + 1) + "' class=''><a href='#' class='menuItem' id='" + (index + 1) + "'>" + item.Nom + "</a></li>");
            });
        });
    }



    //////////// affichage des cartes d'objet  ///////////////////////////////////
    function lireObjet(nb) {
        $('#objetList').empty();
        $.getJSON('/Objet/lireObjetEnVente', { idCategorie: nb }, function (data) {
            alert(data[0].Nom);
            $.each(data, function (index) {
                var view = {
                    id: data[index][0],
                    idEnchere: data[index][1],
                    realisateur: data[index][2],
                    prix: data[index][5],
                    pochette: data[index][6]
                };
                var template = document.getElementById('templateCard').innerHTML;
                var output = Mustache.render(template, view);
                $('#objetList').append(output);
            })
        });
    }

    //////////// ajouter au panier  ///////////////////////////////////
    function ajouterAuPanier(id) {
        alert(id);
        $.getJSON("gestionFilm/filmRequette.php", { id: id, op: 'ajouterPanier' }, function (data) {

        });
    }

    //////////// supprimer de panier  ///////////////////////////////////
    function supprimerDePanier(id) {
        alert(id);
        $.getJSON("gestionFilm/filmRequette.php", { id: id, op: 'supprimerPanier' }, function (data) {
            alert(data);
            afficherPanier();
        });
    }

})