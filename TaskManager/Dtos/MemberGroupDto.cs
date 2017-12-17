using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class MemberGroupDto
    {
        [Key]
        public int MemberGroupId { get; set; }

        public string MemberGroupName { get; set; }
    }
}