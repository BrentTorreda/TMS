using System;
using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class TaskProceduresController : ApiController
    {
        private ApplicationDbContext _context;

        public TaskProceduresController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/taskProcedures
        public IHttpActionResult GetTaskProcedures(int id)
        {
            var taskProcQuery = _context.TaskProcedures;

            var taskProcDto = taskProcQuery
                .ToList()
                .Where(t => t.SubtaskId == id);

            return Ok(taskProcDto);
        }

        // DELETE /api/taskProcedures
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            var taskProcInDb = _context.TaskProcedures.SingleOrDefault(t => t.TaskProcedureId == id);

            if (taskProcInDb == null)
                return NotFound();

            _context.TaskProcedures.Remove(taskProcInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
