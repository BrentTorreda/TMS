using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Dtos
{
    public class TaskProcedureDto
    {
        public int TaskProcedureId { get; set; }

        public string TaskSteps { get; set; }

        public int TaskId { get; set; }
    }
}