﻿using Enchere.Dal;
using Enchere.Models;
using Enchere.Models.ViewModel;
using Enchere.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enchere.Controllers
{
    public class EvaluationController : Controller
    {
        [HttpGet]
        public ActionResult Create(string idEnchere, string op, string rtnUrl) {
            Encher en = EnchereRequette.getEnchereById(idEnchere);
            Evaluation ev = null;
            if (op == "acheteur") {
                ev = new Evaluation("acheteur", idEnchere, DateTime.Now, 0, en.IdAcheteur, en.IdVendeur, "");
            } else if(op == "vendeur") {
                ev = new Evaluation("vendeur", idEnchere, DateTime.Now, 0, en.IdVendeur, en.IdAcheteur, "");
            }

            ViewBag.rtn = rtnUrl;
            return View(ev);         
        }

        [HttpPost]
        public ActionResult Create(Evaluation ev) {
            Encher en = EnchereRequette.getEnchereById(ev.IdEnchere);
            int etat = en.Etat;

            if (ModelState.IsValid) {
                if (ev.Id == "acheteur") {
                    if (etat == 1) {
                        EnchereRequette.setEnchereEtat(ev.IdEnchere, 3);
                    } else if (etat == 4) {
                        EnchereRequette.setEnchereEtat(ev.IdEnchere, 5);
                    }
                    
                } else if (ev.Id == "vendeur") {
                    if (etat == 1) {
                        EnchereRequette.setEnchereEtat(ev.IdEnchere, 4);
                    } else if (etat == 3) {
                        EnchereRequette.setEnchereEtat(ev.IdEnchere, 5);
                    }
                }
                ev.Id = Utility.IdGenerator.getEvaluationId();
                EvaluationRequette.insertEvaluation(ev);
                ////// update cote d'utilisateur  /////////////////
                return RedirectToAction("Index","Home");
            }
            return null;
        }

        [HttpGet]
        public ActionResult List(string idEnchere, string op, string rtnUrl) {
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
    }
}
