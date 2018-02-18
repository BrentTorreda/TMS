using System.Web.Http;
using AutoMapper;
using System.Linq;
using TaskManager.Dtos;
using TaskManager.Models;
using System.Data.Entity;
using System.Net;
using System.Web;
using System;

namespace TaskManager.Controllers.Apis
{
    public class EmailsController : ApiController
    {
        private ApplicationDbContext _context;

        public EmailsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("api/emails/{filter}")]
        public IHttpActionResult GetEmails(string filter)
        {
            var emailQuery = _context.Emails.Where(e => e.IsArchived == false);

            if (filter == "all")
            {
                emailQuery = _context.Emails.Where(e => e.IsArchived == true || e.IsArchived == false);
            }

            var emailDtos = emailQuery
                .ToList()
                .Select(Mapper.Map<Emails, EmailDto>);

            return Ok(emailDtos);
        }

        // POST /api/emails
        [HttpPost]
        [Route("api/emails/{id}/{newVal}")]
        public IHttpActionResult PostEmails(int id, bool newVal)
        {
            var emailInDb = _context.Emails.SingleOrDefault(n => n.MailId == id);

            if (emailInDb != null)
            {
                emailInDb.IsArchived = newVal;

                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
