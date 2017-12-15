using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskTypes
    {
        [Key]
        public int TaskTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string TaskName { get; set; }

        public int AssingmentSecurityLevel { get; set; }
    }
}