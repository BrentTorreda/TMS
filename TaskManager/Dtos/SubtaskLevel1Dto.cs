using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class SubtaskLevel1Dto
    {      
        public int SubTaskId { get; set; }
        
        public string SubTaskName { get; set; }
        
        public string SubTaskDescription { get; set; } 

        public DateTime DateCreated { get; set; }

        public int Hours { get; set; }

        public int PriceId { get; set; }

        public int TaskId { get; set; }

        public int MemberId { get; set; }

        public PriceDto Prices { get; set; }

        public MembersDto Members { get; set; }

        public TaskDto Tasks { get; set; }

        public TaskStatusDto TaskStatuses { get; set; }

        public int Order { get; set; }

        public TimeSpan TimeWorked { get; set; }

        [StringLength(1500)]
        public string Notes { get; set; }
    }
}