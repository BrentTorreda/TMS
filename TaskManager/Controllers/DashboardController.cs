using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNet.Identity;

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
            viewModel.Companies = _context.Companies.ToList();
            viewModel.SubTasksLevel1 = _context.SubTasksLevel1.ToList();

            var userName = User.Identity.GetUserName();

            var member = _context.Members.Single(m => m.Email == userName);

            viewModel.MemberId = member.MemberId;

            return View("Index", viewModel);
        }
    }
}