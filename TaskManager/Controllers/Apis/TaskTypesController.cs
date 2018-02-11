using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;
using System.Web;

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
        // POST /api/tasktypes
        [HttpPost]
        public IHttpActionResult PostTaskTypes(int id)
        {
            var taskTypeInDb = _context.TaskTypes.SingleOrDefault(t => t.TaskTypeId == id);

            if (taskTypeInDb != null)
            {
                taskTypeInDb.TaskName = HttpContext.Current.Request.Params["TaskName"];
            }
            else
            {
                var taskTypes = new Models.TaskTypes();

                taskTypes.TaskName = HttpContext.Current.Request.Params["TaskName"];

                _context.TaskTypes.Add(taskTypes);
            }

            _context.SaveChanges();
            return Ok();
        }

    }
}
