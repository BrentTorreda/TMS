using System.Web.Http;
using AutoMapper;
using System.Linq;
using TaskManager.Dtos;
using TaskManager.Models;
using System.Data.Entity;
using System.Net;

namespace TaskManager.Controllers.Apis
{
    public class NotesController : ApiController
    {
        private ApplicationDbContext _context;

        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/notes/id/getby/filter
        [Route("api/notes/{id}/{getBy}/{filter}")]
        public IHttpActionResult GetNotes(int id, string getBy, string filter)
        {
            var noteQuery = _context.Notes
                .Include( n => n.MembersMadeBy )
                .Include( n => n.Members );
                        
            if (getBy == "member")
            {
                if (filter == "all")
                {
                    noteQuery = noteQuery.Where(n => n.AssignedTo == id && (n.IsArchived == true || n.IsArchived == false));
                }
                else
                {
                    noteQuery = noteQuery.Where(n => n.AssignedTo == id && (n.IsArchived == false));
                }                    
            }                

            var noteDtos = noteQuery
                .ToList()
                .Select(Mapper.Map<Notes, NoteDto>);

            return Ok(noteDtos);
        }

        // UPDATE /api/notes
        [HttpPut]
        public IHttpActionResult UpdateNotes(int id, NoteDto noteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Notes.SingleOrDefault(m => m.id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(noteDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

    }
}
