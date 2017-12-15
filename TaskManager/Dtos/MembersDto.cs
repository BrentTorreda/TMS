using System;
using System.ComponentModel.DataAnnotations;
using TaskManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Dtos
{
    public class MembersDto
    {
        public int MemberId { get; set; }
        
        public string LastName { get; set; }
                
        public string FirstName { get; set; }
            
        public int MemberPositionId { get; set; }

        public MemberPosition MemberPosition { get; set; }

        public int MemberGroupId { get; set; }

        public MemberGroups MemberGroup { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}