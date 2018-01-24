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

        //public bool UploadFile(HttpContext httpRequest)
        //{
        //    var httpRequest = HttpContext.Current.Request;
        //    var procId = httpRequest.Form["procId"]; //Only 1 proc for all files
        //    var fileType = httpRequest.Form["fileType"];
        //    string fileName = procId;

        //    if (httpRequest.Files.Count > 0)
        //    {
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            fileName = procId + "_" + postedFile.FileName;
        //            var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + fileName);
        //            postedFile.SaveAs(filePath);

        //            docfiles.Add(filePath);
        //        }
        //    }

        //    return true;
        //}

        public IHttpActionResult Post()
        {
            var httpRequest = HttpContext.Current.Request;
            var procId = httpRequest.Form["procId"]; //Only 1 proc for all files
            var fileType = httpRequest.Form["fileType"];
            string fileName = procId;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {                    
                    var postedFile = httpRequest.Files[file];
                    fileName = procId + "_" + postedFile.FileName;
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
