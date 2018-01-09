using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class DownloadFileController : Controller
    {
        // GET: DownloadFile
        public ActionResult Index()
        {
            return View();
        }

        public void Download(string fileName)
        {
            string fullName = "~/UploadedFiles/" + fileName;

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(Server.MapPath( fullName ));
            Response.End();
        }
    }
}