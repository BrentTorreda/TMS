using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Models;
using System;
using System.Collections.Generic;

namespace TaskManager.ViewModels
{
    public class MembersFormViewModel
    {
        public IEnumerable<MemberPositions> MemberPositions { get; set; }

        public IEnumerable<MemberGroups> MemberGroups { get; set; }

        public int? MemberId { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "First name")]
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "Login Name")]
        [Required]
        public string LoginName { get; set; }

        [Display(Name = "Position")]
        [Required]
        public int? MemberPositionId { get; set; }
        
        [Display(Name = "Team")]
        [Required]
        public int? MemberGroupId { get; set; }
        
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public MembersFormViewModel()
        {
            MemberId = 0;
        }

        public MembersFormViewModel(Members member)
        {
            MemberId = member.MemberId;
            LastName = member.LastName;
            FirstName = member.FirstName;            
            MemberPositionId = member.MemberPositionId;
            MemberGroupId = member.MemberGroupId;
            Address = member.Address;
            Phone = member.Phone;
            Email = member.Email;
            LoginName = member.LastName;
        }
    }
}