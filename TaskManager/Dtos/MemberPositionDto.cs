using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class MemberPositionDto
    {
        [Key]
        public int MemberPositionId { get; set; }

        public string PositionName { get; set; }
    }
}