using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Members
    {
        [Key]
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string LoginName { get; set; }
    }
}