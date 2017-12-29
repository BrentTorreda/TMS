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
        // GET: Companies        
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            //12.29.17 - BTo - Needed. Otherwise View will return a Model.IsValid = false
            var model = new Companies();

            model.CompanyId = 0;

            return View("CompanyForm", model);
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