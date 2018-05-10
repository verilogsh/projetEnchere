<!DOCTYPE html>
<html class="no-js">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Store One page Bootstrap theme</title>
        <meta name="description" content="">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="css/bootstrap.min.css">
        <!-- <link rel="stylesheet" href="css/font-awesome.min.css">  -->
        <link rel="stylesheet" href="css/main.css">
		<link rel="stylesheet" href="css/administrateur.css">
		
	 <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">  -->
			<!-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script> -->
			<!-- <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> -->
		
		<script language="javascript" src="Administrateur/administrateurRequetes.js"></script>
		<script language="javascript" src="Administrateur/administrateurControleurVue.js"></script>
		
        <link rel="stylesheet" href="css/animate.css">
        <link rel="stylesheet" href="css/responsive.css">
        <script src="js/vendor/modernizr-2.6.2.min.js"></script>
        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
        <script>window.jQuery || document.write('<script src="js/vendor/jquery-1.10.2.min.js"><\/script>')</script>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/plugins.js"></script>
        <script src="js/main.js"></script>
		<script src="js/global.js"></script>
        <script src="js/wow.min.js"></script>
		 <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <script>
         new WOW(
            ).init();
        </script>

        <script>
          (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
          (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
          m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
          })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

          ga('create', 'UA-57708809-1', 'auto');
          ga('send', 'pageview');

        </script>

    </head>
    <body onLoad="creerElementsListes(); peuplerProduits();">
<div id="leheader">    
	 <?php include 'menu.php';?>
