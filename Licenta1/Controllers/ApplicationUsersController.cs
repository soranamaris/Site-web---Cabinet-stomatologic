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
    public class ApplicationUsersController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private StomaEntities db = new StomaEntities();

        // GET: Users
        public ActionResult Index()
        {
            List<AspNetUser> aspNetUsers = db.AspNetUsers.ToList();

            // Convertirea listei de AspNetUser într-o listă de ApplicationUser folosind Select
            IEnumerable<ApplicationUser> applicationUsers = aspNetUsers.Select(user => new ApplicationUser
            {
                Id = user.Id,
                PhoneNumber=user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName
                
            }).ToList();
            return View(applicationUsers);
            
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = new ApplicationUser
            {
                Id = aspNetUser.Id,
                PhoneNumber = aspNetUser.PhoneNumber,
                Email = aspNetUser.Email,
                UserName = aspNetUser.UserName
                
            };
            return View(user);
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
        public ActionResult Create([Bind(Include = "Id,Email,PhoneNumber,UserName")] AspNetUser user)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Proceduras/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = new ApplicationUser
            {
                Id = aspNetUser.Id,
                PhoneNumber = aspNetUser.PhoneNumber,
                Email = aspNetUser.Email,
                UserName = aspNetUser.UserName
               
            };
            return View(user);
        }

        // POST: Proceduras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,UserName")] AspNetUser user)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.AspNetUsers.Find(user.Id);
                if (userInDb == null)
                {
                    return HttpNotFound();
                }

                // Actualizează doar proprietățile specificate
                userInDb.Email = user.Email;
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.UserName = user.UserName;

                // db.Entry(userInDb).State = EntityState.Modified; // Nu mai este necesar, deoarece am actualizat proprietățile manual

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Proceduras/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = new ApplicationUser
            {
                Id = aspNetUser.Id,
                PhoneNumber = aspNetUser.PhoneNumber,
                Email = aspNetUser.Email,
                UserName = aspNetUser.UserName
                
            };
            return View(user);
        }

        // POST: Proceduras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(user);
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
