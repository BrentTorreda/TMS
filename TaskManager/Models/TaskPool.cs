using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskPool
    {
        [Key]
        public int TaskPoolId { get; set; }
        
        [Required]
        public int SubTaskLevel1Id { get; set; }

        [ForeignKey("SubTaskLevel1Id")]
        public SubTasksLevel1 SubTaskLevel1 { get; set; }

        [Required]
        public int TaskStatusId { get; set; }

        [ForeignKey("TaskStatusId")]
        public TaskStatuses TaskStatus { get; set; }

        public TimeSpan TimeWorked { get; set; }

        public DateTime WorkStartedOn { get; set; }

        public DateTime WorkFinishedOn { get; set; }

        public DateTime DateDue { get; set; }
    }
}