</div>	   
    

	
<!--start admin       $('#contenu').show();-->
<div id="admin">
	<div id="menuAdmin">
		<br><br><br>
		<span onClick="rendreVisible('accueil'); rendreVisibleTous(); rendreInvisible('admin'); "><i class="fa fa-close" style="font-size:36px"></i> Fermer</span>
		<h1 style="text-align: center;">TABLEAU DE BORD DE L'ADMINISTRATEUR</h1>
				
				<div id="management" style="text-align: center;">
					<input type="button" value="Gestion des circuits" onClick="rendreInvisibleTous(); rendreVisible('divCircuits'); ">  <!--rendreVisible('divCircuits'); -->
					<input type="button" value="Gestion des usagers" onClick="rendreInvisibleTous(); rendreVisible('divUsagers');  ">
					<input type="button" value="Gestion des rabais" onClick="rendreInvisibleTous(); rendreVisible('divRabais');  ">
				</div>
				
				<br>
	</div>
			<div id="divCircuits" class="tdba" style="text-align: center;" >
				<br>
				<span style="text-align: right;" onClick="rendreInvisible('divCircuits')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
				<h2>Gestion des circuits</h2>
				<br>
				<input type="button" value="Créer un circuit" onClick=" rendreInvisibleTous(); rendreVisible('divEnregCircuit');  ">
				<input type="button" value="Lister les circuits" onClick="rendreInvisibleTous(); lister();">
				<input type="button" value="Modifier un circuit" onClick="rendreInvisibleTous(); rendreVisible('divFiche');   ">
				<br>
				<br>
			</div>
			
			<br>
			<div id="divUsagers" class="tdba" style="text-align: center;">
				<br>
				<span style="text-align: right;" onClick="rendreInvisible('divUsagers')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
				<h2>Gestion des usagers</h2>
				<br>
				<input type="button" value="Lister les usagers(R)" onClick="rendreInvisibleTous(); rendreInvisible('divUsagers');  ">
				<input type="button" value="Réactiver un usager(U)" onClick="rendreInvisibleTous(); rendreInvisible('divUsagers'); rendreVisible('divReactiver');">
				<input type="button" value="Désactiver un usager(U)" onClick="rendreInvisibleTous(); rendreInvisible('divUsagers'); rendreVisible('divDesactiver');">
				<br>
				<br>
			</div>
			
			<br>
			<div id="divRabais" class="tdba" style="text-align: center;">
				<br>
				<span style="text-align: right;" onClick="rendreInvisible('divRabais')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
				<h2>Gestion des rabais</h2>
				<br>
				<input type="button" value="Enregistrer un rabais(C)" onClick="rendreInvisibleTous(); rendreInvisible('divRabais'); rendreVisible('divEnregRabais'); ">
				<input type="button" value="Lister les rabais(R)" onClick="rendreInvisibleTous(); rendreInvisible('divRabais'); ">
				<input type="button" value="Désactiver un rabais(U)" onClick="rendreInvisibleTous(); rendreInvisible('divRabais'); rendreVisible('divDesactiverRabais'); ">
				<br>
				<br>
			</div>
			
			
			<!-- FORMULAIRES -->
			
		<div id="divEnregCircuit">
			
				<form id="formEnregCircuit">
					<br>
					<span onClick="rendreInvisible('divEnregCircuit')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
					<h3>Créer un circuit</h3><br>
					<table border=0 class="table-striped table-hover table-responsive fifty">
						<tr><td><label for="nomCircuit">Nom du circuit: </label></td><td><input type="text" id="nomCircuit" name="nomCircuit"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="nbPlacesMinimum">Nombre minimum de places: </label></td>
							<td><select id="nbPlacesMinimum" name="nbPlacesMinimum"></select></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="nbPlacesMaximum">Nombre maximum de places: </label></td>
							<td><select id="nbPlacesMaximum" name="nbPlacesMaximum"></select></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="dateDepart">Date de départ: </label></td><td><input type="date" id="dateDepart" name="dateDepart"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="dateArrivee">Date de retour: </label></td><td><input type="date" id="dateArrivee" name="dateArrivee"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="prixCircuit">Prix du circuit: </label></td><td>
							<input type="number" step="0.01" min="0" id="prixCircuit" name="dateRetour"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="guide">Nom du guide: </label></td><td><input type="text" id="guide" name="guide"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="transport">Transport: </label></td><td><input type="text" id="transport" name="transport"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="etat">Circuit actif: </label></td>
							<td><select id="etat" name="etat"><option value="1">Actif</option><option value="0">Inactif</option></select></td></tr>							
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="photoCircuit">Image de circuit: </label></td><td><input type="file" id="photoCircuit" name="photoCircuit"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><input type="button" value="Créer le circuit" onClick="rendreInvisibleTous(); enregistrerCircuit();"></td><td>
					</form>				
								<input type="button" value="Ajouter une étape" onClick="rendreInvisibleTous(); rendreVisible('divEnregEtape'); "></td></tr>
						<tr><td><br></td><td><br></td></tr>
					</table>
			
		</div>
				<div id="divEnregEtape">
					<form id="formEnregEtape">
						<br>
						<span onClick="rendreInvisible('formEnregEtape')">FERMER X</span>
						<h3>Créer étape</h3><br>
						<table border=0 class="table-striped table-hover table-responsive fifty">
							<tr><td><label for="nomEtape">Nom Etape: </label></td><td><input type="text" id="nomEtape" name="nomEtape"></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><label for="nombreJour">Nombre de jours: </label></td>
								<td><select id="nombreJour" name="nombreJour"></select></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><label for="dateArrivee">Date arrivee: </label></td><td><input type="date" id="dateArrivee" name="dateArrivee"></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><label for="dateDepart">Date de départ: </label></td><td><input type="date" id="dateDepart" name="dateDepart"></td></tr>
							<tr><td><br></td><td><br></td></tr>	
							<tr><td><label for="descriptionEtape">Description Etape: </label></td><td><textarea cols="30" rows="4" id="descriptionEtape" name="descriptionEtape"></textarea></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><label for="photoEtape">Photo Etape: </label></td><td><input type="file" id="photoEtape" name="photoEtape"></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><input type="button" value="Créer l'étape" onClick="enregistrerEtape();"></td>
									<td><input type="button" value="Ajouter une journée" onClick="rendreInvisibleTous(); rendreVisible('divEnregJour');"></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><br></td><td><br></td></tr>
							<tr><td><br></td><td><br></td></tr>
						</table>
					</form>		
				</div>
					
					<div id="divEnregJour">
						<form id="formEnregJour">
							<br>
							<span onClick="rendreInvisible('formEnregJour')">FERMER X</span>
							<h3>Créer journée</h3><br>
							<table border=0 class="table-striped table-hover table-responsive fifty">
								<tr><td><label for="nomJour">Nom jour: </label></td><td><input type="text" id="nomJour" name="nomJour"></td></tr>
								<tr><td><br></td><td><br></td></tr>
								<tr><td><label for="nomVille">Nom ville: </label></td><td><input type="text" id="nomVille" name="nomVille"></td></tr>
								<tr><td><br></td><td><br></td></tr>
								<tr><td><label for="hotel">Hotel: </label></td><td><input type="text" id="hotel" name="hotel"></td></tr>
								<tr><td><br></td><td><br></td></tr>
								<tr><td><label for="restaurant">Restaurant: </label></td><td><input type="text" id="restaurant" name="restaurant"></td></tr>
								<tr><td><br></td><td><br></td></tr>
								<tr><td><input type="button" value="Créer la journée" onClick="rendreInvisibleTous(); enregistrerJour();"></td><td></td></tr>
							</table>
						</form>
					</div>
				
			
			<div id="divFiche">
				<form id="formFiche">
					<br>	
					<span onClick="rendreInvisible('divFiche')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
					<h3>Modifier un circuit</h3>
					<br>
				<table border=0 class="table-striped table-hover table-responsive">
					<tr><td><label for="idCircuitF">Numéro du circuit: </label></td><td><select id="idCircuitF" name="idCircuitF"></select></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><input type="button" value="Envoyer" onClick="rendreInvisibleTous(); obtenirFiche(); "></td><td></td></tr>					
				</table>
				</form>
			</div>
			
			
			<div id="divFormFiche" style="position:absolute;top:10%;left:50%; display:none">
				<form id="formFicheC">
					<h3></h3><br><br>
					<span onClick="rendreInvisible('divFormFiche')"><i class="fa fa-close" style="font-size:16px"></i></span><br>
					<input type="hidden" id="idCircuitFC" name="idCircuitFC">
				<table border=0 class="table-striped table-hover table-responsive">
					<tr><td><label for="nomCircuitFC">Nom du circuit: </label></td><td><input type="text" id="nomCircuitFC" name="nomCircuitFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="nbPlacesMinimumFC">Nombre minimum de places: </label></td><td><select id="nbPlacesMinimumFC" name="nbPlacesMinimumFC"></select></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="nbPlacesMaximumFC">Nombre maximum de places: </label></td><td><select id="nbPlacesMaximumFC" name="nbPlacesMaximumFC"></select></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="dateDepartFC">Date de départ: </label></td><td><input type="date" id="dateDepartFC" name="dateDepartFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="dateArriveeFC">Date de retour: </label></td><td><input type="date" id="dateArriveeFC" name="dateArriveeFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="prixCircuitFC">Prix du circuit: </label></td><td><input type="number" step="0.01" min="0" id="prixCircuitFC" name="prixCircuitFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="guideFC">Nom du guide: </label></td><td><input type="text" id="guideFC" name="guideFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="transportFC">Transport: </label></td><td><input type="text" id="transportFC" name="transportFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="etatFC">Circuit actif: </label></td><td><select id="etatFC" name="etatFC"><option value="1">Actif</option><option value="0">Inactif</option></select></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><label for="photoCircuitFC">Image de circuit: </label></td><td><input type="file" id="photoCircuitFC" name="photoCircuitFC"></td></tr>
					<tr><td><br></td><td><br></td></tr>
					<tr><td><input type="button" value="Modifier" onClick="modifier(); rendreInvisibleTous();"></td><td></td></tr>
				</table>
				</form>
			</div>
			
			<div id="divReactiver">
				<form id="formReactiverUsager">
					<h3>Reactiver usager</h3><br><br>
					<span onClick="rendreInvisible('divReactiver')">Fermer <i class="fa fa-close" style="font-size:16px"></i></span><br>
					
					<table border=0 class="table-striped table-hover table-responsive">
						<tr><td><label for="idUsager">Id usager: </label></td><td><select id="idUsager" name="idUsager"></select></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><input type="button" value="Reactiver" onClick="rendreInvisibleTous(); reactiverCetUsager();"></td><td></td></tr>
						<tr><td><br></td><td><br></td></tr>
					</table>
				</form>					
			</div>
			
			<div id="divDesactiver">
				<form id="formDesactiverUsager">
					<h3>Desactiver usager</h3><br><br>
					<span onClick="rendreInvisible('divDesactiver')">Fermer <i class="fa fa-close" style="font-size:16px"></i></span><br>
					
					<table border=0 class="table-striped table-hover table-responsive">
						<tr><td><label for="idUsagerDes">Id usager: </label></td><td><select id="idUsagerDes" name="idUsagerDes"></select></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><input type="button" value="Desactiver" onClick="rendreInvisibleTous(); desactiverCetUsager();"></td><td></td></tr>
						<tr><td><br></td><td><br></td></tr>
					</table>
				</form>					
			</div>
			
				
			<div id="divEnregRabais">
			
				<form id="formEnregRabais">
					<br>
					<span onClick="rendreInvisible('divEnregRabais')">FERMER <i class="fa fa-close" style="font-size:16px"></i></span>
					<h3>Créer un rabais</h3><br>
					<table border=0 class="table-striped table-hover table-responsive fifty">
						<tr><td><label for="nomRabais">Nom du rabais: </label></td><td><input type="text" id="nomRabais" name="nomRabais"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="description">Description du rabais: </label></td><td><textarea rows="4" cols="30" id="description" name="description"></textarea></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="pourcentage">Pourcentage: </label></td><td><input type="number" step="0.01" min="0" id="pourcentage" name="pourcentage"></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><label for="codePromo">CodePromo: </label></td><td><input type="text" id="codePromo" name="codePromo"></td></tr>
						<tr><td><br></td><td><br></td></tr>						
						<tr><td><input type="button" value="Créer le rabais" onClick="rendreInvisibleTous(); enregistrerRabais();"></td><td></td></tr>
						<tr><td><br></td><td><br></td></tr>
					</table>
				</form>			
			</div>
			
			<div id="divDesactiverRabais">
				<form id="formDesactiverRabais">
					<h3>Desactiver rabais</h3><br><br>
					<span onClick="rendreInvisible('divDesactiverRabais')">Fermer <i class="fa fa-close" style="font-size:16px"></i></span><br>
					
					<table border=0 class="table-striped table-hover table-responsive">
						<tr><td><label for="idRabaisDes">Id usager: </label></td><td><select id="idRabaisDes" name="idRabaisDes"></select></td></tr>
						<tr><td><br></td><td><br></td></tr>
						<tr><td><input type="button" value="Desactiver rabais" onClick="rendreInvisibleTous(); desactiverCeRabais();"></td><td></td></tr>
						<tr><td><br></td><td><br></td></tr>
					</table>
				</form>
			</div>
			
		<!--	<div id="divFormFicheE" style="position:absolute;top:10%;left:50%; display:none">
				<form id="formFicheE">
					<br><br>
					<span onClick="rendreInvisible('divFormFicheE')">X</span><br>

				</form>
			</div>


			<div id="divFormFicheJ style="position:absolute;top:10%;left:50%; display:none">
				<form id="formFicheJ">
					<br><br>
					<span onClick="rendreInvisible('divFormFicheJ')">X</span><br>

				</form>
			</div>  -->
			
			<div id="contenu"></div>
			
			<div id="messages">
			</div>	
			
			
		</div>
