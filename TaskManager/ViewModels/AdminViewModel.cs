using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels
{
    public class AdminViewModel
    {
        public string UserName { get; set; }

        public string Id { get; set; }

        public string RoleId { get; set; }

        public string Name { get; set; }

        public bool CanAddTasks { get; set; }

        public bool CanReassignTasks { get; set; }

        public bool CanSendEmails { get; set; }

        public bool CanChangeSettings { get; set; }

        public IEnumerable<Members> Members { get; set; }
    }
}