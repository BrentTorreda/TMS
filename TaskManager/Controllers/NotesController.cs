using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNet.Identity;

namespace TaskManager.Controllers
{
    public class NotesController : Controller
    {
        private ApplicationDbContext _context;

        public NotesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Notes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new NoteFormViewModel();

            var userName = User.Identity.GetUserName();
            var member = _context.Members.Single(m => m.Email == userName);
            viewModel.MadeBy = member.MemberId;

            viewModel.Members = _context.Members.ToList();
            viewModel.DateCreated = DateTime.Now;
            viewModel.id = 0;//01.12.17 - BTo - Needed. Otherwise View will return a Model.IsValid = false

            return View("NoteForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Notes notes)
        {
            if (!ModelState.IsValid)
            {
                return View("NoteForm");
            }

            if (notes.id == 0)
            {
                _context.Notes.Add(notes);
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