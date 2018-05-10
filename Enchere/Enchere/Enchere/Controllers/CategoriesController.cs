using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Enchere.Dal;
using System.Threading;
using System.Globalization;
using Enchere.Models.ViewModel;
using Enchere.Models;
using System;
using System.Linq;

namespace Enchere.Controllers
{
    public class CategoriesController : Controller
    {
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
        // GET: Categories
        public ActionResult ListeCategories(string order)
        {
            CreateCulture(getLangue());
            ViewBag.IdOrder = "Id";
            ViewBag.NomOrder = "Nom";
            ViewBag.NomOrder = String.IsNullOrEmpty(order) ? "Nom" : "";
            if (order == null) order = "Nom";
            ViewBag.Cat = CategoriesRequette.lesCategories(order);
            return View();
        }

        [HttpGet]
        public ActionResult AjouterCateg()
        {
            CreateCulture(getLangue());

            return View();
        }

        [HttpPost]
        public ActionResult AjouterCateg(Categorie c)
        {
            if (ModelState.IsValid)
            {
                CategoriesRequette.Add(c);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ModifierCateg()
        {
            CreateCulture(getLangue());
            // string nr = Request.QueryString["Id"];
            string nr = Request.Url.AbsolutePath.Split('/').Last();
            Categorie c = CategoriesRequette.GetCategorieById(nr);
            return View(c);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ModifierCateg(Categorie c)
        {
            if (ModelState.IsValid)
            {
                CategoriesRequette.Update(c);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DeleteCateg()
        {
            CreateCulture(getLangue());
            //string nr = Request.QueryString["Id"];
            string nr = Request.Url.AbsolutePath.Split('/').Last();
            Categorie c = CategoriesRequette.GetCategorieById(nr);
            return View(c);
        }

        [HttpPost]
        public ActionResult DeleteCateg(Categorie c)
        {
            CategoriesRequette.Supprimer(c);
            return RedirectToAction("Index", "Home");
        }
    }
}