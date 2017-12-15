using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members        
        public ActionResult Index()
        {
            return View();
        }
        
        public ViewResult New()
        {  
            var _context = new ApplicationDbContext();

            var groups = _context.MemberGroups.ToList();
            var positions = _context.MemberPosition.ToList();

            var viewModel = new MembersFormViewModel()
            {
                MemberGroups = groups,
                MemberPositions = positions
            };

            return View("MemberForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Members member)
        {
            var _context = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                var viewModel = new MembersFormViewModel(member)
                {
                    MemberGroups = _context.MemberGroups.ToList(),
                    MemberPositions = _context.MemberPosition.ToList()
                };
                return View("MemberForm", viewModel);
            }

            if (member.MemberId == 0)
            {
                _context.Members.Add(member);
            }
            else
            {
                var memberInDb = _context.Members.Single(m => m.MemberId == member.MemberId);
                memberInDb.LastName = member.LastName;
                memberInDb.FirstName = member.FirstName;
                memberInDb.MemberGroupId = member.MemberGroupId;
                memberInDb.MemberPositionId = member.MemberPositionId;
                memberInDb.Email = member.Email;
                memberInDb.Address = member.Address;
                memberInDb.Phone = member.Phone;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Members");
        }
    }
}