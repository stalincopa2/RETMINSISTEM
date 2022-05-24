using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RETMINSISTEM.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
  
    public class autorizacionUser:AuthorizeAttribute
    {
        private USUARIO objUsuario;
        private RETMINEntities1 DB = new RETMINEntities1();
        private int[] ROL;
   
        public autorizacionUser(params int[] ROL)
        {
            this.ROL = ROL;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreRol=" ";
             try
            {
                objUsuario = (USUARIO)HttpContext.Current.Session["User"];
                for ( int i=0; i < ROL.Length; i++)
                {
                    var auxRol = ROL[i];
                    var Roles = from m in DB.USUARIO
                                where m.ROL == auxRol
                                && m.COD_USUARIO == objUsuario.COD_USUARIO
                                select m;

                    if (Roles.ToList().Count() == 0  )
                    {
                        if (i == (ROL.Length - 1)){
                            nombreRol = getNombreRol(objUsuario.COD_USUARIO);
                            nombreRol = nombreRol.Replace(" ", "+");
                            filterContext.Result = new RedirectResult("~/Error/UnatorizedOperation?ROL=" + nombreRol);
                        }

                    }
                    else
                    {
                        i = ROL.Length + 1;
                    }

                }
              

                
                
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnatorizedOperation?ROL=" + nombreRol);
            } 
        }


        public String getNombreRol(String CODUSUARIO)
        {
            var permiso = from A in DB.USUARIO
                          where A.COD_USUARIO == CODUSUARIO
                          select A.ROL;
            String nombreRol = " ";
            try
            {
                switch (permiso.First())
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