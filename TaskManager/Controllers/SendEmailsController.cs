using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.Net.Mail;

namespace TaskManager.Controllers
{
    public class SendEmailsController : Controller
    {
        private ApplicationDbContext _context;

        public SendEmailsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: SendEmails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new TasksFormViewModel()
            {
                TaskTypes = _context.TaskTypes.ToList(),
                TaskCategories = _context.TaskCategories.ToList(),
                Prices = _context.Prices.ToList(),
                Companies = _context.Companies.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                Members = _context.Members.ToList()
            };

            return View("SendEmailForm", viewModel);
        }

        public ActionResult Save(TasksFormViewModel taskViewModel)
        {
            MailMessage msg = new MailMessage();
            msg.Body = taskViewModel.EmailBody;
            msg.Subject = taskViewModel.EmailSubject;
            SendMail(msg);

            //save to Task  
            var task = new Tasks();
            task.TaskName = taskViewModel.TaskName;
            task.TimeWorked = taskViewModel.TimeWorked;

            task.DateCreated = DateTime.Today;
            task.DateDue = DateTime.Today;
            task.DateWorkStarted = DateTime.Today;
            task.CreatedByAction = "email";

            _context.Tasks.Add(task);

            _context.SaveChanges();

            return View("SendEmailForm");
        }

        public bool SendMail(MailMessage msg)
        {
            String userName = "brent.torreda@binacoregroup.com.au";
            String password = "12.Mooroolbark.34";
            msg.To.Add(new MailAddress("torreda.brent@gmail.com"));
            msg.From = new MailAddress(userName);
            msg.Subject = "Test Office 365 Account";
            msg.Body = "Testing email using Office 365 account.";
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.office365.com";
            client.Credentials = new System.Net.NetworkCredential(userName, password);
            client.Port = 587;
            client.EnableSsl = true;
            client.Send(msg);

            return true;
        }
    }
}