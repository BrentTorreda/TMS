using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class WizardInitialSetupController : TaskManagerBaseController
    {
        // GET: WizardInitialSetup
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowInitialSetupWizard()
        {
            return PartialView("_ShowWizardInitialSetup");
        }
    }
}