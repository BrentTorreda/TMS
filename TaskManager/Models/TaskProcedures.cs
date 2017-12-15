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
    }
}