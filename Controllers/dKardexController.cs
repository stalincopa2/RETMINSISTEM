using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RETMINSISTEM.Models;

namespace RETMINSISTEM.Controllers
{
    public class dKardexController : Controller
    {
        private Model db = new Model();

        // GET: dKardex
        public ActionResult Index()
        {
            // var dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Include(d => d.KARDEX).Include(d => d.USUARIO);

            int ID_SUCURSAL = Convert.ToInt32(Session["ID_SUCURSAL"].ToString());

            var dESCRIPCION_KARDEX = from k in db.DESCRIPCION_KARDEX
                         join j in db.USUARIO
                         on k.ID_USUARIO equals j.ID_USUARIO
                         where j.ID_SUCURSAL == ID_SUCURSAL
                         select k;
            return View(dESCRIPCION_KARDEX.ToList());
        }

        // GET: dKardex/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DESCRIPCION_KARDEX dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Find(id);
            if (dESCRIPCION_KARDEX == null)
            {
                return HttpNotFound();
            }
            return View(dESCRIPCION_KARDEX);
        }

        // GET: dKardex/Create
        public ActionResult Create()
        {
            ViewBag.ID_KARDEX = new SelectList(db.KARDEX, "ID_KARDEX", "COD_KARDEX");
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION");
            //ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO");
            return View();
        }

        // POST: dKardex/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DESCRIPCION_KARDEX,ID_KARDEX,FECHA_KARDEX,DESCRIPCION_KARDEX1,ID_TRANSACCION,VALOR_UNITARIO,CANTIDAD,VALOR,CANTIDAD_SALDO,VALOR_SALDO,CADUCIDAD")] DESCRIPCION_KARDEX dESCRIPCION_KARDEX)
        {
            if (ModelState.IsValid)
            {
                dESCRIPCION_KARDEX.ID_USUARIO = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                db.DESCRIPCION_KARDEX.Add(dESCRIPCION_KARDEX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_KARDEX = new SelectList(db.KARDEX, "ID_KARDEX", "COD_KARDEX", dESCRIPCION_KARDEX.ID_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION", dESCRIPCION_KARDEX.ID_TRANSACCION);
            return View(dESCRIPCION_KARDEX);
        }

        // GET: dKardex/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DESCRIPCION_KARDEX dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Find(id);
            if (dESCRIPCION_KARDEX == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KARDEX = new SelectList(db.KARDEX, "ID_KARDEX", "COD_KARDEX", dESCRIPCION_KARDEX.ID_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION", dESCRIPCION_KARDEX.ID_TRANSACCION);
            return View(dESCRIPCION_KARDEX);
        }

        // POST: dKardex/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DESCRIPCION_KARDEX,ID_KARDEX,ID_USUARIO,FECHA_KARDEX,DESCRIPCION_KARDEX1,ID_TRANSACCION,VALOR_UNITARIO,CANTIDAD,VALOR,CANTIDAD_SALDO,VALOR_SALDO,CADUCIDAD")] DESCRIPCION_KARDEX dESCRIPCION_KARDEX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dESCRIPCION_KARDEX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KARDEX = new SelectList(db.KARDEX, "ID_KARDEX", "COD_KARDEX", dESCRIPCION_KARDEX.ID_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION", dESCRIPCION_KARDEX.ID_TRANSACCION);
            return View(dESCRIPCION_KARDEX);
        }

        // GET: dKardex/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DESCRIPCION_KARDEX dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Find(id);
            if (dESCRIPCION_KARDEX == null)
            {
                return HttpNotFound();
            }
            return View(dESCRIPCION_KARDEX);
        }

        // POST: dKardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DESCRIPCION_KARDEX dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Find(id);
            db.DESCRIPCION_KARDEX.Remove(dESCRIPCION_KARDEX);
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
