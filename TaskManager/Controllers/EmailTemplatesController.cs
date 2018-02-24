using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class EmailTemplatesController : TaskManagerBaseController
    {
        // GET: EmailTemplates
        public async Task<ActionResult> Index()
        {
            await AuthorizeUserInIdentity();

            return View();
        }

        [Route("EmailTemplates/IndexAlt")] //KLUDGE: Default index not working!
        public ActionResult IndexAlt()
        {
            return View("Index");
        }

        // NEW
        public ActionResult New()
        {
            var viewModel = new EmailTemplateViewModel();

            return View("EmailTemplateForm", viewModel);
        }

        // EDIT
        public ActionResult Edit(int id)
        {
            var template = _context.EmailTemplates.SingleOrDefault(t => t.MailTemplateId == id);

            if (template == null)
                return HttpNotFound();

            var viewModel = new EmailTemplateViewModel(template);

            return View("EmailTemplateForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmailTemplates emailTemplate)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EmailTemplateViewModel() { };

                return View("EmailTemplateForm", viewModel);
            }

            if (emailTemplate.MailTemplateId == 0)
            {
                emailTemplate.DateCreated = DateTime.Today;
                emailTemplate.MadeBy = "";

                _context.EmailTemplates.Add(emailTemplate);
            }
            else
            {
                var templateInDb = _context.EmailTemplates.Single(t => t.MailTemplateId == emailTemplate.MailTemplateId);
                templateInDb.Subject = emailTemplate.Subject;
                templateInDb.Body = emailTemplate.Body;
                templateInDb.NumberOfAttachments = emailTemplate.NumberOfAttachments;
                templateInDb.FileNames = emailTemplate.FileNames;
                templateInDb.MadeBy = emailTemplate.MadeBy;
                templateInDb.DateCreated = emailTemplate.DateCreated;
            }

            _context.SaveChanges();

            //save attachments    
            var attachment = new EmailTemplateAttachments();
            string[] stringSeparators = new string[] { ";" };
            string[] result = emailTemplate.FileNames.Split(stringSeparators, StringSplitOptions.None);

            for (int x = 1; x < emailTemplate.NumberOfAttachments; x++)
            {

                attachment.EmailTemplateId = emailTemplate.MailTemplateId;
                attachment.FileName = result[x];
                _context.EmailTemplateAttachments.Add(attachment);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "EmailTemplates");
        }
    }
}