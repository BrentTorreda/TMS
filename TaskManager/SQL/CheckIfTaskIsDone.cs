using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.SQL
{
    public class CheckIfTaskIsDone
    {
        private ApplicationDbContext _context;

        public CheckIfTaskIsDone()
        {
            _context = new ApplicationDbContext();
        }

        public void UpdateParentTask(int taskId)
        {
            var taskInDb = _context.Tasks.SingleOrDefault(t => t.TaskId == taskId);

            if (CheckAllSubtasks(taskId) == 1)
            {                
                taskInDb.TaskStatusId = 4; //done
            }
            else
            {
                taskInDb.TaskStatusId = 3; //ongoing      
            }
            _context.SaveChanges();
        }

        public int CheckAllSubtasks(int taskId)
        {
            var tasksInDb = _context.SubTasksLevel1.Where(t => t.TaskId == taskId).ToList();

            //1 = alldone, 2 = ongoing
            int taskStatus = 1;
            foreach (var subTask in tasksInDb)
            {
                if(subTask.IsCompleted == false)
                {
                    taskStatus = 2;
                }
            }

            return taskStatus;
        }
    }
}