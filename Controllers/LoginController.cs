using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string password)
        {
            try {

                using (Models.RETMINEntities1 db = new Models.RETMINEntities1())
                {
                    var oUser = (from d in db.USUARIO
                                where d.USUARIO1==User.Trim() && d.CONTRACENIA == password.Trim()
                                select d).FirstOrDefault();

                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contracenia incorrecta";
                        return View();
                    }
                    Session["User"] = oUser;

                }


            return RedirectToAction("Index", "Home");
            }
            catch(Exception ex) {
                ViewBag.Error = ex.Message;
                return View(); 
            }

        }
    }
}