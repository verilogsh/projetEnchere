<?php


function lister($manager){
    global $tabRes;
    $tabRes['action']="lister";
    $query="SELECT * FROM circuit";
    $tabRes['listeCircuits']=  $manager->getListQuery($query);
    //var_dump($tabRes);

}

function detailsCircuit($manager,$id,$managerEtape,$managerJour){
    global $tabRes;
    $queryEtape ='SELECT idEtape,nomEtape, nombreJour,dateArrivee,dateDepart,descriptionEtape,photoEtape FROM etape WHERE idCircuit = '.$id;

    $tabRes['listeCircuits']=  $manager->getUnique($id);
    $tabRes['listeEtapes']= fetchEtape($managerEtape,$queryEtape,$manager);
   if(count($tabRes['listeEtapes'])!=0) {
       for ($i = 0; $i < count($tabRes['listeEtapes']); $i++) {
           $queryJour = 'SELECT nomJour,nomVille,hotel,restaurant,activite,idEtape FROM jour WHERE idEtape =' . $tabRes['listeEtapes'][$i][1];
           if (count(fetchJour($managerJour, $queryJour, $managerEtape)) != 0)
               $tabRes['listeJours'][] = fetchJour($managerJour, $queryJour, $managerEtape);
       }

   }

}


function fetchEtape($manager,$query,$managerCiruit){
    $result = $manager ->getListQuery($query);
    //$cmpt = $manager->count();
    $data=array();
    if(count($result)!=0){
        foreach($result as $etape)
        {

            $image = '';
            if($etape->getPhotoEtape() != '')
            {
                $image = '<img src="../images/'.$etape->getPhotoEtape().'" class="img-thumbnail" width="50" height="35" />';
            }
            else
            {
                $image = '';
            }
            $sub_array = array();
            $sub_array[] = $image;
            $sub_array[] = $etape->getIdEtape();
            $sub_array[] = $etape->getNomEtape();

            $sub_array[] = $etape->getNombreJour();
            $sub_array[] = $etape->getDateDepart();
            $sub_array[] = $etape->getDateArrivee();
            $sub_array[] = $etape->getDescriptionEtape();
            //$sub_array[] = $managerCiruit->getUnique($etape->getIdCircuit())->getNomCircuit();

            //$sub_array[] = '<button type="button" name="update" id="'. $etape->getIdEtape().'" class="btn btn-warning btn-xs updateEtape">Update</button>';
            //$sub_array[] = '<button type="button" name="delete" id="'. $etape->getIdEtape().'" class="btn btn-danger btn-xs deleteEtape">Delete</button>';
            $data[] = $sub_array;

        }
    }
   // $data['dataEtape']=$data1;
    //$data["cmpt"]= $cmpt;
    return $data;
}


function fetchJour($manager,$query,$managerEtape){
    $result = $manager ->getListQuery($query);
    $cmpt = $manager->count();
    $data =array();
    $filtered_rows = count($result);
    foreach($result as $jour)
    {

        $sub_array = array();
        $sub_array[] = $jour->getNomJour();
        $sub_array[] = $jour->getNomVille();
        $sub_array[] = $jour->getHotel();
        $sub_array[] = $jour->getRestaurant();
        $sub_array[] = $jour->getActivite();
        $sub_array[] = $jour->getIdEtape();

        $data[]= $sub_array;

    }
    //$data['data']=$data1;
    //$data["filtered_rows"]=$filtered_rows ;
    //$data["cmpt"]= $cmpt;
    return $data;
}

//******************************************************
//Contrôleur
$action=$_POST['action'];
$tabRes=array();
include_once("lib/autoload.php");
$db = DBFactory::getMysqlConnexionWithPDO();
$manager = new CircuitManagerPDO($db);
$managerEtape = new EtapeManagerPDO($db);
$managerJour =new JourManagerPDO($db);


switch($action){
    case "details" :
        detailsCircuit($manager,$_POST['id'],$managerEtape,$managerJour);
        break;
    case "lister" :
        lister($manager);
        break;
    case "enlever" :
        enlever();
        break;
    case "fiche" :
        fiche();
        break;
    case "modifier" :
        modifier();
        break;
}
echo json_encode($tabRes);