using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Licenta1.Models;
using System.Collections;

namespace Licenta1.Controllers
{
    public class ProgController : Controller
    {
        // GET: Prog
        private StomaEntities db = new StomaEntities();
        private IEnumerable<SelectListItem> _allhours = new List<SelectListItem>()
        {
            
            new SelectListItem() { Text = "10:00", Value = "10" },
            new SelectListItem() { Text = "11:00", Value = "11" },
            new SelectListItem() { Text = "12:00", Value = "12" },
            new SelectListItem() { Text = "13:00", Value = "13" },
            new SelectListItem() { Text = "14:00", Value = "14" },
            new SelectListItem() { Text = "15:00", Value = "15" },
            new SelectListItem() { Text = "16:00", Value = "16" },
            new SelectListItem() { Text = "17:00", Value = "17" },
            new SelectListItem() { Text = "18:00", Value = "18" },
            new SelectListItem() { Text = "19:00", Value = "19" },
            new SelectListItem() { Text = "20:00", Value = "20" },
        };

        public ActionResult FilteredValues(DateTime date)
        {


            #pragma warning disable

            var occupiedRows = db.Programares.Where(x => x.Data.Year == date.Year &&
                                                       x.Data.Month == date.Month && 
                                                       x.Data.Day == date.Day);
            var occupiedHours = occupiedRows.Select(x => new SelectListItem() {
                Text = x.Data.Hour.ToString() + ":00",
                Value = x.Data.Hour.ToString()                    
            });

            //IEnumerable<SelectListItem> hours = _allhours.Excepat(occupiedHours);
            var allhours = _allhours.ToList();

            foreach (var item in occupiedHours)
            {
                var toRemove = allhours.First(x => x.Value == item.Value);
                allhours.Remove(toRemove);
            }

            return Json(allhours, JsonRequestBehavior.AllowGet);
            


            //din db a venit
            //    IEnumerable<SelectListItem> occupiedhours = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "14:00", Value = "14" },
            //    new SelectListItem() { Text = "15:00", Value = "15" }
            //};

            //rezultat final -> ALL HOURS - OCCUPIED

            //new SelectListItem() { Text = "16:00", Value = "16" },
            //new SelectListItem() { Text = "17:00", Value = "17" },
        }

        public ActionResult Index()
        {
            //string start = "09:00";
            //string end = "18:00";
            //DateTime stTime = DateTime.ParseExact(start, "HH:mm", null);
            //DateTime endTime = DateTime.ParseExact(end, "HH:mm", null);
            //int interval = 60;
            //List<string> lstInterval = new List<string>();
            //for (DateTime i = stTime; i < endTime; i = i.AddMinutes(interval))
            //    lstInterval.Add(i.ToString("hh:mm"));

            //var prog = new List<ProgViewModel>();
            //var id =  User.Identity.GetUserId();
            //ViewBag.ServiciuProcedura_ServiciuProceduraID = x;
            //prog.Add(new ProgViewModel() { Pacient_Id = id });

            //var sel = new SelectList(lstInterval);
            //ViewBag.Ora = sel;

            var x = new SelectList(db.Proceduras, "Id", "Denumire");
            var prog = new ProgViewModel();
            prog.Pacient_Id = User.Identity.GetUserId();
            prog.ServiciuProcedura = x;
            prog.Data = DateTime.Now;
            prog.Ora = _allhours;

            return View(prog);

            // var items = new SelectList(db.Programares, "ProgramareID", "Data");
            // ViewBag.Ora = items;


            // return View();


        }




        [HttpPost]
        public ActionResult SaveProg([Bind(Include = "Data, Pacient_Id,Ora, SelectedHour,SelectedProcedure,ServiciuProcedura")]ProgViewModel vm, int? hour)
        {
            //var i = 10;

            //ToDO: SAVE
            //un viewbag cu id-ul docotorului
            //pacient id vine din user
            //serviciu-proc  il iau din selected proc 


            //var currentUserId = User.Identity.GetUserId();
            //var doctor = db.AspNetUsers.Where(x => x.AspNetRoles => )

            var newProgramare = new Programare()
            {
                Data = vm.Data.AddHours(Convert.ToDouble(vm.SelectedHour)),
                PacientAnonim = "NU",
                ApplicationUser_Id = User.Identity.GetUserId(),
                ApplicationUser1_Id = "8e3947a8-9182-4347-be1e-9d7dab65ef85",       //doctor
                ServiciuProcedura_ServiciuProceduraID = Convert.ToInt32(vm.SelectedProcedure) 
            };

            db.Programares.Add(newProgramare);
            db.SaveChanges();
            
            base.ViewBag.Success = "Programare cu success!";

            return RedirectToAction("Index");
        }
    }
}