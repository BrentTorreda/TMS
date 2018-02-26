using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TasksOccurrenceDetails
    {
        [Key]
        public int SubTaskOccurrenceId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Tasks Tasks { get; set; }

        public string pattern { get; set; }

        public int recurEvery { get; set; }

        public string[] daysOfWeek { get; set; }

        public int dayInWeek { get; set; }

        public int dayInMonth { get; set; }

        public int firstBiMonthlyDay { get; set; }

        public int secondBiMonthlyDay { get; set; }

        public int dayInYear { get; set; }
    }
}