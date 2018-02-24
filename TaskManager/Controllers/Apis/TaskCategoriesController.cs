using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.SQL;
using System;

namespace TaskManager.Controllers.Apis
{
    public class TaskCategoriesController : ApiController
    {
        private ApplicationDbContext _context;

        public TaskCategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        // GET /api/taskcategories
        public IHttpActionResult GetTaskCategories()
        {
            var categoryDtos = _context.TaskCategories
                .ToList()
                .Select(Mapper.Map<TaskCategories, TaskCategoryDto>);

            return Ok(categoryDtos);
        }

        // DELETE /api/taskcategories
        [HttpDelete]
        public IHttpActionResult DeleteTaskCategory(int id)
        {
            var taskCategoryInDb = _context.TaskCategories.SingleOrDefault(t => t.TaskCategoryId == id);

            if (taskCategoryInDb == null)
                return NotFound();

            _context.TaskCategories.Remove(taskCategoryInDb);
            _context.SaveChanges();

            return Ok();
        }

        // POST /api/taskscategories
        [HttpPost]
        public IHttpActionResult PostTaskCategories(int id)
        {
            var taskCatInDb = _context.TaskCategories.SingleOrDefault(t => t.TaskCategoryId == id);

            if (taskCatInDb != null)
            {
                taskCatInDb.CategoryName = HttpContext.Current.Request.Params["CategoryName"];
            }
            else
            {
                var taskCategories = new Models.TaskCategories();

                taskCategories.CategoryName = HttpContext.Current.Request.Params["CategoryName"];

                _context.TaskCategories.Add(taskCategories);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}