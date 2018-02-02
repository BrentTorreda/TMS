using System;

namespace TaskManager.Dtos
{
    public class NoteDto
    { 
        public MembersDto Members { get; set; }
    
        public MembersDto MembersMadeBy { get; set; }
        
        public int id { get; set; }

        public int MadeBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int AssignedTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsMadeToTask { get; set; }

        public bool IsArchived { get; set; }
    }
}