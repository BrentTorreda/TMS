using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskProceduresController : Controller
    {
        private ApplicationDbContext _context;

        public TaskProceduresController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ViewResult New(int taskId, int subtaskId)
        {
            var viewModel = new TaskProcedureViewModel();

            viewModel.TaskId = taskId;
            viewModel.SubTaskId = subtaskId;

            return View("TaskProceduresForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var taskProc = _context.TaskProcedures.SingleOrDefault(t => t.TaskProcedureId == id);

            if (taskProc == null)
                return HttpNotFound();

            var viewModel = new TaskProcedureViewModel(taskProc) { };

            return View("TaskProceduresForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TaskProcedures taskProcedure)
        {   
            if (!ModelState.IsValid)
            {
                var viewModel = new TaskProcedureViewModel(taskProcedure) { };

                return View("TaskProceduresForm", viewModel);
            }

            if (taskProcedure.TaskProcedureId == 0)
            {
                _context.TaskProcedures.Add(taskProcedure);
            }
            else
            {
                var taskProcInDb = _context.TaskProcedures.Single(t => t.TaskProcedureId == taskProcedure.TaskProcedureId);
                taskProcInDb.TaskProcedureOrder = taskProcedure.TaskProcedureOrder;
                taskProcInDb.TaskProcedureDescription = taskProcedure.TaskProcedureDescription;
                taskProcInDb.TaskVideoFile = taskProcedure.TaskVideoFile;
                taskProcInDb.TaskId = taskProcedure.TaskId;
                taskProcInDb.SubtaskId = taskProcedure.SubtaskId;
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "SubtaskLevel1", new { taskId = taskProcedure.TaskId, subtaskId = taskProcedure.SubtaskId });
        }
    }
}