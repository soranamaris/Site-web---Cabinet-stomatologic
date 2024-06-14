using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Licenta1.Models
{
    public class ProgViewModel
    {
        [DataType(DataType.Date)]
        public System.DateTime Data { get; set; }

        public string Pacient_Id { get; set; }

        public IEnumerable<SelectListItem> ServiciuProcedura { get; set; }
        public IEnumerable<SelectListItem> Manager { get; set; }

        public IEnumerable<SelectListItem> Ora { get; set; }

        public string SelectedProcedure { get; set; }
        public IEnumerable<SelectListItem> SelectedManager { get; set; }

        public string SelectedHour { get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; }

        public string Doctor_Id { get; set; }

    }
}