using evalproject.models;
using evalproject.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace evalproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEvalser<Login> ln;
        public LoginController(IEvalser<Login> service)
        {
            ln = service;
        }
        [HttpPost]
        public IActionResult loogin(Login p)
        {
            var user = ln.login(p);
            if (p==null)
            {
                
                return Ok("Something went wrong");
            }
            else
            {
                if (user==true)
                {
                    return Ok(new
                    {

                        StatusCode = 200,
                        Message = "Login successfully"
                    }) ;
                }
                else
                {
                    return Ok(new {StatusCode=400,Message="Email or password invalid"});
                }
                
            }
        }

        [HttpPost]
        [Route("admin")]
        public IActionResult adminlogin(Adminlogin p)
        {
            var user = Adminlogin.login(p);
            if (p == null)
            {

                return Ok("Something went wrong");
            }
            else
            {
                if (user == true)
                {
                    return Ok(new
                    {

                        StatusCode = 200,
                        Message = "Login successfully"
                    });
                }
                else
                {
                    return Ok(new { StatusCode = 400, Message = "Email or password invalid" });
                }

            }
        }


        
        [HttpGet]
        [Route("Result")]
        public IActionResult GetAllStudents()
        {
            List<Result> students = Result.fetchResults();
            return Ok(students);
        }

        [HttpGet]
        [Route("studentresult")]
        public IActionResult GetAllStudents(string mail)
        {
            List<Result> student = Result.fetchstudentdata(mail);
            return Ok(student);
        }


    }
}
