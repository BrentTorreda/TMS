using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskTemplatesController : TaskManagerBaseController
    {
        // GET: TaskTemplates
        public async Task<ActionResult> Index()
        {
            await AuthorizeUserInIdentity();

            var tasks = new List<Tasks>();

            tasks = _context.Tasks.ToList();

            if(User.IsInRole("CanChangeSettings"))
                return View("Index",tasks);

            return View("ReadOnlyIndex", tasks);
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