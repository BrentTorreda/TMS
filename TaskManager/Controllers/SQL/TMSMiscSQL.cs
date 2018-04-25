using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers.SQL
{
    public class TMSMiscSQL
    {
        private ApplicationDbContext _context;

        public TMSMiscSQL()
        {
            _context = new ApplicationDbContext();
        }

        public int GetCurrentTaskProcOrder(int subTaskId)
        {
            List<TaskProcedures> taskProc = new List<TaskProcedures>();
            taskProc = _context.TaskProcedures.Where(s => s.SubtaskId == subTaskId).ToList();
            return taskProc.Max(s => s.TaskProcedureOrder);
        }

        public int GetCurrentSubTaskOrder(int taskId)
        {
            List<SubTasksLevel1> subTasks = new List<SubTasksLevel1>();
            subTasks = _context.SubTasksLevel1.Where(s => s.TaskId == taskId).ToList();
            return subTasks.Max(s => s.SubTaskOrder);
        }
    }
}