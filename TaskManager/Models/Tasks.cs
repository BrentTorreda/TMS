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
        
        [Display(Name ="Name")]
        [StringLength(255)]
        [Required]
        public string TaskName { get; set; }

        [Display(Name ="Detailed Description")]
        [StringLength(1000)]
        public string TaskDescription { get; set; }

        [Required]
        public int Hours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Started")]        
        public DateTime? DateWorkStarted { get; set; }
                
        public TimeSpan? TimeWorked { get; set; }

        [Display(Name ="Category")]
        [Required]
        public int TaskCategoryId { get; set; }

        [ForeignKey("TaskCategoryId")]
        public TaskCategories TaskCategory { get; set; }

        [Display(Name ="Type")]
        [Required]
        public int TaskTypeId { get; set; }

        [ForeignKey("TaskTypeId")]
        public TaskTypes TaskType { get; set; }

        [Display(Name = "Status")]
        public int TaskStatusId { get; set; }

        [ForeignKey("TaskStatusId")]
        public TaskStatuses TaskStatus { get; set; }
        
        [Display(Name ="Company")]
        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Company { get; set; }

        [Display(Name ="Price")]
        [Required]
        public int PriceId { get; set; }

        [ForeignKey("PriceId")]
        public Prices Price { get; set; }

        public string Priority { get; set; }

        [DataType(DataType.Date)]        
        [Display(Name = "Date Due")]
        public DateTime? DateDue { get; set; }

        [Display(Name ="Assinged To")]        
        public int? MemberId { get; set; }
        
        [ForeignKey("MemberId")]        
        public Members Members { get; set; }

        public bool IsTemplate { get; set; }

        public int AncestorTaskId { get; set; }
    }
}