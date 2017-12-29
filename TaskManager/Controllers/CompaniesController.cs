using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManager.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: Members        
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            var _context = new ApplicationDbContext();
            
            return View("CompanyForm");
        }

        public ActionResult Edit(int id)
        {
            var _context = new ApplicationDbContext();

            var company = _context.Companies.SingleOrDefault(m => m.CompanyId == id);

            if (company == null)
                return HttpNotFound();
            
            return View("CompanyForm", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Companies company)
        {
            var _context = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {               
                return View("CompanyForm");
            }

            if (company.CompanyId == 0)
            {
                _context.Companies.Add(company);
            }
            else
            {
                var compnayInDb = _context.Companies.Single(c => c.CompanyId == company.CompanyId);
                compnayInDb.CompanyName = company.CompanyName;
                compnayInDb.Email = company.Email;
                compnayInDb.Phone = company.Phone;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Companies");
        }
    }
}