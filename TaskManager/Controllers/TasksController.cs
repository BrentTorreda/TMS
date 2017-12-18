using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

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
        
        public ViewResult New()
        {
            var viewModel = new TasksFormViewModel()
            {
                TaskTypes = _context.TaskTypes.ToList(),
                TaskCategories = _context.TaskCategories.ToList(),
                Prices = _context.Prices.ToList(),
                Companies = _context.Companies.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                Members = _context.Members.ToList()
            };

            return View("TaskForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var _context = new ApplicationDbContext();

            var task = _context.Tasks.SingleOrDefault(t => t.TaskId == id);

            if (task == null)
                return HttpNotFound();

            var viewModel = new TasksFormViewModel(task)
            {
                TaskTypes = _context.TaskTypes.ToList(),
                TaskCategories = _context.TaskCategories.ToList(),
                Prices = _context.Prices.ToList(),
                Companies = _context.Companies.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                Members = _context.Members.ToList()
            };

            return View("TaskForm", viewModel);
        }

        // POST: Tasks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Tasks task)
        {
            var _context = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                var viewModel = new TasksFormViewModel(task)
                {
                    TaskTypes = _context.TaskTypes.ToList(),
                    TaskCategories = _context.TaskCategories.ToList(),
                    Prices = _context.Prices.ToList(),
                    Companies = _context.Companies.ToList(),
                    TaskStatuses = _context.TaskStatuses.ToList(),
                    Members = _context.Members.ToList()
                };
                return View("TaskForm", viewModel);
            }

            if (task.TaskId == 0)
            {
                _context.Tasks.Add(task);
            }
            else
            {
                var taskInDb = _context.Tasks.Single(t => t.TaskId == task.TaskId);
                taskInDb.TaskDescription = task.TaskDescription;
                taskInDb.Hours = task.Hours;
                taskInDb.DateCreated = task.DateCreated;
                taskInDb.TaskTypeId = task.TaskTypeId;
                taskInDb.TaskCategoryId = task.TaskCategoryId;
                taskInDb.TaskStatusId = task.TaskStatusId;
                taskInDb.CompanyId = task.CompanyId;
                taskInDb.PriceId = task.PriceId;
                taskInDb.MemberId = task.MemberId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }
    }
}