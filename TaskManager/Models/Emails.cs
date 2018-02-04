using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Emails
    {
        [Key]
        public int MailId { get; set; }

        [StringLength(1000)]
        public string Id { get; set; }

        [StringLength(1500)]
        public string Sender { get; set; }

        [Display(Name ="Date Received")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateReceived { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Attachments")]
        public int NumberOfAttachments { get; set; }

        public bool IsArchived { get; set; }

        public bool IsMadeTask { get; set; }
    }
}