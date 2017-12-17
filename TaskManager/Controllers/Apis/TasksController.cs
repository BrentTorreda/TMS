using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class TasksController : ApiController
    {
        private ApplicationDbContext _context;

        public TasksController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/tasks
        public IHttpActionResult GetTasks()
        {
            var tasksQuery = _context.Tasks
               .Include(t => t.TaskCategory).Include(t => t.TaskType).Include(t => t.TaskStatus).Include(t => t.Company).Include(t => t.Price);

            var taskDtos = tasksQuery
                .ToList()
                .Select(Mapper.Map<Tasks, TaskDto>);

            return Ok(taskDtos);
        }

        // DELETE /api/tasks
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            var taskInDb = _context.Tasks.SingleOrDefault(t => t.TaskId == id);

            if (taskInDb == null)
                return NotFound();

            _context.Tasks.Remove(taskInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
