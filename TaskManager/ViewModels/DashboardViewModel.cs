using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.Collections;

namespace TaskManager.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Companies> Companies { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }

        public IEnumerable<SubTasksLevel1> SubTasksLevel1 { get; set; }

        public int MemberId { get; set; }

        public IEnumerable<Microsoft.Graph.Message> EmailDetails { get; set; }

        public IEnumerable<Emails> Emails { get; set; }

        public string OpenPanel { get; set; }
    }
}