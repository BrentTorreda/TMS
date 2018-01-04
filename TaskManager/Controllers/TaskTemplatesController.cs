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
        private ApplicationDbContext _context;

        public TaskTemplatesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: TaskTemplates
        public ActionResult Index()
        {
            var tasks = new List<Tasks>();

            tasks = _context.Tasks.ToList();

            return View(tasks);
        }

        // POST: TasksTemplates
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(List<Tasks> tasks)
        {            
            foreach( var task in tasks)
            {
                var taskInDb = _context.Tasks.Single(t => t.TaskId == task.TaskId);
                taskInDb.IsTemplate = task.IsTemplate;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "TaskTemplates");
        }
    }
}