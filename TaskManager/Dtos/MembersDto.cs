using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class MembersDto
    {
        public int MemberId { get; set; }

        [Required]
        public string MemberName { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        public int SecurityLevelId { get; set; }

        [Required]
        public int MemberGroupId { get; set; }        
    }
}