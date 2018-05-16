using Enchere.Models;
using Enchere.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

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
            encher.Etat = -1;
            EnchereRequette.insertEncher(encher);

            string formatString = "yyyyMMddHHmmss";
            string sample1 = "20180514222630";
            string sample2 = "20180514223010";
            DateTime dt1 = DateTime.ParseExact(sample1, formatString, null);
            DateTime dt2 = DateTime.ParseExact(sample2, formatString, null);

            StartScheduler sch1 = new StartScheduler();
            //sch1.Start(encher.DateDepart, encher.IdObjet, encher.Id);
            sch1.Start(dt1, encher.IdObjet, encher.Id);

            FinishScheduler sch2 = new FinishScheduler();
            //sch2.Start(encher.DateFin, encher.IdObjet, encher.Id);
            sch2.Start(dt2, encher.IdObjet, encher.Id);

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

        public ActionResult ListObjetVenduSouPeu()
        {
            List<Encher> list = EnchereRequette.getEncheresRapport3();
            List<EnchereViewModel> listObj = new List<EnchereViewModel>();
            foreach (Encher en in list)
            {
                Objet obj = ObjetRequette.getObjetById(en.IdObjet);
                EnchereViewModel model = new EnchereViewModel(obj.Id, obj.Nom, obj.Description, en.DateFin, obj.IdCategorie, obj.Photo, obj.Piece, obj.IdMembre, obj.Nouveau, obj.EnVent, obj.PrixDepart, en.PrixAchat, en.Id, en.Etat);
                listObj.Add(model);
            }

           
            return View(listObj);
        
            
         }


        public ActionResult PrintObjetvendu()
        {
            var list = new ActionAsPdf("getEnchereVendeur", new { etat = 1});
            return list;
        }

        public ActionResult PrintRapprt3()
        {
            var list = new ActionAsPdf("ListObjetVenduSouPeu");
            return list;
        }


        [HttpGet]
        public ActionResult UpdateEnchere(string id) {
            Encher en = EnchereRequette.getEnchereById(id);
            return View(en);
        }

        [HttpPost]
        public ActionResult UpdateEnchere(Encher en) {
            EnchereRequette.updateEnchere(en);
            return RedirectToAction("Index", "Home");
        }
    }
}