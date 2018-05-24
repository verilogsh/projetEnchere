using Enchere.Models;
using Enchere.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Enchere.Models.ViewModel;
using System.Threading;
using System.Globalization;
using Enchere.Controllers;

namespace Enchere.Dal
{
    public class EnchereController : Controller
    {
      
        [HttpGet]
        public ActionResult MettreEnVente(string idObjet, string idVendeur, bool vente) {
            Encher en = new Encher("0", idObjet, idVendeur, idVendeur, 0, 0, DateTime.Now, DateTime.Now, 0);
            //  LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
            return View(en);
        }



        [HttpPost]
        public ActionResult MettreEnVente(Encher encher) {
            if (ModelState.IsValid) {
                encher.Id = Utility.IdGenerator.getEncherenId();
                encher.Etat = -1;
                EnchereRequette.insertEncher(encher);

                string formatString = "yyyyMMddHHmmss";
                string sample1 = "20180524121900";
                string sample2 = "20180917102230";
                DateTime dt1 = DateTime.ParseExact(sample1, formatString, null);
                DateTime dt2 = DateTime.ParseExact(sample2, formatString, null);

                StartScheduler sch1 = new StartScheduler();
                ObjetRequette.setObjetEnVente(encher.IdObjet, 1);
                //Encher en = EnchereRequette.getEnchereById(encher.Id);
                //en.Etat = 0;
                //EnchereRequette.updateEnchere(en);
                //sch1.Start(encher.DateDepart, encher.IdObjet, encher.Id);
                sch1.Start(dt1, encher.IdObjet, encher.Id);

                FinishScheduler sch2 = new FinishScheduler();
                //sch2.Start(encher.DateFin, encher.IdObjet, encher.Id);
                sch2.Start(dt2, encher.IdObjet, encher.Id);

                return RedirectToAction("gestionObjetMembre", "Objet");
            }
            return View(encher);
        }

        [HttpGet]
        public ActionResult getEnchereAcheteur(int etat) {
            //   LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
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
            //   LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
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
            //  LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
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
            //  LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
            var list = new ActionAsPdf("getEnchereVendeur", new { etat = 1});
            return list;
        }

        public ActionResult PrintRapprt3()
        {
            //  LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
            var list = new ActionAsPdf("ListObjetVenduSouPeu");
            return list;
        }


        [HttpGet]
        public ActionResult UpdateEnchere(string id) {
            //  LangueController.CreateCulture(getLangue());
            CreateCulture(getLangue());
            Membre mb = MembreRequette.GetUserByEmail(@User.Identity.Name);
            Encher en = EnchereRequette.getEnchereById(id);
            UpdateEnchereViewModel model = new UpdateEnchereViewModel();
            model.Id = en.Id;
            model.IdObjet = en.IdObjet;
            model.IdAcheteur = mb.Numero;
            model.IdVendeur = en.IdVendeur;
            model.PrixAchat = en.PrixAchat;
            model.PasDePrix = en.PasDePrix;
            model.DateDepart = en.DateDepart;
            model.DateFin = en.DateFin;
            model.Etat = en.Etat;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateEnchere(UpdateEnchereViewModel en) {
            if (ModelState.IsValid) {
                Encher model = new Encher();
                Encher enOld = EnchereRequette.getEnchereById(en.Id);
                Objet obj = ObjetRequette.getObjetById(en.IdObjet);

                if ((enOld.PrixAchat + enOld.PasDePrix) > en.Prix) {
                    ViewBag.err = "Au moins augmenter le prix par " + enOld.PasDePrix + "$!";
                    return View(en);
                }

                Historique his = EnchereRequette.getHistorique(en.Id);

                if (his.Prix > en.Prix + en.PasDePrix) {
                    model.PrixAchat = en.Prix + en.PasDePrix;
                    model.IdAcheteur = his.IdMembre;
                    //// Send Email to en.IdAcheteur

                } else if (his.Prix >= en.Prix) {
                    model.PrixAchat = his.Prix;
                    model.IdAcheteur = his.IdMembre;
                    //// Send Email to en.IdAcheteur
                } else {
                    model.PrixAchat = his.Prix + en.PasDePrix;
                    model.IdAcheteur = en.IdAcheteur;
                    //// Send Email to his.IdMembre
                }

                Membre mb = MembreRequette.GetUserByNumero(model.IdAcheteur);
                Utility.Mail.SendEmail(obj.Nom, model.PrixAchat, mb.Adresse);


                model.Id = en.Id;
                model.IdObjet = en.IdObjet;
                //model.IdAcheteur = en.IdAcheteur;
                model.IdVendeur = en.IdVendeur;
                //model.PrixAchat = en.Prix;
                model.PasDePrix = en.PasDePrix;
                model.DateDepart = en.DateDepart;
                model.DateFin = en.DateFin;
                model.Etat = en.Etat;

                EnchereRequette.updateEnchere(model);
                his = new Historique(0, en.IdAcheteur, en.Id, en.Prix, DateTime.Now);
                EnchereRequette.insertHistorique(his);
                ////// send E_mail  to add //////
                return RedirectToAction("Index", "Home");

            }
            return View(en);
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

    }
}