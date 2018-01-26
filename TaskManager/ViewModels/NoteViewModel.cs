using System;
using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class NoteViewModel
    {
        public IEnumerable<Members> Members { get; set; }

        public IEnumerable<Members> MembersMadeBy { get; set; }

        public int id { get; set; }

        public int MadeBy { get; set; }

        public int AssignedTo { get; set; }

        public DateTime DateCreated { get; set; }

        public string Subject { get; set; }
        
        public string Body { get; set; }
    }
}