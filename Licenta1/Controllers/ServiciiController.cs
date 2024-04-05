using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta1.Controllers
{
    public class ServiciiController : Controller
    {
        private StomaEntities db = new StomaEntities();
        // GET: Servicii
        public ActionResult Servicii()
        {

            return View(db.Servicius.ToList());
            //return View();

        }

        public ActionResult Ortodontie()
        {
            return View();
        }
        public ActionResult Implantologie()
        {
            return View();
        }
        public ActionResult Estetica()
        {
            return View();
        }
        public ActionResult Endodontie()
        {
            return View();
        }
        public ActionResult Radiologie()
        {
            return View();
        }
        public ActionResult Parodontologie()
        {
            return View();
        }
        public ActionResult Chirurgie()
        {
            return View();
        }
    }
}