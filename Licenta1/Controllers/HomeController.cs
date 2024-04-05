using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Licenta1.Models;

namespace Licenta1.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private StomaEntities db = new StomaEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           
            ViewBag.Message = "Locul unde zâmbetul dumneavoastră prinde viață!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
     
       
       
       
    }
}