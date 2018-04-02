using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class CompanyTasksDto
    {
        public int CompTaskId { get; set; }
        
        public int CompanyId { get; set; }

        public int TaskId { get; set; }

        public Tasks Tasks { get; set; }

        public Companies Companies { get; set; }
    }
}