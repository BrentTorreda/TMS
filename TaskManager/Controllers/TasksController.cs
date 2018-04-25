using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager.Controllers.SQL;

namespace TaskManager.Controllers
{
    public class TasksController : TaskManagerBaseController
    {
        // GET: Tasks 
        public async Task<ActionResult> Index(int id)
        {
            await AuthorizeUserInIdentity();

            var viewModel = new TasksFormViewModel();

            if (id == 0)
                viewModel.FilterBy = "Task Index";
            else
            {
                var company = new Companies();

                company = _context.Companies.SingleOrDefault(c => c.CompanyId == id);

                viewModel.FilterBy = company.CompanyName;
                viewModel.FilterId = id;
            }            

            if (User.IsInRole("CanAddTasks"))
                return View("Index", viewModel);
            else
                return View("ReadOnlyIndex", viewModel);
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

            return View("TaskFormNew", viewModel);
        }

        public ActionResult NewFromTemplate(int id)
        {
            var sqlDirect = new PrepareTemplate();

            var taskId = sqlDirect.InsertTemplate(id);

            var task = _context.Tasks.SingleOrDefault(t => t.TaskId == taskId);

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

            return View("TaskFormNewFromTemplate", viewModel);
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

            if (User.IsInRole("CanAddTasks"))
                return View("TaskFormEdit", viewModel);
            else
                return View("ReadOnlyTaskForm", viewModel);
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
                return View("TaskFormNew", viewModel);
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

            return RedirectToAction("Index", "Tasks", new { id = 0 });
        }
    }
}