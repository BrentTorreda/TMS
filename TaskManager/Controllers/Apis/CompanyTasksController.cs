using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.Data.Entity;

namespace TaskManager.Controllers.Apis
{
    public class CompanyTasksController : ApiController
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public CompanyTasksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/companytasks
        public IHttpActionResult GetCompanyTasks()
        {
            var companyTasksQuery = _context.CompanyTasks
                .Include(t => t.Companies)
                .Include(t => t.Tasks);

            var companyTasksDtos = companyTasksQuery
                .ToList()
                .Select(Mapper.Map<CompanyTasks, CompanyTasksDto>);

            return Ok(companyTasksDtos);
        }

        [HttpPost]
        public IHttpActionResult PostNotes(int id, bool newVal)
        {
            var noteInDb = _context.Notes.SingleOrDefault(n => n.id == id);

            if (noteInDb != null)
            {
                noteInDb.IsArchived = newVal;

                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
