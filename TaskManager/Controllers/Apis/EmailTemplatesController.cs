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
            var emailTemplateDTos = _context.EmailTemplates
                .ToList()
                .Select(Mapper.Map<EmailTemplates, EmailTemplateDto>);

            return Ok(emailTemplateDTos);
        }

        // GET /api/emailtemplates/id
        public IHttpActionResult GetEmailTemplates(int id)
        {
            var emailTemplateDTos = _context.EmailTemplates
                .Where(t => t.MailTemplateId == id)
                .ToList()
                .Select(Mapper.Map<EmailTemplates, EmailTemplateDto>);

            return Ok(emailTemplateDTos);
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
