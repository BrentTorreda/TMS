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

        //
        public ViewResult New()
        {
            var tasktypes = _context.TaskTypes.ToList();
            var taskcategories = _context.TaskCategories.ToList();
            var prices = _context.Prices.ToList();
            var companies = _context.Companies.ToList();

            var viewModel = new TasksFormViewModel()
            {
                TaskTypes  = tasktypes,
                TaskCategories = taskcategories,
                Prices = prices,
                Companies = companies
            };

            return View("TaskForm", viewModel);
        }
    }
}