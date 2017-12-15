using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskCategories
    {
        [Key]
        public int TaskCategoryId { get; set; }

        [StringLength(255)]
        [Required]
        public string CategoryName { get; set; }
    }
}