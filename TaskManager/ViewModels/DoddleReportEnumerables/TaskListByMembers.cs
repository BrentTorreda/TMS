using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.DoddleReportEnumerables
{
    public class TaskListByMembers
    {
        public string FullName { get; set; }

        public string MainTask { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedByAction { get; set; }

        public string SubTaskName { get; set; }

        public DateTime DateStarted { get; set; }

        public int Hours { get; set; }

        public TimeSpan TimeWorked { get; set; }

        public string CurrentStatus { get; set; }
    }
}