using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskTypesController : Controller
    {
        private ApplicationDbContext _context;

        public TaskTypesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: TaskTypes
        public ActionResult Index()
        {
            if (User.IsInRole("CanChangeSettings"))
                return View("Index");

            return View("ReadOnlyIndex");
        }

        public ViewResult New()
        {
            //12.29.17 - BTo - Needed. Otherwise View will return a Model.IsValid = false
            var model = new TaskTypes();

            model.TaskTypeId = 0;

            return View("TaskTypeForm", model);
        }

        public ActionResult Edit(int id)
        {
            var category = _context.TaskTypes.SingleOrDefault(t => t.TaskTypeId == id);

            if (category == null)
                return HttpNotFound();

            return View("TaskTypeForm", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TaskTypes taskTypes)
        {
            if (!ModelState.IsValid)
            {
                return View("TaskTypeForm");
            }

            if (taskTypes.TaskTypeId == 0)
            {
                _context.TaskTypes.Add(taskTypes);
            }
            else
            {
                var typeInDb = _context.TaskTypes.Single(t => t.TaskTypeId == taskTypes.TaskTypeId);
                typeInDb.TaskName = taskTypes.TaskName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "TaskTypes");
        }
    }
}