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
        private USUARIO objUsuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                objUsuario = (USUARIO)HttpContext.Current.Session["User"];
                if (objUsuario == null) { 
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