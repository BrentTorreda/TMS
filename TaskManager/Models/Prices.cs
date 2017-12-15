using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Prices
    {
        [Key]
        public int PriceId { get; set; }

        [Required]
        public float Amount { get; set; }

        [StringLength(255)]
        [Required]
        public string PriceDescription { get; set; }
    }
}