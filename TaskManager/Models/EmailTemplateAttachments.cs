using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class EmailTemplateAttachments
    {
        [Key]
        public int AttachmentId { get; set; }

        [StringLength(1000)]
        public string FileName { get; set; }

        [Required]
        public int EmailTemplateId { get; set; }

        [ForeignKey("EmailTemplateId")]
        public EmailTemplates EmailTemplates { get; set; }
    }
}