using Licenta1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta1.Controllers
{
    public class TarifeController : Controller
    {
        // GET: Tarife
        public ActionResult Tarife()
        {

            //var _db = new ApplicationDbContext();
            var _db = new StomaEntities();

            //var servicesAndProcedures = db.Servicius
            //                  .Include(s => s.ServiciuProcedura)
            //                  .Include(s => s.Service_Procedure.Select(sp => sp.Procedure))
            //                  .ToList();

            var _tarife = new List<TarifViewModel>();

            foreach (var _serviciu in _db.Servicius.ToList())
            {
                var _procList = new List<Procedura>();

                var t = _db.ServiciuProceduras.Where(x => x.Serviciu_ServiciuID == _serviciu.ServiciuID).Select(y => y.Procedura).ToList();

                _tarife.Add(new TarifViewModel() { DenumireServiciu = _serviciu.Denumire, Procedura = t });
            }

            return View(_tarife.ToList());
        }
    }
}