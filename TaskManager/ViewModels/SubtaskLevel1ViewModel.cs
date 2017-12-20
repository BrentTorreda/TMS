using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.ViewModels;
using TaskManager.Models;
using System.Collections;

namespace TaskManager.ViewModels
{
    public class SubtaskLevel1ViewModel
    {
        public IEnumerable<Prices> Prices { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }

        public IEnumerable<Members> Members { get; set; }

        public int SubTaskId { get; set; }
        
        public string SubTaskName { get; set; }
        
        public string SubTaskDescription { get; set; }
        
        public int TaskId { get; set; }    
        
        public int MemberId { get; set; }

        public DateTime DateCreated { get; set; }

        public int Hours { get; set; }
        
        public int PriceId { get; set; }

        public int Order { get; set; }

        public SubtaskLevel1ViewModel()
        {
            SubTaskId = 0;
        }

        public SubtaskLevel1ViewModel(SubTasksLevel1 subTask)
        {
            SubTaskId = subTask.SubTaskId;
            SubTaskName = subTask.SubTaskName;
            SubTaskDescription = subTask.SubTaskDescription;
            TaskId = subTask.TaskId;
            MemberId = subTask.MemberId;
            DateCreated = subTask.DateCreated;
            Hours = subTask.Hours;
            PriceId = subTask.PriceId;
            Order = subTask.Order;
        }
    }
}