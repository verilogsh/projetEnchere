$(function () {
    setCategorie();
    lireObjet(0);

    $(document).on("click", "a.menuItem", function () {
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
            $("#categorieMenu").empty();
            $("#categorieMenu").append("<li id='item0' class='active'><a href='#'  class='menuItem' id='0'>Tous les objets</a></li>");
            $.each(data, function (index, item) {
                $("#categorieMenu").append("<li  id='item" + item.Id + "' class=''><a href='#' class='menuItem' id='" + item.Id + "'>" + item.Nom + "</a></li>");
            });
        });
    }



    //////////// affichage des cartes d'objet  ///////////////////////////////////
    function lireObjet(nb) {
        alert(nb);
        $('#objetList').empty();
        $.getJSON('/Objet/lireObjetEnVente', { idCategorie: nb }, function (data) {
            
            $.each(data, function (index) {
                //var dt1 = data[index].DateDepart;
                //alert(dt1);
                var dt2 = data[index].DateFin;
                //var dt3 = new Date(parseInt(dt1.substr(6)));
                //alert(dt3);
                var dt4 = new Date(parseInt(dt2.substr(6)));
                //alert(dt4);
                var date = formatDate(dt4);
                var view = {
                    Id: data[index].Id,
                    IdEnchere: data[index].IdEnchere,
                    Description: data[index].Description,
                    Nom: data[index].Nom,
                    PrixDepart: data[index].PrixDepart,
                    PrixActuel: data[index].PrixActuel,
                    IdVendeur: data[index].IdVendeur,
                    Image: data[index].Photo,
                    Jour: date
                };
                var template = document.getElementById('templateCard').innerHTML;
                var output = Mustache.render(template, view);
                $('#objetList').append(output);
            })
        });
    }

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }
})