using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Licenta1;
using Licenta1.Models;

namespace Licenta1.Controllers
{
    public class ProgramaresController : Controller
    {
        //private ApplicationDbContext db1 = new ApplicationDbContext();
        private StomaEntities db = new StomaEntities();

        // GET: Programares
        public ActionResult Index()
        {
            var programares = db.Programares.Include(p => p.ServiciuProcedura);
            return View(programares.ToList());
        }

        // GET: Programares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programares.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            return View(programare);
        }

        // GET: Programares/Create
        public ActionResult Create()
        {
            var x1 = new SelectList(db.AspNetUsers, "Id", "UserName");
            
            ViewBag.ServiciuProcedura_ServiciuProceduraID = new SelectList(db.Proceduras, "Id", "Denumire");
            ViewBag.ApplicationUser_Id = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.ApplicationUser1_Id = x1;
            //ViewBag.ApplicationUser1_Id = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Programares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramareID,Data,PacientAnonim,ApplicationUser_Id,ApplicationUser1_Id,ServiciuProcedura_ServiciuProceduraID")] Programare programare)
        {
            if (ModelState.IsValid)
            {
                //programare.ApplicationUser1_Id = ;    DOCTOR?
                db.Programares.Add(programare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiciuProcedura_ServiciuProceduraID = new SelectList(db.Proceduras, "Id", "Denumire", programare.ServiciuProcedura_ServiciuProceduraID);
            ViewBag.ApplicationUser_Id = new SelectList(db.AspNetUsers, "Id", "UserName", programare.ApplicationUser_Id);
            


            //ViewBag.ServiciuProcedura_ServiciuProceduraID = new SelectList(db.ServiciuProceduras, "ServiciuProceduraID", "ServiciuProceduraID", programare.ServiciuProcedura_ServiciuProceduraID);
            //ViewBag.ApplicationUser_Id = new SelectList(db1.Users, "Id", "Username");
            //ViewBag.ApplicationUser1_Id = new SelectList(db1.Users, "Id", "Username");

            return View(programare);
        }

        // GET: Programares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programares.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiciuProcedura_ServiciuProceduraID = new SelectList(db.ServiciuProceduras, "ServiciuProceduraID", "ServiciuProceduraID", programare.ServiciuProcedura_ServiciuProceduraID);
            //ViewBag.ApplicationUser_Id = new SelectList(db1.Users, "Id", "Username", programare.ApplicationUser_Id);
            //ViewBag.ApplicationUser1_Id = new SelectList(db1.Users, "Id", "Username",programare.ApplicationUser1_Id);
            return View(programare);
        }

        // POST: Programares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramareID,Data,PacientAnonim,ApplicationUser_Id,ApplicationUser1_Id,ServiciuProcedura_ServiciuProceduraID")] Programare programare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ServiciuProcedura_ServiciuProceduraID = new SelectList(db.ServiciuProceduras, "ServiciuProceduraID", "ServiciuProceduraID", programare.ServiciuProcedura_ServiciuProceduraID);
            //ViewBag.ApplicationUser_Id = new SelectList(db1.Users, "Id", "Username", programare.ApplicationUser_Id);
            //ViewBag.ApplicationUser1_Id = new SelectList(db1.Users, "Id", "Username", programare.ApplicationUser1_Id);

            return View(programare);
        }

        // GET: Programares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programare programare = db.Programares.Find(id);
            if (programare == null)
            {
                return HttpNotFound();
            }
            return View(programare);
        }

        // POST: Programares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programare programare = db.Programares.Find(id);
            db.Programares.Remove(programare);
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
