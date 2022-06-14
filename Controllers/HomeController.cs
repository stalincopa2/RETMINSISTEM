using RETMINSISTEM.Filters;
using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();

        // GET: kardex


        public ActionResult Index()
        {
            return View();
        }


        [AutorizacionUser(ROL: new int[] {1,2})]
        public ActionResult usuarios()
        {
            ViewBag.Message = "Your application description page.";

            return View();

        }

        public ActionResult Inventario()
        {
            ViewBag.Message = "Ahora estas en el inventario.";

            return View();
        }
        public ActionResult Movimiento()
        {
            ViewBag.Message = "Aqui deberia aparecer una lista de todos los productos";


            int ID_SUCURSAL = Convert.ToInt32(Session["ID_SUCURSAL"].ToString());

            var kARDEX = from k in db.KARDEX
                         join j in db.USUARIO
                         on k.ID_USUARIO equals j.ID_USUARIO
                         where j.ID_SUCURSAL == ID_SUCURSAL
                         select k;
            return View(kARDEX.ToList());
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
        }
    }
}