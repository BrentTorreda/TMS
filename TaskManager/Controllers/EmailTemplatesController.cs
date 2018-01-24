using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class EmailTemplatesController : Controller
    {
        private ApplicationDbContext _context;

        public EmailTemplatesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: EmailTemplates
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new EmailTemplateViewModel();

            return View("EmailTemplateForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmailTemplates emailtemplate)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EmailTemplateViewModel() { };

                return View("EmailTemplateForm", viewModel);
            }

            if (emailtemplate.MailTemplateId == 0)
            {
                emailtemplate.DateCreated = DateTime.Today;
                emailtemplate.MadeBy = "";

                _context.EmailTemplates.Add(emailtemplate);
            }
            else
            {
                return View("EmailTemplateForm");
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}