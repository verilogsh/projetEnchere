﻿using System;
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

namespace Enchere.Controllers
{
    public class ObjetController : Controller
    {
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
        public ActionResult gestionObjetMembre(string ordre = "none")
        {
            string currentUser = @User.Identity.Name;
            List<Objet> list = new List<Objet>();            
            Membre mb = MembreRequette.GetUserByEmail(currentUser);
            list = ObjetRequette.getObjetMembre(mb.Courriel, ordre);
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

        [HttpGet]
        public ActionResult MettreEnVente(string id, string idVendeur, bool vente) {
            Encher en = new Encher( Utility.IdGenerator.getEncherenId(),id, idVendeur, 0, 0, DateTime.Now, DateTime.Now);
            return View(en);
        }

        [HttpPost]
        public ActionResult MettreEnVente(Encher encher) {
            ObjetRequette.insertEncher(encher);
            return RedirectToAction("gestionObjetMembre", "Objet");
        }

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
    }
}