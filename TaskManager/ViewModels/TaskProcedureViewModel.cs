using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class TaskProcedureViewModel
    {
        public IEnumerable<Tasks> Tasks { get; set; }

        public int TaskProcedureId { get; set; }

        [Display(Name = "Order")]
        public int TaskProcedureOrder { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string TaskProcedureDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Steps")]
        [StringLength(1000)]
        public string TaskSteps { get; set; }

        [Display(Name = "Video")]
        [StringLength(1000)]
        public string TaskVideoFile { get; set; }
        
        public int TaskId { get; set; }

        public int SubTaskId { get; set; }

        public bool IsStepDone { get; set; }

        [StringLength(1000)]
        public string Image1 { get; set; }

        [StringLength(1000)]
        public string Image2 { get; set; }

        [StringLength(1000)]
        public string Image3 { get; set; }

        public int ViewOnly_bv { get; set; }

        [StringLength(1000)]
        public string FilePath1 { get; set; }

        [StringLength(1000)]
        public string FilePath2 { get; set; }

        [StringLength(1000)]
        public string FilePath3 { get; set; }

        public string Caller { get; set; }

        public TaskProcedureViewModel()
        {
            TaskProcedureId = 0;
        }

        public TaskProcedureViewModel(TaskProcedures taskProc)
        {
            TaskProcedureId = taskProc.TaskProcedureId;
            TaskId = taskProc.TaskId;
            SubTaskId = taskProc.SubtaskId;
            TaskVideoFile = taskProc.TaskVideoFile;
            TaskProcedureDescription = taskProc.TaskProcedureDescription;
            TaskProcedureOrder = taskProc.TaskProcedureOrder;
            TaskSteps = taskProc.TaskSteps;
            FilePath1 = taskProc.FilePath1;
            FilePath2 = taskProc.FilePath2;
            FilePath3 = taskProc.FilePath3;
        }
    }
}