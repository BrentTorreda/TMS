using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskCategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public TaskCategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: TaskCategories
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            //12.29.17 - BTo - Needed. Otherwise View will return a Model.IsValid = false
            var model = new TaskCategories();

            model.TaskCategoryId = 0;

            return View("TaskCategoryForm", model);
        }

        public ActionResult Edit(int id)
        {
            var category = _context.TaskCategories.SingleOrDefault(t => t.TaskCategoryId == id);

            if (category == null)
                return HttpNotFound();

            return View("TaskCategoryForm", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TaskCategories taskCategory)
        {
            if (!ModelState.IsValid)
            {
                return View("TaskCategoryForm");
            }

            if (taskCategory.TaskCategoryId == 0)
            {
                _context.TaskCategories.Add(taskCategory);
            }
            else
            {
                var categoryInDb = _context.TaskCategories.Single(t => t.TaskCategoryId == taskCategory.TaskCategoryId);
                categoryInDb.CategoryName = taskCategory.CategoryName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "TaskCategories");
        }
    }
}