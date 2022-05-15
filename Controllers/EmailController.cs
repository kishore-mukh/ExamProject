﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using evalproject.models;


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
            if(Adminlogin.resetpass(mailid, pass) == true)
            {
                string To = mailid;
                string body = $"Your new password is: {pass}\nPlease paste the link on a new tab\nhttp://localhost:4200/resetpassword";
                string subject = "Reset Password";
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("kaniniquiz@gmail.com");
                mm.Subject = subject;
                mm.Body = body;
                mm.To.Add(To);
                mm.IsBodyHtml = false;

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