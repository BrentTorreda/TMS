using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        
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

        [Display(Name ="Category")]
        [Required]
        public int TaskCategoryId { get; set; }

        [ForeignKey("TaskCategoryId")]
        public TaskCategories TaskCategories { get; set; }

        [Display(Name ="Type")]
        [Required]
        public int TaskTypeId { get; set; }

        [ForeignKey("TaskTypeId")]
        public TaskTypes TaskTypes { get; set; }

        [Display(Name = "Status")]
        public int TaskStatusId { get; set; }

        [ForeignKey("TaskStatusId")]
        public TaskStatuses TaskStatuses { get; set; }
        
        [Display(Name ="Company")]
        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Companies { get; set; }

        [Display(Name ="Price")]
        [Required]
        public int PriceId { get; set; }

        [ForeignKey("PriceId")]
        public Prices Prices { get; set; }                
    }
}