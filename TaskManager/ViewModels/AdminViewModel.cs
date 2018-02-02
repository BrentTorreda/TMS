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
        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Members Members { get; set; }
    }
}