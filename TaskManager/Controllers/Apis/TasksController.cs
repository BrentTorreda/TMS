using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.SQL;
using System;
using System.Globalization;

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
                   .Include(t => t.TaskCategory)
                   .Include(t => t.TaskType)
                   .Include(t => t.TaskStatus)
                   .Include(t => t.Company)
                   .Include(t => t.Price);

            var taskDtos = tasksQuery
                   .ToList()
                   .Select(Mapper.Map<Tasks, TaskDto>);

            return Ok(taskDtos);
        }              

        // GET /api/tasks

        public IHttpActionResult GetTask(int id)
        {
            var tasksQuery = _context.Tasks
                   .Include(t => t.TaskCategory)
                   .Include(t => t.TaskType)
                   .Include(t => t.TaskStatus)
                   .Include(t => t.Company)
                   .Include(t => t.Price);

            if (id != 0)
                tasksQuery = tasksQuery.Where(t => t.CompanyId == id);

            var taskDtos = tasksQuery
                   .ToList()
                   .Select(Mapper.Map<Tasks, TaskDto>);

            return Ok(taskDtos);
        }

        // GET api/tasks
        [Route("api/Tasks/{id}/{getBy}")]
        public IHttpActionResult GetTasks(int id, string getBy)
        {
            var tasksQuery = _context.Tasks
                   .Include(t => t.TaskCategory)
                   .Include(t => t.TaskType)
                   .Include(t => t.TaskStatus)
                   .Include(t => t.Company)
                   .Include(t => t.Price);

            if (getBy == "status")
                tasksQuery = tasksQuery.Where(t => t.TaskStatusId == id);
            else if (getBy == "company")
                tasksQuery = tasksQuery.Where(t => t.CompanyId == id);
            else if (getBy == "ancestor")
                tasksQuery = tasksQuery.Where(t => t.AncestorTaskId == id);

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

        // POST /api/tasks
        [HttpPost]
        public IHttpActionResult PostTasks(int id)
        {
            //get type of occurrence
            var pattern = HttpContext.Current.Request.Params["RenewalPattern"];

            //get days
            int[] days = new int[7];
            if (HttpContext.Current.Request.Params["daylistSunday"] == "on")
                days[0] = 1;
            if (HttpContext.Current.Request.Params["daylistMonday"] == "on")
                days[1] = 1;
            if (HttpContext.Current.Request.Params["daylistTuesday"] == "on")
                days[2] = 1;
            if (HttpContext.Current.Request.Params["daylistWednesday"] == "on")
                days[3] = 1;
            if (HttpContext.Current.Request.Params["daylistThursday"] == "on")
                days[4] = 1;
            if (HttpContext.Current.Request.Params["daylistFriday"] == "on")
                days[5] = 1;
            if (HttpContext.Current.Request.Params["daylistSaturday"] == "on")
                days[6] = 1;

            //get number of dates to insert
            var dates = HttpContext.Current.Request.Params["NumberOfDates"];

            var startDate = HttpContext.Current.Request.Params["StartDate"];

            var sqlTrans = new InsertTaskOccurences();

            sqlTrans.InsertTasks(id, Convert.ToInt32(pattern), days, Convert.ToInt32(dates), DateTime.ParseExact(startDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture));

            return Ok();
        }
    }
}
