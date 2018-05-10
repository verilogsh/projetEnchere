using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enchere.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult ListeCategories()
        {
            return View();
        }
    }
}