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
    public class SUCURSALsController : Controller
    {
        private RETMINEntities1 db = new RETMINEntities1();

        // GET: SUCURSALs
        public ActionResult Index()
        {
            return View(db.SUCURSAL.ToList());
        }

        // GET: SUCURSALs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUCURSAL sUCURSAL = db.SUCURSAL.Find(id);
            if (sUCURSAL == null)
            {
                return HttpNotFound();
            }
            return View(sUCURSAL);
        }

        // GET: SUCURSALs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUCURSALs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SUCURSAL,RUC_SUCURSAL,COD_SUCURSAL,NOMBRE_SUCURSAL,DIRECCION,TELEFONO")] SUCURSAL sUCURSAL)
        {
            if (ModelState.IsValid)
            {
                db.SUCURSAL.Add(sUCURSAL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUCURSAL);
        }

        // GET: SUCURSALs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUCURSAL sUCURSAL = db.SUCURSAL.Find(id);
            if (sUCURSAL == null)
            {
                return HttpNotFound();
            }
            return View(sUCURSAL);
        }

        // POST: SUCURSALs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SUCURSAL,RUC_SUCURSAL,COD_SUCURSAL,NOMBRE_SUCURSAL,DIRECCION,TELEFONO")] SUCURSAL sUCURSAL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUCURSAL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUCURSAL);
        }

        // GET: SUCURSALs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUCURSAL sUCURSAL = db.SUCURSAL.Find(id);
            if (sUCURSAL == null)
            {
                return HttpNotFound();
            }
            return View(sUCURSAL);
        }

        // POST: SUCURSALs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUCURSAL sUCURSAL = db.SUCURSAL.Find(id);
            db.SUCURSAL.Remove(sUCURSAL);
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
