using System.Linq;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using SQL = TaskManager.Controllers.SQL;
using System;


namespace TaskManager.Controllers
{
    public class SubtaskLevel1Controller : Controller
    {
        private ApplicationDbContext _context;
        private int TaskCompleted = 4; //TO DO: Find a better way for this 

        public SubtaskLevel1Controller()
        {
            _context = new ApplicationDbContext();
        }

        // GET: SubtaskLevel1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(int id)
        {
            SQL.TMSMiscSQL miscSQL = new SQL.TMSMiscSQL();
            var viewModel = new SubtaskLevel1ViewModel()
            {
                Prices = _context.Prices.ToList(),
                Members = _context.Members.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                TaskId = id,
                TaskStatusId = 1, //Not started
                DateCreated = DateTime.Today,
                SubTaskOrder = miscSQL.GetCurrentSubTaskOrder(id)
            };

            return View("SubtaskLevel1FormNew", viewModel);
        }

        // GET: SubtaskLevel1\LogWork\1
        public ActionResult LogWork(int id)
        {
            var subTask = _context.SubTasksLevel1.SingleOrDefault(t => t.SubTaskId == id);

            //get previous task status
            int prevOrderNum = subTask.SubTaskOrder - 1;
            string SQL = "select* from SubTasksLevel1 where SubTaskOrder = " + prevOrderNum  + " and TaskId = " + subTask.TaskId;
            var prevSubTask = _context.SubTasksLevel1.SqlQuery(SQL).SingleOrDefault();
            
            if (subTask == null)
                return HttpNotFound();

            var viewModel = new SubtaskLevel1ViewModel(subTask)
            {
                Prices = _context.Prices.ToList(),
                Members = _context.Members.ToList(),
                Tasks = _context.Tasks.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList()               
            };

            //if there is a subtask, check it's status
            if (prevSubTask != null)
            {
                if (prevSubTask.TaskStatusId != TaskCompleted) 
                    viewModel.PrevTaskDone = false;
                else
                    viewModel.PrevTaskDone = true;
            }
            else
                viewModel.PrevTaskDone = true; //first subtask

            return View("SubTaskLevel1LogWork", viewModel);
        }

        public ActionResult View(int id)
        {
            var subTask = _context.SubTasksLevel1.SingleOrDefault(t => t.SubTaskId == id);

            if (subTask == null)
                return HttpNotFound();

            var viewModel = new SubtaskLevel1ViewModel(subTask)
            {
                Prices = _context.Prices.ToList(),
                Members = _context.Members.ToList(),
                Tasks = _context.Tasks.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList()
            };

            viewModel.ViewOnly_bv = 1;

            return View("SubTaskLevel1FormEdit", viewModel);
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
                Tasks = _context.Tasks.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList()
            };

            return View("SubTaskLevel1FormEdit", viewModel);
        }

        // POST: SubtaskLevel1
        public ActionResult Save(SubTasksLevel1 subTask )
        {
            string action = "Edit";
            string controller = "Tasks";
            int procId = subTask.TaskId;

            if (!ModelState.IsValid)
            {
                //fix 05.03.18 - Model is emptied after saving
                var viewModel = new SubtaskLevel1ViewModel(subTask)
                {
                    Prices = _context.Prices.ToList(),
                    Members = _context.Members.ToList(),
                    Tasks = _context.Tasks.ToList(),
                    TaskStatuses = _context.TaskStatuses.ToList()

                };
                return View("SubTaskLevel1FormNew", viewModel);
            }

            if (subTask.SubTaskId == 0)
            {
                subTask.StartedOn = DateTime.Now; //KLUDGE - Remove
                _context.SubTasksLevel1.Add(subTask);
            }
            else
            {
                var subTaskInDb = _context.SubTasksLevel1.Single(s => s.SubTaskId == subTask.SubTaskId);
                subTaskInDb.SubTaskName = subTask.SubTaskName;
                subTaskInDb.SubTaskDescription = subTask.SubTaskDescription;
                subTaskInDb.SubTaskOrder = subTask.SubTaskOrder;
                subTaskInDb.MemberId = subTask.MemberId;
                subTaskInDb.PriceId = subTask.PriceId;
                subTaskInDb.TaskId = subTask.TaskId;
                subTaskInDb.Notes = subTask.Notes;
                subTaskInDb.IsCompleted = subTask.IsCompleted;
                subTaskInDb.DateCreated = subTask.DateCreated;
                subTaskInDb.StartedOn = DateTime.Now; //KLUDGE - Remove

                if (subTask.IsCompleted)
                {
                    var updateMainTask = new SQL.CheckIfTaskIsDone();
                    updateMainTask.UpdateParentTask(subTask.TaskId); //check if all subtasks are done
                    subTaskInDb.TaskStatusId = TaskCompleted;
                }
                else
                {
                    subTaskInDb.TaskStatusId = 3;
                }

                if (subTask.TimeWorked != null)
                    if (subTask.TimeWorked != subTaskInDb.TimeWorked) //from Work Log
                    {
                        action = "LogWork";
                        controller = "SubtaskLevel1";
                        procId = subTask.SubTaskId;
                    }

                subTaskInDb.TimeWorked = subTask.TimeWorked;                
            }
            _context.SaveChanges();

            return RedirectToAction( action, controller, new { id = procId });
        }
    }
}