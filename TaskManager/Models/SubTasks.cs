using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class SubTasks
    {
        [Key]
        public int SubTaskId { get; set; }

        [Required]
        public string SubTaskName { get; set; }

        [Required]
        public string SubTaskDescription { get; set; }

        [Required]
        public string TaskStatusId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Members Members { get; set; }
    }
}