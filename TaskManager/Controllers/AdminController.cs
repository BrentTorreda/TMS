using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace TaskManager.Controllers
{
    public class AdminController : TaskManagerBaseController
    {
        // GET: Admin
        public async Task<ViewResult> Index()
        {
            await AuthorizeUserInIdentity();

            var viewModel = _context.Database.SqlQuery<AdminViewModel>("SELECT A.UserName, A.Id, B.RoleId, C.Name FROM AspNetUsers AS A LEFT JOIN AspNetUserRoles AS B ON A.Id = B.UserId LEFT JOIN AspNetRoles AS C ON B.RoleId = C.Id").ToList();
            
            return View("Index", viewModel);
        }

        // POST: Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(FormCollection form)
        {
            //KLUDGED but frickin works
            //TO DO: Need to improve this

            var member = _context.Members.ToList();
            string UserName;
            string CanAddTasks;
            string CanReassignTasks;
            string CanSendEmails;
            string CanChangeSettings;

            foreach (var m in member)
            {
                UserName = form["member.UserName-" + m.ApplicationUserId].ToString();
                CanAddTasks = form["CanAddTasks-" + m.ApplicationUserId].ToString();
                CanReassignTasks = form["CanReassignTasks-" + m.ApplicationUserId].ToString();
                CanSendEmails = form["CanSendEmails-" + m.ApplicationUserId].ToString();
                CanChangeSettings = form["CanChangeSettings-" + m.ApplicationUserId].ToString();

                var user = await UserManager.FindByNameAsync(UserName);
                
                if (CanAddTasks.IndexOf("on") > -1)
                    UserManager.AddToRole(user.Id, "CanAddTasks");
                else
                    UserManager.RemoveFromRole(user.Id, "CanAddTasks");

                if (CanReassignTasks.IndexOf("on") > -1)
                    UserManager.AddToRole(user.Id, "CanReassignTasks");
                else
                    UserManager.RemoveFromRole(user.Id, "CanReassignTasks");

                if (CanSendEmails.IndexOf("on") > -1)
                    UserManager.AddToRole(user.Id, "CanSendEmails");
                else
                    UserManager.RemoveFromRole(user.Id, "CanSendEmails");

                if (CanChangeSettings.IndexOf("on") > -1)
                    UserManager.AddToRole(user.Id, "CanChangeSettings");
                else
                    UserManager.RemoveFromRole(user.Id, "CanChangeSettings");
            }

            TempData["Msg"] = "User permissions successfully updated!";

            return RedirectToAction("Index", "Admin");
        }
    }
}