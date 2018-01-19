using System;
using TaskManager.Models;

namespace TaskManager.Dtos
{
    public class SubTaskDeferralDetail
    {
        public int DeferId { get; set; }

        public DateTime DeferredOn { get; set; }

        public DateTime DeferredTo { get; set; }

        public TimeSpan DeferredFor { get; set; }

        public int MemberId { get; set; }

        public Members Members { get; set; }

        public string DeferredReason { get; set; }
    }
}