using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class EmailTemplates
    {
        [Key]
        public int MailTemplateId { get; set; }

        [StringLength(1000)]
        public string Subject { get; set; }

        [AllowHtml]
        [StringLength(2000)]
        public string Body { get; set; }

        public string MadeBy { get; set; }

        public DateTime DateCreated { get; set; }
    }
}