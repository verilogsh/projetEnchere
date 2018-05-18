using Enchere.Dal;
using Enchere.Models;
using Enchere.Models.ViewModel;
using Enchere.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Enchere.Controllers
{
    public class EvaluationController : Controller
    {
       
        [HttpGet]
        public ActionResult Create(string idEnchere, string op, string rtnUrl) {
            LangueController.CreateCulture(getLangue());
            Encher en = EnchereRequette.getEnchereById(idEnchere);
            Evaluation ev = null;
            if (op == "acheteur") {
                ev = new Evaluation("acheteur", idEnchere, DateTime.Now, 0, "", en.IdAcheteur, en.IdVendeur);
            } else if(op == "vendeur") {
                ev = new Evaluation("vendeur", idEnchere, DateTime.Now, 0, "", en.IdVendeur, en.IdAcheteur);
            }

            ViewBag.rtn = rtnUrl;
            return View(ev);         
        }

        [HttpPost]
        public ActionResult Create(Evaluation ev) {
            if (ModelState.IsValid) {
                Encher en = EnchereRequette.getEnchereById(ev.IdEnchere);
                int etat = en.Etat;
                string id = "";

                if (ModelState.IsValid) {
                    if (ev.Id == "acheteur") {
                        if (etat == 1) {
                            EnchereRequette.setEnchereEtat(ev.IdEnchere, 3);
                        } else if (etat == 4) {
                            EnchereRequette.setEnchereEtat(ev.IdEnchere, 5);
                        }
                        id = en.IdVendeur;
                    } else if (ev.Id == "vendeur") {
                        if (etat == 1) {
                            EnchereRequette.setEnchereEtat(ev.IdEnchere, 4);
                        } else if (etat == 3) {
                            EnchereRequette.setEnchereEtat(ev.IdEnchere, 5);
                        }
                        id = en.IdAcheteur;
                    }
                    ev.Id = Utility.IdGenerator.getEvaluationId();
                    EvaluationRequette.insertEvaluation(ev);
                    Membre mb = MembreRequette.GetUserByNumero(id);
                    mb.Cote += ev.Cote;
                    MembreRequette.UpdateCote(mb);
                    ////// update cote d'utilisateur  /////////////////
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(ev);
        }

        [HttpGet]
        public ActionResult List(string idEnchere, string op, string rtnUrl) {
            LangueController.CreateCulture(getLangue());
            Encher en = EnchereRequette.getEnchereById(idEnchere);
            Evaluation ev = null;

            if (op == "acheteur") {
                ev = EvaluationRequette.getEvaluation(idEnchere, en.IdAcheteur);
            } else if (op == "vendeur") {
                ev = EvaluationRequette.getEvaluation(idEnchere, en.IdAcheteur);
            }    

            ViewBag.rtn = rtnUrl;
            return View(ev);
        }

        public ActionResult ListeEvaluationsMembres()
        {
            LangueController.CreateCulture(getLangue());
            List<EvaluationMembre> listObj = EvaluationRequette.getEvaluationMembre();
            return View(listObj);
        }

        public ActionResult PrintEvaluaionMembre()
        {
            LangueController.CreateCulture(getLangue());
            var list = new ActionAsPdf("ListeEvaluationsMembres");
            return list;
        }

        public ActionResult SyntheseVenteAnnuel()
        {
            LangueController.CreateCulture(getLangue());
            List<Commissions> listCommisions = EvaluationRequette.getCommissions();
            List<SyntheseVentes> synthese = EnchereRequette.getSynthese(listCommisions);
            SyntheseVentes v = synthese[synthese.Count - 1];
            ViewBag.TotalVente = v.TotalVente;
            ViewBag.TotalCom = v.TotalCommision;
            return View(synthese);
        }

        public ActionResult PrintSyntheseVenteAnnuel()
        {
            LangueController.CreateCulture(getLangue());
            var list = new ActionAsPdf("SyntheseVenteAnnuel");
            return list;
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
