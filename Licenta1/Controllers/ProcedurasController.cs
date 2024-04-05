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
    public class ProcedurasController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private StomaEntities db = new StomaEntities();

        // GET: Proceduras
        public ActionResult Index()
        {
            return View(db.Proceduras.ToList());
        }

        // GET: Proceduras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedura procedura = db.Proceduras.Find(id);
            if (procedura == null)
            {
                return HttpNotFound();
            }
            return View(procedura);
        }

        // GET: Proceduras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proceduras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Denumire,Pret,Descriere,Durata")] Procedura procedura)
        {
            if (ModelState.IsValid)
            {
                db.Proceduras.Add(procedura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(procedura);
        }

        // GET: Proceduras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedura procedura = db.Proceduras.Find(id);
            if (procedura == null)
            {
                return HttpNotFound();
            }
            return View(procedura);
        }

        // POST: Proceduras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Denumire,Pret,Descriere,Durata")] Procedura procedura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedura);
        }

        // GET: Proceduras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedura procedura = db.Proceduras.Find(id);
            if (procedura == null)
            {
                return HttpNotFound();
            }
            return View(procedura);
        }

        // POST: Proceduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Procedura procedura = db.Proceduras.Find(id);
            db.Proceduras.Remove(procedura);
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
