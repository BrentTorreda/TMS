﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        
        [Required]
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public DateTime DateStarted { get; set; }


    }
}