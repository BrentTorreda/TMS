using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Dtos
{
    public class EmailTemplateDto
    {
        public int MailTemplateId { get; set; }
        
        public string Subject { get; set; }
        
        public string Body { get; set; }

        public string MadeBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int NumberOfAttachments { get; set; }
        
        public string FileNames { get; set; }
    }
}