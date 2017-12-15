using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class TasksFormViewModel
    {
        public IEnumerable<TaskCategories> TaskCategories { get; set; }

        public IEnumerable<TaskStatuses> TaskStatuses { get; set; }

        public IEnumerable<TaskTypes> TaskTypes { get; set; }

        public IEnumerable<Prices> Prices { get; set; }

        public IEnumerable<Companies> Companies { get; set; }
        
        public int? TaskId { get; set; }

        [StringLength(255)]
        [Required]
        public string TaskName { get; set; }

        [StringLength(1000)]
        public string TaskDescription { get; set; }

        [Required]
        public int Hours { get; set; }

        [Display(Name = "Date Created")]
        [Required]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Started")]
        [Required]
        public DateTime DateWorkStarted { get; set; }

        public TimeSpan TimeWorked { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int TaskCategoryId { get; set; }            

        [Display(Name = "Type")]
        [Required]
        public int TaskTypeId { get; set; }
                
        [Display(Name = "Status")]
        public int TaskStatusId { get; set; }
        
        [Display(Name = "Company")]
        [Required]
        public int CompanyId { get; set; }

        [Display(Name = "Price")]
        [Required]
        public int PriceId { get; set; }

        public TasksFormViewModel()
        {
            TaskId = 0;
        }

        public TasksFormViewModel(Tasks task)
        {
            TaskId = task.TaskId;
            TaskName = task.TaskName;
            TaskDescription = task.TaskDescription;
            TaskTypeId = task.TaskTypeId;
            TaskCategoryId = task.TaskCategoryId;
            PriceId = task.PriceId;
            CompanyId = task.CompanyId;
        }
    }
}