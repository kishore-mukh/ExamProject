using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using evalproject.models;
using System.IO;

namespace evalproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> emailsend(string mailid)
        {

            Random rnd = new Random();
            int num = rnd.Next();
            string pass = "abc" + num.ToString();
            if (Adminlogin.resetpass(mailid, pass) == true)
            {
                string To = mailid;
                string body ="<table >" + "<tr>" + "<td style='font-size: 15px; font-weight: bold;'>" + "Your new password is: " + $"{pass}" + "</td>" + "</tr>" + "<tr>" + "<td style='font-size: 15px; font-weight: bold;'>" + "Please click on the button to reset the password" + "</td>" + "</tr>" + "<tr>" + "<td>" +  "<button style='background-color: #4CAF50; border: none; color: white; padding: 15px 32px; text-align:center; text-decoration:none; display:inline-block; font-size:16px; margin: 4px 2px; cursor: pointer; border-radius:8px;'>"+ "<a style='color:white; text-decoration:none;' href =\"http://localhost:4200/resetpassword \">" + "Click here to update" +"</a>"+ "</button>"  + "</td>" + "</tr>" + "</table>";
                string subject = "Reset Password";
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("kaniniquiz@gmail.com");
                mm.Subject = subject;
                


        
            mm.Body = body;

            mm.To.Add(To);
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("kaniniquiz@gmail.com", "kanini@quiz23");
                smtp.Send(mm);
                return Ok("The link is sent in the mail\nPlease check to reset..");
            }
            else
            {
                return Ok("The following mail id does not exists!!");
            }
        }
        

        [HttpGet]
        [Route("resetpass")]
        public ActionResult<string> updatePass(string mail,string oldpass,string newpass)
        {
            if (Adminlogin.resetpassword(mail, oldpass, newpass) == true)
            {
                return Ok("The password has been updated");
            }
            else
            {
                return Ok("The entered mail or password is incorrect");
            }
            
        }
    }
}
