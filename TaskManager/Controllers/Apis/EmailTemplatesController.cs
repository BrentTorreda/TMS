using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class EmailTemplatesController : ApiController
    {
        private ApplicationDbContext _context;

        public EmailTemplatesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/emailtemplates
        public IHttpActionResult GetEmailTemplates()
        {
            var typeDtos = _context.EmailTemplates
                .ToList()
                .Select(Mapper.Map<EmailTemplates, EmailTemplateDto>);

            return Ok(typeDtos);
        }

        // DELETE /api/emailtemplates
        [HttpDelete]
        public IHttpActionResult DeleteEmailTemplate(int id)
        {
            var templateInDb = _context.EmailTemplates.SingleOrDefault(t => t.MailTemplateId == id);

            if (templateInDb == null)
                return NotFound();

            _context.EmailTemplates.Remove(templateInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
