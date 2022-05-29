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
using RETMINSISTEM.Models;

namespace RETMINSISTEM.Controllers
{
    public class UsuarioController : Controller
    {
        private Model db = new Model();

        // GET: Usuario
        public ActionResult Index()
        {
            var uSUARIO = db.USUARIO.Include(u => u.ROL);
            return View(uSUARIO.ToList());
        }

        // GET: Usuario/Details/5
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

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,ID_ROL,COD_USUARIO,CEDULA,NOMBRE,APELLIDO,USUARIO1,CONTRACENIA")] USUARIO uSUARIO)
        {
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
                                insertUser.CommandType = CommandType.StoredProcedure;
                                insertUser.Parameters.Add("@ID_ROL", SqlDbType.Int).Value = uSUARIO.ID_ROL;
                                insertUser.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar).Value =uSUARIO.COD_USUARIO ;
                                insertUser.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = uSUARIO.NOMBRE;
                                insertUser.Parameters.Add("@APELLIDO", SqlDbType.VarChar).Value = uSUARIO.APELLIDO;
                                insertUser.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = uSUARIO.USUARIO1;
                                insertUser.Parameters.Add("@CONTRACENIA", SqlDbType.VarChar).Value = uSUARIO.CONTRACENIA;
                                insertUser.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = "RETMINSIS";                             
                                con.Open();
                                var oUser = insertUser.ExecuteNonQuery();

                               // db.USUARIO.Add(uSUARIO);
                               // db.SaveChanges();
                               if (oUser==1)
                                return RedirectToAction("Index");
                            

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

        // GET: Usuario/Edit/5
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
            return View(uSUARIO);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,ID_ROL,COD_USUARIO,CEDULA,NOMBRE,APELLIDO,USUARIO1,CONTRACENIA")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ROL = new SelectList(db.ROL, "ID_ROL", "NOMBRE_ROL", uSUARIO.ID_ROL);
            return View(uSUARIO);
        }

        // GET: Usuario/Delete/5
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

        // POST: Usuario/Delete/5
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
