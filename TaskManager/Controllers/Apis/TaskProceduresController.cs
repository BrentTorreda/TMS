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

        // GET /api/tasks
        public IHttpActionResult GetTasks()
        {
            var taskProcQuery = _context.TaskProcedures;

            var taskProcDto = taskProcQuery
                .ToList();

            return Ok(taskProcDto);
        }
    }
}
