using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Companies
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(255)]
        [Required]
        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}