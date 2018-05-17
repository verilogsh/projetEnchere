using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enchere.Models;
using Enchere.Models.ViewModel;
using Enchere.Dal;
using Enchere.Utility;
using System.Threading;
using System.Globalization;
using System.Net.Mail;
using Rotativa;

namespace Enchere.Controllers
{
    public class ObjetController : Controller
    {
        ///////////////////////////////// Haiqiang XU ////////////////////////////////////////////

        // GET: Objet
        [HttpGet]
        public ActionResult lireCategorie()
        {
            // List<Categorie> list = new List<Categorie>();
            // list = ObjetRequtte.getCategorie();
            return Json(ObjetRequette.getCategorie(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult lireObjetEnVente(string idCategorie)
        {
            List<ObjetEnchereAff> list = new List<ObjetEnchereAff>();
            list = ObjetRequette.getObjetEnVente(idCategorie);
            return Json(ObjetRequette.getObjetEnVente(idCategorie), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult gestionObjetMembre(string idCateg = "0", string ordre = "Nom") {
            List<Categorie> list1 = ObjetRequette.getCategorie();
            ViewBag.listCateg = list1;
            ViewBag.Selected = idCateg;
            string currentUser = @User.Identity.Name;
            List<Objet> list = new List<Objet>();
            Membre mb = MembreRequette.GetUserByEmail(currentUser);
            list = ObjetRequette.getObjetMembre(mb.Courriel, idCateg, ordre);
            ViewBag.IdVendeur = mb.Numero;
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Categorie> list = ObjetRequette.getCategorie();
            ViewBag.listCateg = list;
            return View();
        }

        [HttpPost]
        public ActionResult Create(ObjetViewModel model)
        {
            List<Categorie> list = ObjetRequette.getCategorie();
            ViewBag.listCateg = list;
            if (ModelState.IsValid)
            {
                string currentUser = @User.Identity.Name;
                Membre mb = MembreRequette.GetUserByEmail(currentUser);
                ObjetRequette.SavePhoto(model.Photo);
                ObjetRequette.SavePiece(model.Piece);
                Objet obj = new Objet(IdGenerator.getObjetId(), model.Nom.Trim(), model.Description.Trim(), DateTime.Now, model.Categorie.Trim(), model.Photo.FileName.Trim(), model.Piece.FileName.Trim(), mb.Numero.Trim(), true, false, model.PrixDepart);
                ObjetRequette.insertObjet(obj);
                return RedirectToAction("gestionObjetMembre");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            List<Categorie> list = ObjetRequette.getCategorie();
            ViewBag.listCateg = list;
            Objet obj = ObjetRequette.getObjetById(id);
            ObjetViewModel model = new ObjetViewModel(obj.Id, obj.Nom, obj.Description, obj.IdCategorie, obj.PrixDepart, null, null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ObjetViewModel model)
        {
            if (ModelState.IsValid)
            {
                ObjetRequette.updateObjet(model);
                List<Categorie> list = ObjetRequette.getCategorie();
                ViewBag.listCateg = list;
                return RedirectToAction("gestionObjetMembre", "Objet");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Objet obj = ObjetRequette.deleteObjetById(id);
            List<Categorie> list = ObjetRequette.getCategorie();
            ViewBag.listCateg = list;
            return RedirectToAction("gestionObjetMembre", "Objet");
        }




        ///////////////////   End of Haiqiang Xu       ////////////////////////////////////


        public ActionResult DerniersProduits(string order)
        {
            CreateCulture(getLangue());
            ViewBag.Numero = "Id";
            ViewBag.NomOrder = "Nom";
            ViewBag.NomOrder = String.IsNullOrEmpty(order) ? "Nom" : "";
            ViewBag.Description = "Description";
            ViewBag.Date = "DateInscri";
            ViewBag.Categorie = "IdCategorie";
            ViewBag.Photo = "Photo";
            ViewBag.Piece = "Piece";
            ViewBag.Idmembre = "IdMembre";
            ViewBag.Nouveau = "Nouveau";
            ViewBag.EnVente = "EnVente";
                ViewBag.PrixDepart = "PrixDepart";
            if (order == null) order = "Nom";
            ViewBag.Objets = ObjetRequette.lesProduitsRecemmentInscrits(order);
            return View();

        }

        [HttpGet]
        public ActionResult lireObjetIntersse(int idCategorie = 1)
        {
            var list = ObjetRequette.getObjetEnVente("2");
            ViewBag.list = list;
            return View();
        }

        public static void CreateCulture(string str)
        {
            if (str.IndexOf("fr") != -1)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");

            }
            else if (str.IndexOf("en") != -1)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");

            }
        }

        public string getLangue()
        {
            string str = "fr";
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie"))
            {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                return cookie;

            }
            else
                return str;
        }

        public ActionResult ObjetIntressemembre()
        {
            //string currentUser = @User.Identity.Name;
            string currentUser = "bb@bb.com";
            Membre mb = MembreRequette.GetUserByEmail(currentUser);
            return View( ObjetRequette.lesProduitsInteressants(mb.Numero,mb.Courriel));
        }

        public ActionResult PrintObjetIntressemembre()
        {
            var list = new ActionAsPdf("ObjetIntressemembre");
            return list;
        }

     

        [HttpGet]
        public ActionResult MesObjetsEnVente()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MesObjetsEnVente(String monEmail)
        {
            //string email = Request["monEmail"];
            
            Membre mb = MembreRequette.GetUserByEmail(monEmail);
            if (mb != null)
            {
                

               return RedirectToAction("ObjetsEnVenteSansAuth", new {email=monEmail});
                
            }
            else
            {
                ViewBag.Error = "Cet Email n'existe pas..! Enregistrez vous";
                return View();

            }

        }

        public ActionResult ObjetsEnVenteSansAuth(string email)
        {
            List<Objet> list = new List<Objet>();

            list = ObjetRequette.getObjetMembre(email, "0", "none");
            return View(list);
        }

     


    }
}