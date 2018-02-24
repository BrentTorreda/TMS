using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TaskManager.Models;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class TaskManagerBaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        protected ApplicationDbContext _context;

        public TaskManagerBaseController()
        {
            _context = new ApplicationDbContext();
        }

        public TaskManagerBaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }        

        public async Task<bool> AuthorizeUserInIdentity()
        {
            string userName;

            if (Request.IsAuthenticated)
            {
                userName = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
                var user = await UserManager.FindByNameAsync(userName);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
            }
            else
            {
                userName = User.Identity.GetUserName();
            }
            return true;
        }

    }
}