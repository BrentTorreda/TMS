using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using System.Web.Mvc;

namespace TaskManager.ViewModels
{
    public class NoteFormViewModel
    {
        public IEnumerable<Members> Members { get; set; }

        public int id { get; set; }
        
        public int MadeBy { get; set; }        

        public DateTime DateCreated { get; set; }
        
        public int AssignedTo { get; set; }

        public string Subject { get; set; }
        
        [AllowHtml]
        public string Body { get; set; }

        public DateTime CurrentDate { get; set; }
    }
}