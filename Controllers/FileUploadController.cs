using evalproject.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace evalproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Uploading()
        {
            try
            {

                var file = Request.Form.Files[0];

                var justName = file.FileName.Split('+')[2];
                var ext = justName.Split('.')[1];
                //var extension= file.FileName.Split('.')[1];
                string subject = file.FileName.Split('+')[0];
                string ID = file.FileName.Split('+')[1];


                var folderName = Path.Combine("Upload", "files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.Ticks + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Split('+')[2].Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Questions.GetDetails(file,subject,ID);
                    //return Ok(new { fileName });
                    return Ok(new{
                        StatusCode = 200,
                        Message = "File Uploaded Successfully!!"
                    });

                }
                else
                {
                    return Ok(new { StatusCode = 400, Message = "Something went wrong!!\nCheck everything and try again"});
                }
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = 400, Message = ex });
            }
        }
    }
}