<!--fin admin-->

<!--start accueil-->
<div id="accueil">
    <section id="home">
        <div class="container">
		
		<br><br><br><br><br><br>
			<div id="carousel-example-generic" class="carousel slide" data-ride="carousel" style="width:750px; height:500px; margin:auto;">
			  <!-- Indicators -->
			  <ol class="carousel-indicators"></ol>
			  <!-- Wrapper for slides -->
			  <div class="carousel-inner"></div>
			  <!-- Controls -->
			  <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
				<span class="glyphicon glyphicon-chevron-left"></span>
			  </a>
			  <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
				<span class="glyphicon glyphicon-chevron-right"></span>
			  </a>
			</div>
        </div>
		<!--fin carousel-->
    </section>
	<br><br><br>



    <section id="services">
        <div class="container">
            <h1 class="title">Services</h1>
            <hr class="divider" style="width:50%;border-color:#CCC;">
            <p class="text-center">Vous exploitez une entreprise touristique québécoise et rêvez d’être connu de plus de sept millions de touristes du monde entier? N'attendez plus pour réaliser votre rêve! Inscrivez-vous sur notre site dès maintenant! </p>
            <div class="service-wrapper">
                <div class="row">
                    <div class="col-md-3 col-sm-6">
                        <div class="block wow fadeInRight" data-wow-delay="1s">
                            <div class="icon">
                               <i class="fa fa-plane"></i></i>
                            </div>
                            
                            <h3>Transportation</h3>
                            <p>Notre équipe met à votre disposition le meilleur transport pour assurer au maximum votre confort.</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <div class="block wow fadeInRight" data-wow-delay="1.3s">
                            <div class="icon">
                                <i class="fa  fa-list"></i>
                            </div>
                            <h3>200+ Produits</h3>
                            <p>Circuits en Europe, Afrique, Asie et Amérique. Découvrez nos offres de voyage!</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <div class="block wow fadeInRight" data-wow-delay="1.6s">
                            <div class="icon">
                                <i class="fa  fa-shopping-cart"></i>
                            </div>
                            <h3>Réservation et paiement en ligne</h3>
                            <p>Si vous revez d'un voyage inoubliable et que vous disposez d'un compte Paypal, n'attendez plus!</p>
                        </div>
                    </div>
                     <div class="col-md-3 col-sm-6">
                        <div class="block wow fadeInRight" data-wow-delay="1.9s">
                            <div class="icon">
                                <i class="fa fa-cc-paypal"></i>
                            </div>
                            <h3>PayPal accepté</h3>
                            <p>Faites votre réservation en toute securité!</p>
                        </div>
                    </div>
                </div>
            </div>
        
    </section>



    <section id="products">
        <div class="container">
            <h1 class="title">Produits</h1>
            <hr class="divider" style="width:50%;">
            <h2 class="text-center">Découvrez nos circuits:</h2>
          
			<section id="peuplerProduits">	                
				<!--peupler circuits-->  
			</section>
        </div>
		<br><br>
		<div class="text-center">
                    <ul class="pagination pagination-lg wow fadeInUp" data-wow-delay="1.1s">
                        <li class="active"><a href="#">1</a></li>
                        <li><a href="#">2</a></li>
                        <li><a href="#">3</a></li>
                        <li><a href="#">4</a></li>
                        <li><a href="#">5</a></li>
                        <li><a href="#">6</a></li>
                        <li><a href="#">7</a></li>
                        <li><a href="#">8</a></li>
                    </ul>
		</div>
       
    </section>





    <section id="testimonials">
        <div id="carousel-example-generict" class="carousel slide" data-ride="carousel">

            <ol class="carousel-indicators">
                <li data-target="#carousel-example-generict" data-slide-to="0" class="active"></li>
                <li data-target="#carousel-example-generict" data-slide-to="1"></li>
            </ol>


            <div class="carousel-inner" role="listbox">

                <div class="item active carousel2">
                    <div class="carousel-caption">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <div class="block text-center">
                                    <img class="img-responsive img-circle center-block" src="images/testimonials/2.jpg" alt="" style="height:100px;">
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 text-center">
                                <div class="block">
                                    <h3 style="color:#ff0f37;" class="text-center">Un voyage extraordinaire - Julia Savoie</h3>
                                    <p style="color:#111;font-size:14px;">
                                        Il n’y a rien à dire. L’accueil dans la magnifique maison d’hôtes Nadir Home était superbe, on n’attendait pas tant. Merci !
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="item carousel2">
                    <div class="carousel-caption">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <div class="block text-center">
                                    <img class="img-responsive img-circle center-block" src="images/testimonials/1.jpg" alt="" style="height:100px;">
                                </div>
                            </div>
                            <div class="col-md-12 col-sm-12 text-center">
                                <div class="block">
                                    <h3 style="color:#ff0f37;" class="text-center">Très bons souvenirs! - Mark Deloit</h3>
                                    <p style="color:#111;font-size:14px;">
                                         C’est un circuit qui permet de découvrir et comprendre les contrastes du développement passé et actuel des cités antiques, des périodes byzantines et ottomanes, du régime Hoxa, et maintenant de l’ouverture vers l’Europe…. Un accueil très chaleureux des Albanais tout au long du voyage.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

        </div>
    </section>

	


    <section id="contact" class="wow fadeInUp bgc-one-top mts-section-wrapper mts-contact-section" data-wow-delay=".8s" style="margin-top:50px;margin-bottom:50px;">
        <div class="container">
            <div class="row">
                <h1 class="title">Contactez nous</h1>
                <hr class="divider" style="width:50%;">
                <p class="text-center">Contactez nous pour apprendre plus sur nos offres magnifiques.</p>
                <!-- Section Header End -->

                <div class="mts-contact-details" style="margin-top:30px;">

                    <!-- Address Area End -->

                    <!-- Contact Form -->
                    <div class="col-md-6 col-sm-6 col-xs-12 mts-contact-form wow bounceInRight">
                        <div id="contact-result"></div>
                        <div id="contact-form">
                            <div class="mts-input-name mts-input-fields">
                                <input type="text" name="name" id="name" placeholder="Nom">
                            </div>

                            <div class="mts-input-email mts-input-fields">
                                <input type="email" name="email" id="email" placeholder="Courriel">
                            </div>

                            <div class="mts-input-message mts-input-fields">
                                <textarea name="message" id="message" cols="30" rows="10" placeholder="Message"></textarea>
                            </div>

                            <input type="submit" value="ENVOYER MESSAGE" id="submit-btn">
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <p>Laissez-nous un message en complétant ce formulaire, nous y répondrons dans les meilleurs délais. 

Pour vos questions relatives à un voyage, un projet de développement ou simplement pour contacter l'un de nos membres, rendez-vous sur la fiche correspondante.</p>
                            <li><i class="fa fa-home"></i>01011 Road ave.</li>
                            <li><i class="fa fa-phone"></i>(+1) 012 3456</li>
                            <li><i class="fa fa-envelope-o"></i>john@store.com</li>
                        </ul>
                    </div>
                    <!-- Contact Form End -->

                </div>
            </div>
        </div>
    </section>

<?php include 'footer.php';?>
   
</div>



<!--fin accueil-->	

</body>
</html>
