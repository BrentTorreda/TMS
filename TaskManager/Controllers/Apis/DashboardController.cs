using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class DashboardController : ApiController
    {
        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/dashboard
        public IHttpActionResult GetDashboard()
        {
            var tasksQuery = _context.Tasks
               .Include(t => t.TaskCategory).Include(t => t.TaskType).Include(t => t.TaskStatus).Include(t => t.Company).Include(t => t.Price);

            var taskDtos = tasksQuery
                .ToList()
                .Select(Mapper.Map<Tasks, TaskDto>);

            return Ok(taskDtos);
        }
    }
}
