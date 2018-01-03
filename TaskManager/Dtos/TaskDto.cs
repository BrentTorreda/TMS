using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        
        public string TaskName { get; set; }
        
        public string TaskDescription { get; set; }
        
        public int Hours { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateWorkStarted { get; set; }

        public TimeSpan TimeWorked { get; set; }
        
        public int TaskCategoryId { get; set; }
        
        public TaskCategoryDto TaskCategory { get; set; }
        
        public int TaskTypeId { get; set; }
        
        public TaskTypeDto TaskType { get; set; }
        
        public int TaskStatusId { get; set; }
        
        public TaskStatusDto TaskStatus { get; set; }
        
        public int CompanyId { get; set; }
        
        public CompanyDto Company { get; set; }
        
        public int PriceId { get; set; }
        
        public PriceDto Price { get; set; }

        public DateTime DateDue { get; set; }

        public bool IsTemplate { get; set; }
    }
}