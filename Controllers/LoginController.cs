using RETMINSISTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                String conectar = ConfigurationManager.ConnectionStrings["RETMINDBENTITIES"].ConnectionString;
                {
                    
                    SqlConnection con = new SqlConnection(conectar);
                    
              
                    using (con)
                    {
                        using (SqlCommand verificacionUser = new SqlCommand("DECRYPTION_PASSWORD", con))
                        {
                            verificacionUser.CommandType = CommandType.StoredProcedure;
                            verificacionUser.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = "RETMINSIS";
                            verificacionUser.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = password;
                            verificacionUser.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = User;
                            con.Open();
                            var oUser = verificacionUser.ExecuteReader(CommandBehavior.CloseConnection);
                            
                            if (oUser.Read())
                            {

                                Session["User"] = oUser["ID_ROL"];
                            }
                            else
                            {
                                ViewBag.Error = "Usuario o contracenia incorrecta";
                                return View();
                            }
                                                      
                            con.Close();
                           
                        }
                    }
                 
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