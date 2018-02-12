using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Dtos
{
    public class EmailDto
    {
        public int MailId { get; set; }
        
        public string Id { get; set; }
        
        public string Sender { get; set; }
        
        public DateTime DateReceived { get; set; }
        
        public string Subject { get; set; }
        
        public string MailBody { get; set; }
        
        public int NumberOfAttachments { get; set; }

        public bool IsArchived { get; set; }

        public bool IsMadeTask { get; set; }
    }
}