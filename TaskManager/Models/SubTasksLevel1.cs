using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class SubTasksLevel1
    {
        [Key]
        public int SubTaskId { get; set; }

        [Required]
        public string SubTaskName { get; set; }

        [Required]
        public string SubTaskDescription { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Members Members { get; set; }

        public DateTime DateCreated { get; set; }

        public int Hours { get; set; }

        [Required]
        public int PriceId { get; set; }

        [ForeignKey("PriceId")]
        public Prices Prices { get; set; }

        [Required]
        public int SubTaskOrder { get; set; }

        [Required]
        public int TaskStatusId { get; set; }

        [ForeignKey("TaskStatusId")]
        public TaskStatuses TaskStatuses { get; set; }

        public TimeSpan TimeWorked { get; set; }

        [StringLength(1500)]    
        public string Notes { get; set; }        
    }
}