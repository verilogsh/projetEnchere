using Enchere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enchere.Dal
{
    public class EnchereController : Controller
    {
        [HttpGet]
        public ActionResult MettreEnVente(string idObjet, string idVendeur, bool vente) {
            Encher en = new Encher("0", idObjet, idVendeur, idVendeur, 0, 0, DateTime.Now, DateTime.Now, 0);
            return View(en);
        }

        [HttpPost]
        public ActionResult MettreEnVente(Encher encher) {
            encher.Id = Utility.IdGenerator.getEncherenId();
            encher.Etat = 0;
            EnchereRequette.insertEncher(encher);
            ObjetRequette.setObjetEnVente(encher.IdObjet);
            return RedirectToAction("gestionObjetMembre", "Objet");
        }

        [HttpGet]
        public ActionResult getEnchereAcheteur(int etat) {
            if (ModelState.IsValid) {
                string currentUser = @User.Identity.Name;
                Membre mb = MembreRequette.GetUserByEmail(currentUser);
                List<Encher> list = EnchereRequette.getEncheresAcheteur(mb.Numero, etat);
                List<EnchereViewModel> listObj = new List<EnchereViewModel>();
                foreach (Encher en in list) {
                    Objet obj = ObjetRequette.getObjetById(en.IdObjet);
                    EnchereViewModel model = new EnchereViewModel(obj.Id, obj.Nom, obj.Description, obj.DateInscri, obj.IdCategorie, obj.Photo, obj.Piece, obj.IdMembre, obj.Nouveau, obj.EnVent, obj.PrixDepart, en.PrixAchat, en.Id, en.Etat);
                    listObj.Add(model);
                }

                if (etat == 1) {
                    ViewBag.option = "Les objets achetes:";
                } else if (etat == 0) {
                    ViewBag.option = "Les encheres participes:";
                }
                return View(listObj);
            }
            return View();
        }

        [HttpGet]
        public ActionResult getEnchereVendeur(int etat) {
            if (ModelState.IsValid) {
                string currentUser = @User.Identity.Name;
                Membre mb = MembreRequette.GetUserByEmail(currentUser);
                List<Encher> list = EnchereRequette.getEncheresVendeur(mb.Numero, etat);
                List<EnchereViewModel> listObj = new List<EnchereViewModel>();
                foreach (Encher en in list) {
                    Objet obj = ObjetRequette.getObjetById(en.IdObjet);
                    EnchereViewModel model = new EnchereViewModel(obj.Id, obj.Nom, obj.Description, obj.DateInscri, obj.IdCategorie, obj.Photo, obj.Piece, obj.IdMembre, obj.Nouveau, obj.EnVent, obj.PrixDepart, en.PrixAchat, en.Id, en.Etat);
                    listObj.Add(model);
                }
                if (etat == 0) {
                    ViewBag.option = "Les encheres en cours:";
                } else if (etat == 1) {
                    ViewBag.option = "Les encheres remportes:";
                } else if (etat == 2) {
                    ViewBag.option = "Les encheres perdues:";
                }

                //if ()
                return View(listObj);
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateEnchere(string id) {
            Encher en = EnchereRequette.getEnchereById(id);
            return View(en);
        }

        [HttpPost]
        public ActionResult UpdateEnchere(Encher en) {
            Encher en1 = en;
            EnchereRequette.updateEnchere(en);
            return RedirectToAction("Index", "Home");
        }
    }
}