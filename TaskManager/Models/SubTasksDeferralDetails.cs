using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class SubTasksDeferralDetails
    {
        [Key]
        public int DeferId { get; set; }

        [Required]
        [Display(Name = "Defer To")]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Members Members { get; set; }

        [Required]
        public int SubTaskId { get; set; }

        [ForeignKey("SubTaskId")]
        public SubTasksLevel1 SubTasksLevel1 { get; set; }

        [Required]
        public DateTime DeferredOn { get; set; }

        [Display(Name = "Defer Until")]
        public DateTime DeferredTo { get; set; }

        public TimeSpan DeferredFor { get; set; }             

        [Display(Name ="Deferred Reason")]
        [StringLength(2000)]
        public string DeferredReason { get; set; }
    }
}