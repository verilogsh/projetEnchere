using Enchere.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Enchere.Models;
using System.Threading;
using System.Globalization;
using Enchere.Controllers;

namespace Enchere.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Langue()
        {
            string langue = Request.Form["choixLangue"];
            HttpCookie cookie = new HttpCookie("Cookie");
            cookie.Value = langue;
            ViewBag.Langue = cookie.Value;
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
        }

       

        [HttpGet]
        public ActionResult SendEmail()
        {
            ViewBag.Message = "Your contact page.";
            LangueController.CreateCulture(getLangue());
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(String message, String subject, String emetteur)
        {
            try
            {
                var senderemail = new MailAddress("hasnimed07@gmail.com", "Demo test");
                var receiverEmail = new MailAddress(emetteur, "recepteur");

                var password = "hasni1985";
                var sub = subject;
                var body = message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(senderemail.Address, password)
                };
                using (var mess = new MailMessage(senderemail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception ex)
            {
                // Status.Text = ex.Message;
                //ViewBag.Error = "Probleme d'envoie d'email";
                ViewBag.Error = ex.ToString();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SendNewPass()
        {
            LangueController.CreateCulture(getLangue());
            return View();
        }


       [HttpPost]
        public ActionResult SendNewPass(string courriel)
        {
            try
            {
                string emetteur = Request.Form["monEmail"];
                var senderemail = new MailAddress("hasnimed07@gmail.com", "demoo");
                var receiverEmail = new MailAddress(emetteur, "recepteur");

                var password = "hasni1985";
                var sub = "Réinintialisation du mot de passe";
                var body = Utility.IdGenerator.getObjetId();
                MembreRequette.UpdateMDP(emetteur, body);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(senderemail.Address, password)
                };
                using (var mess = new MailMessage(senderemail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception ex)
            {
                // Status.Text = ex.Message;
                //ViewBag.Error = "Probleme d'envoie d'email";
                ViewBag.Error = ex.ToString();
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PageRapports()
        {
            LangueController.CreateCulture(getLangue());
            ViewBag.Rapports = "Rapports";

            return View();
        }

        public ActionResult PageCommission()
        {
            LangueController.CreateCulture(getLangue());
            ViewBag.Commission = CommissionRequette.ChercherCommission();
            return View();
        }

        [HttpPost]
        public ActionResult ModifierCommission()
        {
            string commission = Request.Form["Taux"];
            CommissionRequette.ChangerCommission(commission);
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
        }

        /////////////////////// added by Haiqiang Xu  ///////////////////////
        [HttpGet]
        public ActionResult ViewVendeur(string id) {
            LangueController.CreateCulture(getLangue());
            Membre mb = MembreRequette.GetUserByNumero(id);
            ViewBag.Id = id;
            ViewBag.Cote = mb.Cote;
            List<Evaluation> list = EvaluationRequette.getEvaluationsByIdMembre(id);
            return View(list);
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