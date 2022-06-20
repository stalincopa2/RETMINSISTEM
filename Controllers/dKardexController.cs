using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RETMINSISTEM.Models;
using RETMINSISTEM.Service;

namespace RETMINSISTEM.Controllers
{
    public class dKardexController : Controller
    {
        private Model db = new Model();
        private DKardexService DkService = new DKardexService();

        // GET: dKardex/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KARDEX KARDEX = db.KARDEX.Find((id));

            if (KARDEX == null)
            {
                return HttpNotFound();
            }
            Session["ID_KARDEX"] = id;
            return View(DkService.DkardexListByIdKardex(id).ToList());
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
            int ID_KARDEX = Convert.ToInt32(Session["ID_KARDEX"].ToString());
            KARDEX KARDEX = db.KARDEX.Find(ID_KARDEX);
            if (KARDEX == null) {
                return HttpNotFound();
            }

            ViewBag.ISEMPY = DkService.kardexListIsEmpty(ID_KARDEX); 
            ViewBag.ID_KARDEX = ID_KARDEX;
            ViewBag.ARTICULO = KARDEX.ARTICULO;
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION");
            return View();
        }

        // POST: dKardex/CreateS
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DESCRIPCION_KARDEX,ID_KARDEX,FECHA_KARDEX,DESCRIPCION_KARDEX1,ID_TRANSACCION,VALOR_UNITARIO,CANTIDAD,VALOR,CANTIDAD_SALDO,CADUCIDAD,CANTIDAD_DISPONIBLE")] DESCRIPCION_KARDEX dESCRIPCION_KARDEX)
        {

            dESCRIPCION_KARDEX = DkService.calcularValoresDKardex(dESCRIPCION_KARDEX); // Metodo que calcula los valores faltantes en el detalle

            if (ModelState.IsValid)
            {
                dESCRIPCION_KARDEX.ID_USUARIO = Convert.ToInt32(Session["ID_USUARIO"].ToString());
                db.DESCRIPCION_KARDEX.Add(dESCRIPCION_KARDEX);
                db.SaveChanges();
                return RedirectToAction("Index", new { id= dESCRIPCION_KARDEX.ID_KARDEX});
            }

            int ID_KARDEX = Convert.ToInt32(Session["ID_KARDEX"].ToString());
            KARDEX KARDEX = db.KARDEX.Find(ID_KARDEX);

            ViewBag.ISEMPY = DkService.kardexListIsEmpty(ID_KARDEX);
            ViewBag.ID_KARDEX = ID_KARDEX;
            ViewBag.ARTICULO = KARDEX.ARTICULO;
            ViewBag.ID_TRANSACCION = new SelectList(db.TRANSACCION, "ID_TRANSACCION", "NOMBRE_TRANSACCION");
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

            int ID_KARDEX = Convert.ToInt32(Session["ID_KARDEX"].ToString());
            KARDEX KARDEX = db.KARDEX.Find(ID_KARDEX);
            ViewBag.ID_KARDEX = ID_KARDEX;
            ViewBag.ARTICULO = KARDEX.ARTICULO;

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

            dESCRIPCION_KARDEX =  DkService.modificarRegistros(dESCRIPCION_KARDEX); // calcula los registros faltantes. 
            
            if (ModelState.IsValid)
            {
                db.Entry(dESCRIPCION_KARDEX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id= dESCRIPCION_KARDEX.ID_KARDEX});
            }
           
            int ID_KARDEX = Convert.ToInt32(Session["ID_KARDEX"].ToString());
            KARDEX KARDEX = db.KARDEX.Find(ID_KARDEX);
            ViewBag.ID_KARDEX = ID_KARDEX;
            ViewBag.ARTICULO = KARDEX.ARTICULO;

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
