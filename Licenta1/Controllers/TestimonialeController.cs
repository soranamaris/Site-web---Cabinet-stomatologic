using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta1.Controllers
{
    public class TestimonialeController : Controller
    {
        // GET: Testimoniale
        public ActionResult Testimoniale()
        {
            ViewBag.Message = "Aici vor fi introduse testimonialele";
            return View();
        }
    }
}