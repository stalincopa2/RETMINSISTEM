using RETMINSISTEM.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [autorizacionUser(ROL: 1)]
        public ActionResult usuarios()
        {
            ViewBag.Message = "Your application description page.";

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
        public ActionResult Inventario()
        {
            ViewBag.Message = "Ahora estas en el inventario.";

            return View();
        }
    }
}