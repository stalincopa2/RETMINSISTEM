using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Controllers
{
    public class errorController: Controller
    {
        [HttpGet]
        public ActionResult UnatorizedOperation(String operacion, String msajeErrorExceotion)
        {
            ViewBag.operacion = operacion;
            ViewBag.msajeErrorExceotion = msajeErrorExceotion;
            return View(); 
        }
    }
}