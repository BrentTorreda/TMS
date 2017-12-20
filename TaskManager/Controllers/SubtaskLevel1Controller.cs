using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class SubtaskLevel1Controller : Controller
    {
        private ApplicationDbContext _context;

        public SubtaskLevel1Controller()
        {
            _context = new ApplicationDbContext();
        }

        // GET: SubtaskLevel1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var subTask = _context.SubTasksLevel1.SingleOrDefault(t => t.SubTaskId == id);

            if (subTask == null)
                return HttpNotFound();

            var viewModel = new SubtaskLevel1ViewModel(subTask)
            {
                Prices = _context.Prices.ToList(),
                Members = _context.Members.ToList(),
                Tasks = _context.Tasks.ToList()
            };

            return View("SubTaskLevel1Form", viewModel);
        }

        public ActionResult Save(SubTasksLevel1 subTask )
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SubtaskLevel1ViewModel(subTask) { };

                return View("SubTaskLevel1Form", viewModel);
            }

            if (subTask.SubTaskId == 0)
            {
                _context.SubTasksLevel1.Add(subTask);
            }
            else
            {
                var subTaskInDb = _context.SubTasksLevel1.Single(s => s.SubTaskId == subTask.SubTaskId);
                subTaskInDb.SubTaskName = subTask.SubTaskName;
                subTaskInDb.SubTaskDescription = subTask.SubTaskDescription;
                subTaskInDb.Order = subTask.Order;
                subTaskInDb.MemberId = subTask.MemberId;
                subTaskInDb.PriceId = subTask.PriceId;
                subTaskInDb.TaskId = subTask.TaskId;
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "Tasks", new { id = subTask.TaskId });
        }
    }
}