using RETMINSISTEM.Controllers;
using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Filters
{
    public class Verificacion: ActionFilterAttribute
    {
        private object objSesion;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                objSesion = HttpContext.Current.Session["User"];

                if (objSesion == null) { 
                    if (filterContext.Controller is LoginController == false)
                    {  
                        filterContext.HttpContext.Response.Redirect("/Login/Login");
                    }
                }
               }catch
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
          
        }

    }
}