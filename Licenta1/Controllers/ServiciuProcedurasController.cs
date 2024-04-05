using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Licenta1.Models;

namespace Licenta1.Controllers
{
    public class ServiciuProcedurasController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private StomaEntities db = new StomaEntities();

        // GET: ServiciuProceduras
        public ActionResult Index()
        {
            var list = db.ServiciuProceduras.ToList();

            return View(list);
            //return View(db.ServiciuProceduras.ToList());
        }

        // GET: ServiciuProceduras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciuProcedura serviciuProcedura = db.ServiciuProceduras.Find(id);
            if (serviciuProcedura == null)
            {
                return HttpNotFound();
            }
            return View(serviciuProcedura);
        }

        // GET: ServiciuProceduras/Create
        public ActionResult Create()
        {
            ViewBag.Procedura_Id = new SelectList(db.Proceduras, "Id", "Denumire");
            ViewBag.Serviciu_ServiciuID = new SelectList(db.Servicius, "ServiciuID", "Denumire");
            return View();
        }

        // POST: ServiciuProceduras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiciuProceduraID,Serviciu_ServiciuID,Procedura_Id")] ServiciuProcedura serviciuProcedura)
        {
            if (ModelState.IsValid)
            {
                db.ServiciuProceduras.Add(serviciuProcedura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Procedura_Id = new SelectList(db.Proceduras, "Id", "Denumire");
            ViewBag.Serviciu_ServiciuID = new SelectList(db.Servicius, "ServiciuID", "Denumire");
            return View(serviciuProcedura);
        }

        // GET: ServiciuProceduras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciuProcedura serviciuProcedura = db.ServiciuProceduras.Find(id);
            if (serviciuProcedura == null)
            {
                return HttpNotFound();
            }

            ViewBag.Procedura_Id = new SelectList(db.Proceduras, "Id", "Denumire", serviciuProcedura.Procedura_Id);
            ViewBag.Serviciu_ServiciuID = new SelectList(db.Servicius, "ServiciuID", "Denumire", serviciuProcedura.Serviciu_ServiciuID);
            return View(serviciuProcedura);
        }

        // POST: ServiciuProceduras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiciuProceduraID,Serviciu_ServiciuID,Procedura_Id")] ServiciuProcedura serviciuProcedura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviciuProcedura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Procedura_Id = new SelectList(db.Proceduras, "Id", "Denumire", serviciuProcedura.Procedura_Id);
            ViewBag.Serviciu_ServiciuID = new SelectList(db.Servicius, "ServiciuID", "Denumire", serviciuProcedura.Serviciu_ServiciuID);
            return View(serviciuProcedura);
        }

        // GET: ServiciuProceduras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiciuProcedura serviciuProcedura = db.ServiciuProceduras.Find(id);
            if (serviciuProcedura == null)
            {
                return HttpNotFound();
            }
            return View(serviciuProcedura);
        }

        // POST: ServiciuProceduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiciuProcedura serviciuProcedura = db.ServiciuProceduras.Find(id);
            db.ServiciuProceduras.Remove(serviciuProcedura);
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
