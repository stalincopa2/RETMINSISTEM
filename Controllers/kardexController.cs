﻿using System;
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
            //var kARDEX = db.KARDEX.Include(k => k.BODEGA).Include(k => k.PROVEEDOR).Include(k => k.USUARIO);

                               
            int ID_SUCURSAL = Convert.ToInt32( Session["ID_SUCURSAL"].ToString());

            var kARDEX = from k in db.KARDEX
                         join j in db.USUARIO
                         on k.ID_USUARIO equals j.ID_USUARIO
                         where j.ID_SUCURSAL == ID_SUCURSAL
                         select k;
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
            List<SelectListItem> tipoKardex = new List<SelectListItem>();
            var tKardex = from tk in db.TIPO_KARDEX
                          where tk.NOMBRE_TIPO != "Inactivo"
                          select tk;

            foreach (var item in tKardex) {
                tipoKardex.Add(new SelectListItem { Text = item.NOMBRE_TIPO, Value = item.ID_TIPO_KARDEX.ToString() });
            }
          

            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA");
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR");

            ViewBag.ID_TIPO_KARDEX = tipoKardex;
            // ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO");
            return View();
        }

        // POST: kardex/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_KARDEX,ID_PROVEEDOR,ID_BODEGA,ARTICULO,UNIDADES,STOCK_MIN,STOCK_MAX,LOCALIZACION,ID_TIPO_KARDEX")] KARDEX kARDEX, HttpPostedFileBase FOTO_ARTICULO)
        {
            
            int ID_KARDEX_ACTUAL = (db.KARDEX.ToList().Count() + 1);

            if (FOTO_ARTICULO!= null)
            {
                kARDEX.FOTO_ARTICULO = new byte[FOTO_ARTICULO.ContentLength];
                FOTO_ARTICULO.InputStream.Read(kARDEX.FOTO_ARTICULO, 0, FOTO_ARTICULO.ContentLength);
            }

            if (ModelState.IsValid)
            {
                kARDEX.COD_KARDEX = "K" + ID_KARDEX_ACTUAL.ToString("D9");
                kARDEX.ID_USUARIO = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                db.KARDEX.Add(kARDEX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> tipoKardex = new List<SelectListItem>();
            var tKardex = from tk in db.TIPO_KARDEX
                          where tk.NOMBRE_TIPO != "Inactivo"
                          select tk;
            foreach (var item in tKardex)
            {
                tipoKardex.Add(new SelectListItem { Text = item.NOMBRE_TIPO, Value = item.ID_TIPO_KARDEX.ToString() });
            }

            ViewBag.ID_BODEGA = new SelectList(db.BODEGA, "ID_BODEGA", "COD_BODEGA", kARDEX.ID_BODEGA);
            ViewBag.ID_PROVEEDOR = new SelectList(db.PROVEEDOR, "ID_PROVEEDOR", "COD_PROOVEDOR", kARDEX.ID_PROVEEDOR);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIO, "ID_USUARIO", "COD_USUARIO", kARDEX.ID_USUARIO);
            ViewBag.ID_TIPO_KARDEX = tipoKardex;
            return View(kARDEX);
        }

        // GET: kardex/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_TIPO_KARDEX = new SelectList(db.TIPO_KARDEX, "ID_TIPO_KARDEX", "NOMBRE_TIPO", kARDEX.ID_TIPO_KARDEX);
            return View(kARDEX);
        }

        // POST: kardex/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_KARDEX,ID_PROVEEDOR,ID_BODEGA,ID_USUARIO,COD_KARDEX,ARTICULO,UNIDADES,STOCK_MIN,STOCK_MAX,LOCALIZACION,ID_TIPO_KARDEX,FOTO_ARTICULO")] KARDEX kARDEX, HttpPostedFileBase FOTO_ARTICULO_MODIFIED)
        {
            
             
            if (FOTO_ARTICULO_MODIFIED != null)
            {
                kARDEX.FOTO_ARTICULO = new byte[FOTO_ARTICULO_MODIFIED.ContentLength];
                FOTO_ARTICULO_MODIFIED.InputStream.Read(kARDEX.FOTO_ARTICULO, 0, FOTO_ARTICULO_MODIFIED.ContentLength);
            }

            if (ModelState.IsValid)
            {
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

