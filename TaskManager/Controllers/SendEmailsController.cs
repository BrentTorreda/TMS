using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

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
                Members = _context.Members.ToList(),
                PrevMailStatus = false
            };

            return View("SendEmailForm", viewModel);
        }

        public ActionResult Save(TasksFormViewModel taskViewModel)
        {
            MailMessage msg = new MailMessage();
            msg.Body = taskViewModel.EmailBody;
            msg.Subject = taskViewModel.TaskName;
            msg.To.Add(new MailAddress(taskViewModel.EmailSendee));

            //get attachments
            string[] stringSeparators = new string[] { ";" };
            string[] result = taskViewModel.FileNames.Split(stringSeparators, StringSplitOptions.None);
            foreach( string file in result)
            {
                if (file != "")
                {
                    Attachment attach = new Attachment(Server.MapPath("~/UploadedFiles/Attachments/" + file));
                    msg.Attachments.Add(attach);
                }
            }   
            
            SendMail(msg);

            //save to Task  
            var task = new Tasks();

            //KLUDGE - temp code
            task.PriceId = 2;
            task.TaskCategoryId = 1;
            task.TaskTypeId = 9;
            task.TaskStatusId = 4;

            task.TaskName = taskViewModel.TaskName;
            task.TimeWorked = taskViewModel.TimeWorked;
            task.CompanyId = taskViewModel.CompanyId;
            task.DateCreated = DateTime.Today;
            task.DateDue = DateTime.Today;
            task.DateWorkStarted = DateTime.Today;
            task.CreatedByAction = "email";

            _context.Tasks.Add(task);

            _context.SaveChanges();

            var newVM = new TasksFormViewModel()
            {
                TaskTypes = _context.TaskTypes.ToList(),
                TaskCategories = _context.TaskCategories.ToList(),
                Prices = _context.Prices.ToList(),
                Companies = _context.Companies.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                Members = _context.Members.ToList(),
                PrevMailStatus = true
            };
            return View("SendEmailForm", newVM);
        }

        public bool SendMail(MailMessage msg)
        {
            //temp code
            string userName = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
            msg.From = new MailAddress(userName);
            String password = "12.Mooroolbark.34";

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