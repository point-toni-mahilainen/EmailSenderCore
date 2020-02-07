using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public ActionResult EmailSender([FromBody] Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("mail.shellit.org");

                mail.From = new MailAddress("palaute@koskenseo.fi");
                mail.To.Add("info@koskenseo.fi");
                mail.Subject = email.Subject;
                mail.Body = "Lähettäjä:" + "\r\n" + email.Name + "\r\n" + "\r\n" + 
                            "Lähettäjän sähköposti:" + "\r\n" + email.EmailAddress + "\r\n" + "\r\n" + 
                            "Viesti:" + "\r\n" + email.Message;

                client.Port = 587;
                client.Credentials = new NetworkCredential("palaute@koskenseo.fi", "palauteSeo");
                client.EnableSsl = true;

                client.Send(mail);
                return Ok("Message has sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}