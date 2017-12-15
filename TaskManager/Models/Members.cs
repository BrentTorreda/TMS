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

        [Required]      
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Display(Name = "Position")]
        [Required]
        public int MemberPositionId { get; set; }

        [ForeignKey("MemberPositionId")]
        public MemberPosition MemberPosition { get; set; }
        
        [Display(Name = "Team")]
        [Required]
        public int MemberGroupId { get; set; }

        [ForeignKey("MemberGroupId")]
        public MemberGroups MemberGroup { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }
    }
}