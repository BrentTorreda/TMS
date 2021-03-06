﻿using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels;
using SQL = TaskManager.Controllers.SQL;

namespace TaskManager.Controllers
{
    public class TaskProceduresController : TaskManagerBaseController
    {
        public async Task<ViewResult> Index()
        {
            await AuthorizeUserInIdentity();

            return View();
        }
        
        public ViewResult New(int taskId, int subTaskId)
        {
            var viewModel = new TaskProcedureViewModel();
            SQL.TMSMiscSQL miscSQL = new SQL.TMSMiscSQL();

            Tasks parentTask = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            SubTasksLevel1 parentSubTask = _context.SubTasksLevel1.FirstOrDefault(s => s.SubTaskId == subTaskId);

            viewModel.TaskId = taskId;
            viewModel.SubTaskId = subTaskId;
            viewModel.TaskName = parentTask.TaskName;
            viewModel.SubTaskName = parentSubTask.SubTaskName;
            viewModel.TaskProcedureOrder = miscSQL.GetCurrentTaskProcOrder(subTaskId) + 1;

            return View("TaskProceduresForm", viewModel);
        }

        [Route("TaskProcedures/Edit/{id}/{caller}/")]
        public ActionResult Edit(int id, string caller)
        {
            var taskProc = _context.TaskProcedures.SingleOrDefault(t => t.TaskProcedureId == id);

            if (taskProc == null)
                return HttpNotFound();

            var viewModel = new TaskProcedureViewModel(taskProc) { };
            viewModel.Caller = caller;

            return View("TaskProceduresForm", viewModel);
        }

        public ActionResult View(int id)
        {
            var taskProc = _context.TaskProcedures.SingleOrDefault(t => t.TaskProcedureId == id);

            if (taskProc == null)
                return HttpNotFound();

            var viewModel = new TaskProcedureViewModel(taskProc) { };            

            return View("TaskProceduresFormView", viewModel);
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
                taskProcInDb.TaskSteps = taskProcedure.TaskSteps;
                taskProcInDb.FilePath1 = taskProcedure.FilePath1;
                taskProcInDb.FilePath2 = taskProcedure.FilePath2;
                taskProcInDb.FilePath3 = taskProcedure.FilePath3;
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "SubtaskLevel1", new { id = taskProcedure.SubtaskId });
        }
    }
}