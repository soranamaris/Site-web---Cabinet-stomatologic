using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Licenta1.Models
{
    public class ReminderEmailModel
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}