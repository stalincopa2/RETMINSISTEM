using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
  
    public class AutorizacionUser:AuthorizeAttribute
    {
        private USUARIO objUsuario;
       // private RETMINDBEntities DB = new RETMINDBEntities();
        private int[] ROL;
        private int ID_ROL;
   
        public AutorizacionUser(params int[] ROL)
        {
            this.ROL = ROL;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {   

            String nombreRol=" ";
             try
             {
                ID_ROL = Convert.ToInt32(HttpContext.Current.Session["User"].ToString());
      
               
                for (int i = 0; i < ROL.Length; i++)
                {
                   if (ID_ROL != this.ROL[i])
                   {
                        if (i == (ROL.Length - 1))
                        {
                            nombreRol = getNombreRol(ID_ROL);
                            nombreRol = nombreRol.Replace(" ", "+");
                            filterContext.Result = new RedirectResult("~/Error/UnatorizedOperation?ROL=" + nombreRol);
                        }
                   }
                    else
                    {
                        Roles = ID_ROL.ToString();
                        i = ROL.Length + 1;
                    }
                }
            }catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnatorizedOperation?ROL=" + nombreRol);
            }
        }

        public String getNombreRol(int ROL_USUARIO)
        {
            String nombreRol = " ";
            try
            {

                switch (ROL_USUARIO)
                {
                    case 1:
                        nombreRol = "Gerente General";
                        break;
                    case 2:
                        nombreRol = "Gerente Sucursal";
                        break;
                    case 3:
                        nombreRol = "Bodeguero";
                        break;
                }
            }
            catch (Exception)
            {
                nombreRol = " ";
            }
            return nombreRol;
        }
    }  
}