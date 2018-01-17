using System.Web.Http;
using AutoMapper;
using System.Linq;
using TaskManager.Dtos;
using TaskManager.Models;
using System.Data.Entity;

namespace TaskManager.Controllers.Apis
{
    public class NotesController : ApiController
    {
        private ApplicationDbContext _context;

        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/notes/id/getby
        [Route("api/notes/{id}/{getBy}")]
        public IHttpActionResult GetNotes(int id, string getBy)
        {
            var noteQuery = _context.Notes
                .Include( n => n.MembersMadeBy )
                .Include( n => n.Members );

            if (getBy == "member")
                noteQuery = noteQuery.Where(n => n.AssignedTo == id);

            var noteDtos = noteQuery
                .ToList()
                .Select(Mapper.Map<Notes, NoteDto>);

            return Ok(noteDtos);
        }
    }
}
