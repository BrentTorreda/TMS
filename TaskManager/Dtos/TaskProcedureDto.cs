using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Dtos
{
    public class TaskProcedureDto
    {
        public int TaskProcedureId { get; set; }
        
        public int TaskProcedureOrder { get; set; }
        
        public string TaskProcedureDescription { get; set; }
        
        public string TaskSteps { get; set; }
        
        public string TaskVideoFile { get; set; }
        
        public int TaskId { get; set; }
        
        public Tasks Tasks { get; set; }
        
        public int SubtaskId { get; set; }
        
        public SubTasksLevel1 SubTasksLevel1 { get; set; }

        public bool IsStepDone { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }
        
        public string FilePath1 { get; set; }
        
        public string FilePath2 { get; set; }
        
        public string FilePath3 { get; set; }
    }
}