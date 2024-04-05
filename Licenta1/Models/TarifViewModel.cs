using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta1.Models
{
    
    public class TarifViewModel
    {
        public string DenumireServiciu { get; set; }
        public List<Procedura> Procedura { get; set; }
    }
}