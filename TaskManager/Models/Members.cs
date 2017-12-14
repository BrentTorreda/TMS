﻿using System;
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

        [Required]
        public string MemberName { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        public int SecurityLevelId { get; set; }

        [Required]
        public int MemberGroupId { get; set; }

        [ForeignKey("MemberGroupId")]
        public MemberGroups MemberGroup { get; set; }
    }
}