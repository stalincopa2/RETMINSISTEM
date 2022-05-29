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
    public class vehiculoController : Controller
    {
        private Model db = new Model();

        // GET: vehiculo
        public ActionResult Index()
        {
            var vEHICULO = db.VEHICULO.Include(v => v.SUCURSAL);
            return View(vEHICULO.ToList());
        }

        // GET: vehiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULO vEHICULO = db.VEHICULO.Find(id);
            if (vEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULO);
        }

        // GET: vehiculo/Create
        public ActionResult Create()
        {
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL");
            return View();
        }

        // POST: vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VEHICULO,ID_SUCURSAL,COD_VEHICULO,MARCA,MODELO,COLOR,FECHA_ADQUISISION,UBIC_ACTUAL")] VEHICULO vEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.VEHICULO.Add(vEHICULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", vEHICULO.ID_SUCURSAL);
            return View(vEHICULO);
        }

        // GET: vehiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULO vEHICULO = db.VEHICULO.Find(id);
            if (vEHICULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", vEHICULO.ID_SUCURSAL);
            return View(vEHICULO);
        }

        // POST: vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VEHICULO,ID_SUCURSAL,COD_VEHICULO,MARCA,MODELO,COLOR,FECHA_ADQUISISION,UBIC_ACTUAL")] VEHICULO vEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vEHICULO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_SUCURSAL = new SelectList(db.SUCURSAL, "ID_SUCURSAL", "RUC_SUCURSAL", vEHICULO.ID_SUCURSAL);
            return View(vEHICULO);
        }

        // GET: vehiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULO vEHICULO = db.VEHICULO.Find(id);
            if (vEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULO);
        }

        // POST: vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VEHICULO vEHICULO = db.VEHICULO.Find(id);
            db.VEHICULO.Remove(vEHICULO);
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
