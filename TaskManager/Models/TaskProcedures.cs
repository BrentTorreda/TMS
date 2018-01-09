using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskProcedures
    {
        [Key]
        public int TaskProcedureId { get; set; }

        [Display(Name = "Order")]
        public int TaskProcedureOrder { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string TaskProcedureDescription { get; set; }

        [Display(Name = "Steps")]
        [StringLength(1000)]
        [Required]
        public string TaskSteps { get; set; }

        [Display(Name ="Video")]
        [StringLength(1000)]
        [Required]
        public string TaskVideoFile { get; set; }

        [Display(Name = "Task")]
        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }

        [Display(Name = "Subtask")]
        [Required]
        public int SubtaskId { get; set; }

        public bool IsStepDone { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        [StringLength(1000)]
        public string FilePath1 { get; set; }

        [StringLength(1000)]
        public string FilePath2 { get; set; }

        [StringLength(1000)]
        public string FilePath3 { get; set; }
    }
}