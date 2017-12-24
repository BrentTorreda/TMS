using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

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
        public IHttpActionResult GetMembers()
        {
            var membersQuery = _context.Members
               .Include(m => m.MemberPosition).Include(m => m.MemberGroup).Include(m => m.ApplicationUser);
            
            var memberDtos = membersQuery
                .ToList()
                .Select(Mapper.Map<Members, MembersDto>);

            return Ok(memberDtos);
        }

        // DELETE /api/members
        [HttpDelete]
        public IHttpActionResult DeleteMember(int id)
        {
            var memberInDb = _context.Members.SingleOrDefault(m => m.MemberId == id);

            if (memberInDb == null)
                return NotFound();

            _context.Members.Remove(memberInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
