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
    public class DESCRIPCION_KARDEXController : Controller
    {
        private RETMINEntities1 db = new RETMINEntities1();

        // GET: DESCRIPCION_KARDEX
        public ActionResult Index()
        {
            var dESCRIPCION_KARDEX = db.DESCRIPCION_KARDEX.Include(d => d.KARDEX).Include(d => d.USUARIO);
            return View(dESCRIPCION_KARDEX.ToList());
        }

        // GET: DESCRIPCION_KARDEX/Details/5
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

        // GET: DESCRIPCION_KARDEX/Create
        public ActionResult Create()
        {
            ViewBag.COD_KARDEX = new SelectList(db.KARDEX, "COD_KARDEX", "ARTICULO");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO");
            return View();
        }

        // POST: DESCRIPCION_KARDEX/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DESCRIPCION_KARDEX,ID_USUARIO,COD_KARDEX,FECHA_KARDEX,DESCRIPCION_KARDEX1,TIPO_TRANSACION,VALOR_UNITARIO,CANTIDAD,VALOR,CANTIDAD_SALDO,VALOR_SALDO,CADUCIDAD")] DESCRIPCION_KARDEX dESCRIPCION_KARDEX)
        {
            if (ModelState.IsValid)
            {
                db.DESCRIPCION_KARDEX.Add(dESCRIPCION_KARDEX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COD_KARDEX = new SelectList(db.KARDEX, "COD_KARDEX", "ARTICULO", dESCRIPCION_KARDEX.COD_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            return View(dESCRIPCION_KARDEX);
        }

        // GET: DESCRIPCION_KARDEX/Edit/5
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
            ViewBag.COD_KARDEX = new SelectList(db.KARDEX, "COD_KARDEX", "ARTICULO", dESCRIPCION_KARDEX.COD_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            return View(dESCRIPCION_KARDEX);
        }

        // POST: DESCRIPCION_KARDEX/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DESCRIPCION_KARDEX,ID_USUARIO,COD_KARDEX,FECHA_KARDEX,DESCRIPCION_KARDEX1,TIPO_TRANSACION,VALOR_UNITARIO,CANTIDAD,VALOR,CANTIDAD_SALDO,VALOR_SALDO,CADUCIDAD")] DESCRIPCION_KARDEX dESCRIPCION_KARDEX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dESCRIPCION_KARDEX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COD_KARDEX = new SelectList(db.KARDEX, "COD_KARDEX", "ARTICULO", dESCRIPCION_KARDEX.COD_KARDEX);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", dESCRIPCION_KARDEX.ID_USUARIO);
            return View(dESCRIPCION_KARDEX);
        }

        // GET: DESCRIPCION_KARDEX/Delete/5
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

        // POST: DESCRIPCION_KARDEX/Delete/5
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
