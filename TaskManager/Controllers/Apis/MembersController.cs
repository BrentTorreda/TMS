using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Models;
using TaskManager.Dtos;

namespace TaskManager.Controllers.Apis
{
    public class MembersController : ApiController
    {
        private ApplicationDbContext _context;

        public MembersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/members
        public IEnumerable<MembersDto> GetMembers()
        {
            return _context.Members.ToList().Select(Mapper.Map<Members, MembersDto>);
        }
    }
}
