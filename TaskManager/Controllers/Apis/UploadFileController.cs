using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.Linq;
using System;

namespace TaskManager.Controllers.Apis
{
    public class UploadFileController : ApiController
    {
        private ApplicationDbContext _context;

        public UploadFileController()
        {
            _context = new ApplicationDbContext();
        }

        // POST
        // Email Attachments - overloaded with dummy string
        [Route("api/UploadFile/{source}")]
        public IHttpActionResult Post(string source)
        {
            var httpRequest = HttpContext.Current.Request;            
            var fileName = "";
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    fileName = postedFile.FileName;
                    if (fileName != "")
                    {
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/Attachments/" + fileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    else
                        break;
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST 
        // Task Procedure Files
        public IHttpActionResult Post()
        {
            var httpRequest = HttpContext.Current.Request;
            var procId = httpRequest.Form["procId"]; //Only 1 proc for all files
            var fileType = httpRequest.Form["fileType"];
            string fileName = "";

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {                    
                    var postedFile = httpRequest.Files[file];
                    fileName = "TP_" + procId + "_" + postedFile.FileName;
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + fileName);
                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);
                }

                //save the filename to DB
                var id = Int32.Parse(procId);
                var taskProcInDb = _context.TaskProcedures.Single(p => p.TaskProcedureId == id);
                if (fileType == "video")
                {
                    taskProcInDb.TaskVideoFile = fileName;
                }
                else
                {
                    if(fileType == "file1")
                        taskProcInDb.FilePath1 = fileName;
                    else if(fileType == "file2")
                        taskProcInDb.FilePath2 = fileName;
                    else if(fileType == "file3")
                        taskProcInDb.FilePath3 = fileName;
                }
                _context.SaveChanges();
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
