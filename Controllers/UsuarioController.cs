using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RETMINSISTEM.Filters;
using RETMINSISTEM.Models;

namespace RETMINSISTEM.Controllers
{
    public class usuarioController : Controller
    {
        private Model db = new Model();

        // GET: usuario
        [AutorizacionUser(ROL: new int[] { 1, 2 })]
        public ActionResult Index()
        {
            var uSUARIO = db.USUARIO.Include(u => u.ROL);
            return View(uSUARIO.ToList());
        }

        // GET: usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // GET: usuario/Create
        public ActionResult Create()
        {
            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL");
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "NOMBRE_SUCURSAL");
            return View();
        }

        // POST: usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,ID_SUCURSAL,ID_ROL,CEDULA,NOMBRE,APELLIDO,USUARIO1")] USUARIO uSUARIO, String password)
        {
            int ID_USUARIO_ACTUAL = (db.USUARIO.ToList().Count()+1);

            try
            {
                String conectar = ConfigurationManager.ConnectionStrings["RETMINDBENTITIES"].ConnectionString;
                {

                    SqlConnection con = new SqlConnection(conectar);


                    using (con)
                    {
                        using (SqlCommand insertUser = new SqlCommand("INSERT_USUARIO", con))
                        {
                           
                            if (ModelState.IsValid)
                            {
                                uSUARIO.COD_USUARIO = "U" + ID_USUARIO_ACTUAL.ToString("D9");// Generador de codigo en base al numero de registro

                                insertUser.CommandType = CommandType.StoredProcedure;
                                insertUser.Parameters.Add("@ID_ROL", SqlDbType.Int).Value = uSUARIO.ID_ROL;
                                insertUser.Parameters.Add("@ID_SUCURSAL", SqlDbType.Int).Value = uSUARIO.ID_SUCURSAL;
                                insertUser.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar).Value = uSUARIO.COD_USUARIO;
                                insertUser.Parameters.Add("@CEDULA", SqlDbType.VarChar).Value = uSUARIO.CEDULA;
                                insertUser.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = uSUARIO.NOMBRE;
                                insertUser.Parameters.Add("@APELLIDO", SqlDbType.VarChar).Value = uSUARIO.APELLIDO;
                                insertUser.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = uSUARIO.USUARIO1;
                                insertUser.Parameters.Add("@CONTRACENIA", SqlDbType.VarChar).Value = password;
                                insertUser.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = "RETMINSIS";
                                con.Open();
                                var oUser = insertUser.ExecuteNonQuery();
                                if (oUser == 1)
                                    return RedirectToAction("Index");
                                con.Close();
                            }

                        }
                    }
                }
            }
            catch
            {

            }
            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL", uSUARIO.ID_ROL);
            return View(uSUARIO);
        }

        // GET: usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }


            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL", uSUARIO.ID_ROL);
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "NOMBRE_SUCURSAL");
            return View(uSUARIO);
        }

        // POST: usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,ID_SUCURSAL, ID_ROL,CEDULA,NOMBRE,APELLIDO,USUARIO1")] USUARIO uSUARIO, string password)
        {
            try
            {
                String conectar = ConfigurationManager.ConnectionStrings["RETMINDBENTITIES"].ConnectionString;
                {

                    SqlConnection con = new SqlConnection(conectar);


                    using (con)
                    {
                        using (SqlCommand updatetUser = new SqlCommand("UPDATE_USUARIO", con))
                        {
                            if (ModelState.IsValid)
                            {
                                updatetUser.CommandType = CommandType.StoredProcedure;
                                updatetUser.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = uSUARIO.ID_USUARIO;
                                updatetUser.Parameters.Add("@ID_ROL", SqlDbType.Int).Value = uSUARIO.ID_ROL;
                                updatetUser.Parameters.Add("@ID_SUCURSAL", SqlDbType.Int).Value = uSUARIO.ID_SUCURSAL;
                                updatetUser.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar).Value = uSUARIO.COD_USUARIO;
                                updatetUser.Parameters.Add("@CEDULA", SqlDbType.VarChar).Value = uSUARIO.CEDULA;
                                updatetUser.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = uSUARIO.NOMBRE;
                                updatetUser.Parameters.Add("@APELLIDO", SqlDbType.VarChar).Value = uSUARIO.APELLIDO;
                                updatetUser.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = uSUARIO.USUARIO1;
                                updatetUser.Parameters.Add("@CONTRACENIA", SqlDbType.VarChar).Value = password;
                                updatetUser.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = "RETMINSIS";
                                con.Open();
                                var oUser = updatetUser.ExecuteNonQuery();
                                if (oUser == 1)
                                    return RedirectToAction("Index");
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch( Exception e)
            {
                
            }
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "NOMBRE_SUCURSAL");
            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL", uSUARIO.ID_ROL);
            return View(uSUARIO);
        }

        // GET: usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
