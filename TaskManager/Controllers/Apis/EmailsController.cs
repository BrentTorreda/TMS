using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Models;
using TaskManager.Dtos;
using AutoMapper;

namespace TaskManager.Controllers.Apis
{
    public class EmailsController : ApiController
    {
        private ApplicationDbContext _context;

        public EmailsController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetEmails()
        {
            var emailDtos = _context.Emails
                .ToList()
                .Select(Mapper.Map<Emails, EmailDto>);

            return Ok(emailDtos);
        }
    }
}
