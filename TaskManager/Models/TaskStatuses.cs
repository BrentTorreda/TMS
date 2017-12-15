using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskStatuses
    {
        [Key]
        public int TaskStatusId { get; set; }

        [StringLength(255)]
        [Required]
        public string TaskStatusName { get; set; }
    }
}