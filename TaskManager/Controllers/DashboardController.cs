using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var _context = new ApplicationDbContext();

            var viewModel = new DashboardViewModel();

            viewModel.Tasks = _context.Tasks.ToList();
            
            return View("Index", viewModel);
        }
    }
}