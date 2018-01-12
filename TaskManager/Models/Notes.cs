using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class Notes
    {
        [Key]
        public int id { get; set; }
     
        public int MadeBy { get; set; }

        [Display(Name ="Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Assigned To")]
        public int AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public Members Members { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [AllowHtml]
        [StringLength(2000)]
        public string Body { get; set; }
    }
}