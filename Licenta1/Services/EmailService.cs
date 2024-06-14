using System.Net;
using System.Net.Mail;
using Licenta1.Models;

namespace Licenta1.Services
{
    public class EmailService
    {
        public void SendEmail(ReminderEmailModel emailModel)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("stomadent.office@gmail.com", "uuojodjjezddebuo"),
                EnableSsl = true,
            };

            mail.To.Add(emailModel.ToEmail);
            mail.Subject = emailModel.Subject;
            mail.Body = emailModel.Body;
            mail.IsBodyHtml = false;

            smtpClient.Send(mail);
        }
    }
}