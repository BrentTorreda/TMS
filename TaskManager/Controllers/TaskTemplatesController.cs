using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskTemplatesController : Controller
    {
        // GET: TaskTemplates
        public ActionResult Index()
        {
            return View();
        }
    }
}