using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models;
using System.Web.Mvc;

namespace TaskManager.ViewModels
{
    public class TasksFormViewModel
    {
        [Display(Name ="Category")]
        public IEnumerable<TaskCategories> TaskCategories { get; set; }

        [Display(Name = "Status")]
        public IEnumerable<TaskStatuses> TaskStatuses { get; set; }

        [Display(Name = "Type")]
        public IEnumerable<TaskTypes> TaskTypes { get; set; }

        [Display(Name = "Price")]
        public IEnumerable<Prices> Prices { get; set; }

        [Display(Name = "Company")]
        public IEnumerable<Companies> Companies { get; set; }

        [Display(Name = "Member")]
        public IEnumerable<Members> Members { get; set; }

        public int? TaskId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string TaskName { get; set; }

        [Display(Name = "Detailed Description")]
        [StringLength(1000)]
        public string TaskDescription { get; set; }

        [Required]
        public int Hours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        [Required]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

        [Display(Name = "Assigned To")]
        [Required]
        public int? MemberId { get; set; }

        public string FilterBy { get; set; }

        public int FilterId { get; set; }

        public bool IsTemplate { get; set; }

        public int ViewOnly_bv { get; set; }

        public bool IsCompleted { get; set; }

        public int AncestorTaskId { get; set; }

        public string CreatedByAction { get; set; }
        
        [StringLength(1500)]
        public string Notes { get; set; }

        [AllowHtml]
        [StringLength(5000)]
        public string EmailBody { get; set; }

        [StringLength(1000)]
        public string EmailSubject { get; set; }

        public bool PrevMailStatus { get; set; }

        public string EmailSendee { get; set; }

        public string FileNames { get; set; }

        public int SubTaskCount { get; set; }

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
            DateCreated = task.DateCreated;
            MemberId = task.MemberId;
            Hours = task.Hours;
            TaskStatusId = task.TaskStatusId;
            IsTemplate = task.IsTemplate;
            AncestorTaskId = task.AncestorTaskId;
            CreatedByAction = task.CreatedByAction;
        }
    }
}