using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

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
            return View("EmailTemplateForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmailTemplates emailtemplate)
        {
            if (!ModelState.IsValid)
            {
                return View("NoteForm");
            }

            if (emailtemplate.MailTemplateId == 0)
            {
                _context.EmailTemplates.Add(emailtemplate);
            }
            else
            {
                return View("NoteForm");
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}