using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class TaskCategoriesController : ApiController
    {
        private ApplicationDbContext _context;

        public TaskCategoriesController()
        {
            _context = new ApplicationDbContext();
        }

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
    }
}
