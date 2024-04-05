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
    public class ServiciusController : Controller
    {
        /*private ApplicationDbContext db = new ApplicationDbContext()*/
        private StomaEntities db = new StomaEntities();

        // GET: Servicius
        public ActionResult Index()
        {
            return View(db.Servicius.ToList());
        }

        // GET: Servicius/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serviciu serviciu = db.Servicius.Find(id);
            if (serviciu == null)
            {
                return HttpNotFound();
            }
            return View(serviciu);
        }

        // GET: Servicius/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicius/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiciuID,Denumire,Descriere")] Serviciu serviciu)
        {
            if (ModelState.IsValid)
            {
                db.Servicius.Add(serviciu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviciu);
        }

        // GET: Servicius/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serviciu serviciu = db.Servicius.Find(id);
            if (serviciu == null)
            {
                return HttpNotFound();
            }
            return View(serviciu);
        }

        // POST: Servicius/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiciuID,Denumire,Descriere")] Serviciu serviciu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviciu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviciu);
        }

        // GET: Servicius/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serviciu serviciu = db.Servicius.Find(id);
            if (serviciu == null)
            {
                return HttpNotFound();
            }
            return View(serviciu);
        }

        // POST: Servicius/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Serviciu serviciu = db.Servicius.Find(id);
            db.Servicius.Remove(serviciu);
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
