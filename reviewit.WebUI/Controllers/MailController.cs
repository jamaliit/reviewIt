using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace reviewIt.WebUI.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public void sendMail(string receiver, string subject, string mailBody)

        {

                MailMessage mail = new MailMessage();
                mail.To.Add(receiver);
                mail.From = new MailAddress("mail@gmail.com");
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                mail.IsBodyHtml = true;
                client.Credentials = new NetworkCredential("jamaliit25@gmail.com", "776052856");
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                mail.Subject = subject;
                mail.Body = mailBody;
                client.Send(mail);
        }

    }
}