using System.Linq;
using System.Web.Mvc;
using TaskManager.Models;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class PricesController : TaskManagerBaseController
    {
        // GET: Prices
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
            var model = new Prices();

            model.PriceId = 0;

            return View("PriceForm", model);
        }

        public ActionResult Edit(int id)
        {
            var _context = new ApplicationDbContext();

            var company = _context.Prices.SingleOrDefault(m => m.PriceId == id);

            if (company == null)
                return HttpNotFound();

            return View("PriceForm", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Prices price)
        {
            var _context = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                return View("PriceForm");
            }

            if (price.PriceId == 0)
            {
                _context.Prices.Add(price);
            }
            else
            {
                var pricesInDb = _context.Prices.Single(c => c.PriceId == price.PriceId);
                pricesInDb.Amount = price.Amount;
                pricesInDb.PriceDescription = price.PriceDescription;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Prices");
        }
    }
}