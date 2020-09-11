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
        [Route("")]
        public ActionResult EmailSender([FromBody] Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("outgoing mail server");

                mail.From = new MailAddress("<sending email>");
                mail.To.Add(email.RecipientEmail);
                mail.Subject = email.Subject;
                mail.Body = "This message has sent from the WebPortfolio's contact service." + "\r\n" + 
                            "If you want to answer to the sender, please send your answer to the address shown in ''Sender's email'' -section below." + "\r\n" + "\r\n" +
                            "Sender:" + "\r\n" + email.Name + "\r\n" + "\r\n" + 
                            "Sender's email:" + "\r\n" + email.SenderEmail + "\r\n" + "\r\n" + 
                            "Message:" + "\r\n" + email.Message;

                client.Port = 587;
                client.Credentials = new NetworkCredential("<sending email>", "<password>");
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