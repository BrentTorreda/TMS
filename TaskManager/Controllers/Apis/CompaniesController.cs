using AutoMapper;
using System.Linq;
using System.Web.Http;
using TaskManager.Dtos;
using TaskManager.Models;
using System.Web;

namespace TaskManager.Controllers.Apis
{
    public class CompaniesController : ApiController
    {
        private ApplicationDbContext _context;

        public CompaniesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/companies
        public IHttpActionResult GetCompanies()
        {           
            var memberDtos = _context.Companies
                .ToList()
                .Select(Mapper.Map<Companies, CompanyDto>);

            return Ok(memberDtos);
        }

        // DELETE /api/companies
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

        // POST /api/companies
        [HttpPost]
        public IHttpActionResult PostCompanies(int id)
        {
            var compInDb = _context.Companies.SingleOrDefault(t => t.CompanyId == id);

            if (compInDb != null)
            {
                compInDb.CompanyName = HttpContext.Current.Request.Params["CompanyName"];
                compInDb.Email = HttpContext.Current.Request.Params["Email"];
                compInDb.Phone = HttpContext.Current.Request.Params["Phone"];
            }
            else
            {
                var companies = new Models.Companies();

                companies.CompanyName = HttpContext.Current.Request.Params["CompanyName"];
                companies.Email = HttpContext.Current.Request.Params["Email"];
                companies.Phone = HttpContext.Current.Request.Params["Phone"];

                _context.Companies.Add(companies);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
