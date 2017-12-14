using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class MemberGroups
    {
        [Key]
        public int MemberGroupId { get; set; }

        [Required]
        public string MemberGroupName { get; set; }
    }
}