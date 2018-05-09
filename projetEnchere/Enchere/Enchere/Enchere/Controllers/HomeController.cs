using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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