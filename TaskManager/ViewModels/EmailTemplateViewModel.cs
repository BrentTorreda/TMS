using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.ViewModels
{
    public class EmailTemplateViewModel
    {
        public int MailTemplateId { get; set; }
        
        public string Subject { get; set; }
        
        public string Body { get; set; }

        public string MadeBy { get; set; }

        public DateTime DateCreated { get; set; }

        public EmailTemplateAttachments EmailTemplateAttachments { get; set; }
    }
}