using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class TaskTypesController : ApiController
    {
        private ApplicationDbContext _context;

        public TaskTypesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/taskcategories
        public IHttpActionResult GetTaskTypes()
        {
            var typeDtos = _context.TaskTypes
                .ToList()
                .Select(Mapper.Map<TaskTypes, TaskTypeDto>);

            return Ok(typeDtos);
        }

        // DELETE /api/taskcategories
        [HttpDelete]
        public IHttpActionResult DeleteTaskType(int id)
        {
            var typeInDb = _context.TaskTypes.SingleOrDefault(t => t.TaskTypeId == id);

            if (typeInDb == null)
                return NotFound();

            _context.TaskTypes.Remove(typeInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
