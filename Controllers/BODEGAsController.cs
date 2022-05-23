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
    public class BODEGAsController : Controller
    {
        private RETMINEntities1 db = new RETMINEntities1();

        // GET: BODEGAs
        public ActionResult Index()
        {
            var bODEGA = db.BODEGA.Include(b => b.SUCURSAL);
            return View(bODEGA.ToList());
        }

        // GET: BODEGAs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BODEGA bODEGA = db.BODEGA.Find(id);
            if (bODEGA == null)
            {
                return HttpNotFound();
            }
            return View(bODEGA);
        }

        // GET: BODEGAs/Create
        public ActionResult Create()
        {
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL");
            return View();
        }

        // POST: BODEGAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BODEGA,ID_SUCURSAL,COD_BODEGA,UBICACION,DESCRIPCION_BODEGA")] BODEGA bODEGA)
        {
            if (ModelState.IsValid)
            {
                db.BODEGA.Add(bODEGA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", bODEGA.ID_SUCURSAL);
            return View(bODEGA);
        }

        // GET: BODEGAs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BODEGA bODEGA = db.BODEGA.Find(id);
            if (bODEGA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", bODEGA.ID_SUCURSAL);
            return View(bODEGA);
        }

        // POST: BODEGAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BODEGA,ID_SUCURSAL,COD_BODEGA,UBICACION,DESCRIPCION_BODEGA")] BODEGA bODEGA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bODEGA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", bODEGA.ID_SUCURSAL);
            return View(bODEGA);
        }

        // GET: BODEGAs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BODEGA bODEGA = db.BODEGA.Find(id);
            if (bODEGA == null)
            {
                return HttpNotFound();
            }
            return View(bODEGA);
        }

        // POST: BODEGAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            BODEGA bODEGA = db.BODEGA.Find(id);
            db.BODEGA.Remove(bODEGA);
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
