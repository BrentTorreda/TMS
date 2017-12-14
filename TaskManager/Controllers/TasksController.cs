using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext _context;
        
        public TasksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }

        //
        public ActionResult CreateMember()
        {
            return View();
        }
    }
}