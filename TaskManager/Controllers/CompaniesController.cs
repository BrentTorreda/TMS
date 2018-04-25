using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager.Controllers.SQL;
using System;

namespace TaskManager.Controllers
{
    public class CompaniesController : TaskManagerBaseController
    {

        // GET: Companies        
        public async Task<ActionResult> Index()
        {
            await AuthorizeUserInIdentity();

            if (User.IsInRole("CanChangeSettings"))
                return View("Index");

            return View("ReadOnlyIndex");
        }

        public ViewResult New()
        {
            //12.29.17 - BTo - Needed. Otherwise View will return a Model.IsValid = false
            var viewModel = new CompanyFormViewModel();

            viewModel.CompanyId = 0;
            viewModel.Tasks = _context.Tasks.ToList();

            return View("CompanyForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = new CompanyFormViewModel(_context.Companies.SingleOrDefault(m => m.CompanyId == id));
            viewModel.Tasks = _context.Tasks.ToList();

            if (viewModel == null)
                return HttpNotFound();
            
            return View("CompanyForm", viewModel);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CompanyFormViewModel companyViewModel)
        {
            Companies company = new Companies();
            CompanyTasks compTasks = new CompanyTasks();

            if (!ModelState.IsValid)
            {               
                return View("CompanyForm");
            }

            if (companyViewModel.CompanyId == 0)
            {
                //add company
                company.CompanyName = companyViewModel.CompanyName;
                company.Email = companyViewModel.Email;
                company.Phone = companyViewModel.Phone;
                
                _context.Companies.Add(company);
                _context.SaveChanges(); //save to get the new company id

                //add company tasks
                PrepareTemplate autoInsertTask = new PrepareTemplate();
                var total = System.Web.HttpContext.Current.Request.Params["totalTasks"];
                for( var i = 1; i <= Convert.ToInt32(total); i++)
                {
                    compTasks.CompanyId = company.CompanyId;
                    compTasks.TaskId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params["TaskId" + i.ToString()]);

                    //auto insert tasks
                    autoInsertTask.InsertTemplateForCompany(compTasks.TaskId, compTasks.CompanyId);
                }

                _context.CompanyTasks.Add(compTasks);
            }
            else
            {
                var companyInDb = _context.Companies.Single(c => c.CompanyId == company.CompanyId);
                companyInDb.CompanyName = company.CompanyName;
                companyInDb.Email = company.Email;
                companyInDb.Phone = company.Phone;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Companies");
        }
    }
}