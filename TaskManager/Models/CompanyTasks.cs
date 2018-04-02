using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class CompanyTasks
    {
        [Key]
        public int CompTaskId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Companies { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }
    }
}