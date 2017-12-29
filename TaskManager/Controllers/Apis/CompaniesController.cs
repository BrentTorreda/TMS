using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers.Apis
{
    public class CompaniesController : ApiController
    {
        private ApplicationDbContext _context;

        public CompaniesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/members
        public IHttpActionResult GetCompanies()
        {           
            var memberDtos = _context.Companies
                .ToList()
                .Select(Mapper.Map<Companies, CompanyDto>);

            return Ok(memberDtos);
        }

        // DELETE /api/members
        [HttpDelete]
        public IHttpActionResult DeleteCompany(int id)
        {
            var companyInDb = _context.Companies.SingleOrDefault(m => m.CompanyId == id);

            if (companyInDb == null)
                return NotFound();

            _context.Companies.Remove(companyInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
