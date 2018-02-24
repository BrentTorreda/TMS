using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    public class AdminController : TaskManagerBaseController
    {
        // GET: Admin
        public async Task<ActionResult>  Index()
        {
            await AuthorizeUserInIdentity();

            AdminViewModel viewModel = new AdminViewModel();

            return View();
        }
    }
}