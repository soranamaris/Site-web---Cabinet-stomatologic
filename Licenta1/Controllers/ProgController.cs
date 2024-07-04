using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Licenta1.Models;
using System.Collections;
using Licenta1.Services;
namespace Licenta1.Controllers
{
    public class ProgController : Controller
    {
        private readonly Services.EmailService emailService = new Services.EmailService();
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

        public ActionResult FilteredValues(DateTime date , string applicationUser1_Id)
        {
#pragma warning disable

            // Obținerea programărilor pentru data și doctorul specificat
            var occupiedRows = db.Programares.Where(x => x.Data.Year == date.Year &&
                                                         x.Data.Month == date.Month &&
                                                         x.Data.Day == date.Day &&
                                                         x.ApplicationUser1_Id== applicationUser1_Id
                                                    ).ToList();

            // Selectarea orelor ocupate
            var occupiedHours = occupiedRows.Select(x => new SelectListItem()
            {
                Text = x.Data.Hour.ToString("00") + ":00",
                Value = x.Data.Hour.ToString()
            }).ToList();

            // Presupunem că _allhours este o listă de SelectListItem cu toate orele posibile într-o zi
            var allhours = _allhours.ToList();

            // Eliminarea orelor ocupate din lista tuturor orelor disponibile
            foreach (var item in occupiedHours)
            {
                var toRemove = allhours.FirstOrDefault(x => x.Value == item.Value);
                if (toRemove != null)
                {
                    allhours.Remove(toRemove);
                }
            }

            // Returnarea orelor disponibile sub formă de JSON
            return Json(allhours, JsonRequestBehavior.AllowGet);
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
            // Obține doar utilizatorii care sunt doctori
            var doctorRoleId = db.AspNetRoles.Where(r => r.Name == "Manager").Select(r => r.Id).FirstOrDefault();
            var doctors = db.AspNetUsers.Where(u => u.AspNetRoles.Any(r => r.Id == doctorRoleId)).ToList();

            prog.Doctors = new SelectList(doctors, "Id", "UserName");
            ViewBag.Success = TempData["Success"] as string;

            return View(prog);

            // var items = new SelectList(db.Programares, "ProgramareID", "Data");
            // ViewBag.Ora = items;


            // return View();


        }




        [HttpPost]
        public ActionResult SaveProg([Bind(Include = "Data, Pacient_Id,Ora, SelectedHour,SelectedProcedure,ServiciuProcedura, Doctor_Id")]ProgViewModel vm, int? hour)
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
                ApplicationUser1_Id = vm.Doctor_Id,       //doctor
                ServiciuProcedura_ServiciuProceduraID = Convert.ToInt32(vm.SelectedProcedure) 
            };

            db.Programares.Add(newProgramare);
            db.SaveChanges();

            var userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var user = db.AspNetUsers.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    var doctor = db.AspNetUsers.FirstOrDefault(u => u.Id == vm.Doctor_Id);

                    ReminderEmailModel reminderEmailModel = new ReminderEmailModel
                    {
                        ToEmail = user.Email,
                        Subject = "Confirmare programare",
                        Body = $"Bună ziua!\n\nVă mulțumim că ați ales clinica Stoma Dent.\n\nDoamna doctor {doctor.UserName} vă așteaptă în data de {newProgramare.Data.ToString("dd/MM/yyyy")} pentru programare.\n\nO zi frumoasă,\n\nEchipa Stoma Dent"
                    };
                    emailService.SendEmail(reminderEmailModel);
                }
            }

            //base.ViewBag.Success = "Programare cu success!";
            TempData["Success"] = "Programare cu succes!";

            return RedirectToAction("Index");
        }
    }
}