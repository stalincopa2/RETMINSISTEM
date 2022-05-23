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
    public class PROVEEDORsController : Controller
    {
        private RETMINEntities1 db = new RETMINEntities1();

        // GET: PROVEEDORs
        public ActionResult Index()
        {
            return View(db.PROVEEDOR.ToList());
        }

        // GET: PROVEEDORs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            if (pROVEEDOR == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDOR);
        }

        // GET: PROVEEDORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PROVEEDORs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PROVEEDOR,COD_PROOVEDOR,NOMBRE_PROVEEDOR,APELLIDO_PROVEEDOR,EMPRESA_REPRESENTA,CONTACTO,CORREO")] PROVEEDOR pROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                db.PROVEEDOR.Add(pROVEEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROVEEDOR);
        }

        // GET: PROVEEDORs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            if (pROVEEDOR == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDOR);
        }

        // POST: PROVEEDORs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PROVEEDOR,COD_PROOVEDOR,NOMBRE_PROVEEDOR,APELLIDO_PROVEEDOR,EMPRESA_REPRESENTA,CONTACTO,CORREO")] PROVEEDOR pROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROVEEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROVEEDOR);
        }

        // GET: PROVEEDORs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            if (pROVEEDOR == null)
            {
                return HttpNotFound();
            }
            return View(pROVEEDOR);
        }

        // POST: PROVEEDORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            db.PROVEEDOR.Remove(pROVEEDOR);
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
