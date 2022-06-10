using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RETMINSISTEM.Models;

namespace RETMINSISTEM.Controllers
{
    public class kardexController : Controller
    {
        private Model db = new Model();

        // GET: kardex
        public ActionResult Index()
        {
            var kARDEX = db.KARDEX.Include(k => k.BODEGA).Include(k => k.PROVEEDOR).Include(k => k.USUARIO);
            return View(kARDEX.ToList());
        }

        // GET: kardex/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KARDEX kARDEX = db.KARDEX.Find(id);
            if (kARDEX == null)
            {
                return HttpNotFound();
            }
            return View(kARDEX);
        }

        // GET: kardex/Create
        public ActionResult Create()
        {
            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA");
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR");
           // ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO");
            return View();
        }

        // POST: kardex/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_KARDEX,ID_PROVEEDOR,ID_BODEGA,ARTICULO,UNIDADES,STOCK_MIN,STOCK_MAX,LOCALIZACION,TIPO_KARDEX")] KARDEX kARDEX, HttpPostedFileBase FOTO_ARTICULO)
        {
            
            int ID_KARDEX_ACTUAL = (db.KARDEX.ToList().Count() + 1);

            if (ModelState.IsValid)
            {
                kARDEX.COD_KARDEX = "K" + ID_KARDEX_ACTUAL.ToString("D9");
                kARDEX.ID_USUARIO = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                kARDEX.FOTO_ARTICULO = new byte[FOTO_ARTICULO.ContentLength];
                FOTO_ARTICULO.InputStream.Read(kARDEX.FOTO_ARTICULO, 0, FOTO_ARTICULO.ContentLength);
                db.KARDEX.Add(kARDEX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA", kARDEX.ID_BODEGA);
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR", kARDEX.ID_PROVEEDOR);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", kARDEX.ID_USUARIO);
            return View(kARDEX);
        }

        // GET: kardex/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KARDEX kARDEX = db.KARDEX.Find(id);
            if (kARDEX == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA", kARDEX.ID_BODEGA);
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR", kARDEX.ID_PROVEEDOR);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", kARDEX.ID_USUARIO);
            return View(kARDEX);
        }

        // POST: kardex/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_KARDEX,ID_PROVEEDOR,ID_BODEGA,ID_USUARIO,COD_KARDEX,ARTICULO,UNIDADES,STOCK_MIN,STOCK_MAX,LOCALIZACION,TIPO_KARDEX")] KARDEX kARDEX, HttpPostedFileBase FOTO_ARTICULO)
        {
            if (ModelState.IsValid)
            {
                kARDEX.FOTO_ARTICULO = new byte[FOTO_ARTICULO.ContentLength];
                FOTO_ARTICULO.InputStream.Read(kARDEX.FOTO_ARTICULO, 0, FOTO_ARTICULO.ContentLength);
                db.Entry(kARDEX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA", kARDEX.ID_BODEGA);
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR", kARDEX.ID_PROVEEDOR);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", kARDEX.ID_USUARIO);
            return View(kARDEX);
        }

        // GET: kardex/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KARDEX kARDEX = db.KARDEX.Find(id);
            if (kARDEX == null)
            {
                return HttpNotFound();
            }
            return View(kARDEX);
        }

        // POST: kardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            KARDEX kARDEX = db.KARDEX.Find(id);
            db.KARDEX.Remove(kARDEX);
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
