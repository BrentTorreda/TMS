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
    }
}