using System;
using System.Linq;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using TaskManager.TokenStorage;
using System.Configuration;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Globalization;

namespace TaskManager.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var _context = new ApplicationDbContext();

            var viewModel = new DashboardViewModel();

            viewModel.Tasks = _context.Tasks.ToList();
            viewModel.Companies = _context.Companies.ToList();
            viewModel.SubTasksLevel1 = _context.SubTasksLevel1.ToList();
            viewModel.Emails = _context.Emails.ToList();

            string userName = "";
            //OWIN or cookie auth
            if (Request.IsAuthenticated)
            {
                userName = await GetUserEmail();
            }
            else
            {
                userName = User.Identity.GetUserName();
            }

            var member = _context.Members.SingleOrDefault(m => m.Email == userName);

            if (member == null)
            {
                return View("Index", viewModel);
            }
            else
            {
                viewModel.MemberId = member.MemberId;

                //GET MAIL
                IEnumerable<Microsoft.Graph.Message> Mails = await Inbox();

                viewModel.EmailDetails = Mails;
            }
            return View("Index", viewModel);
        }

        public async Task<string> GetAccessToken()
        {
            string accessToken = null;

            // Load the app config from web.config
            string appId = ConfigurationManager.AppSettings["ida:AppId"];
            string appPassword = ConfigurationManager.AppSettings["ida:AppPassword"];
            string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
            string[] scopes = ConfigurationManager.AppSettings["ida:AppScopes"]
                .Replace(' ', ',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Get the current user's ID
            string userId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // Get the user's token cache
                SessionTokenCache tokenCache = new SessionTokenCache(userId, HttpContext);

                ConfidentialClientApplication cca = new ConfidentialClientApplication(
                    appId, redirectUri, new ClientCredential(appPassword), tokenCache.GetMsalCacheInstance(), null);

                // Call AcquireTokenSilentAsync, which will return the cached
                // access token if it has not expired. If it has expired, it will
                // handle using the refresh token to get a new one.
                AuthenticationResult result = await cca.AcquireTokenSilentAsync(scopes, cca.Users.First());

                accessToken = result.AccessToken;
            }

            return accessToken;
        }

        public async Task<IEnumerable<Microsoft.Graph.Message>> Inbox()
        {
            string token = await GetAccessToken();
            if (string.IsNullOrEmpty(token))
            {
                // If there's no token in the session, redirect to Home
                //return Redirect("/");
            }

            string userEmail = await GetUserEmail();

            GraphServiceClient client = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    (requestMessage) =>
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

                        requestMessage.Headers.Add("X-AnchorMailbox", userEmail);

                        return Task.FromResult(0);
                    }));
                var mailResults = await client.Me.MailFolders.Inbox.Messages.Request()
                                    .OrderBy("receivedDateTime DESC")
                                    .Select("subject,receivedDateTime,from,body,attachments")
                                    .Top(100)
                                    .GetAsync();

            SaveMailsToDb(mailResults);

            return mailResults.CurrentPage;
        }

        public async Task<string> GetUserEmail()
        {
            GraphServiceClient client = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        string accessToken = await GetAccessToken();
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", accessToken);
                    }));

            // Get the user's email address
            try
            {
                Microsoft.Graph.User user = await client.Me.Request().GetAsync();
                return user.Mail;
            }
            catch (ServiceException ex)
            {
                return string.Format("#ERROR#: Could not get user's email address. {0}", ex.Message);
            }
        }

        //Save mails to DB
        public void SaveMailsToDb(IEnumerable<Microsoft.Graph.Message> mailResults)
        {
            var emailInDb = new Emails();
            var emails = new Emails();

            foreach (var mail in mailResults)
            {
                //check if mail has already been saved
                emailInDb = _context.Emails.SingleOrDefault(e => e.Id == mail.Id);

                if (emailInDb == null)
                {
                    emails.DateReceived = Convert.ToDateTime( mail.ReceivedDateTime.ToString());
                    emails.Id = mail.Id;
                    emails.Subject = mail.Subject;
                    _context.Emails.Add(emails);
                    _context.SaveChanges();
                }
            }
        }

        [ValidateInput(false)]
        [Route("Dashboard/TaskFromExternal/{taskName}/{taskDesc}/{type}/{id}")]
        public ActionResult TaskFromExternal(string taskName, string taskDesc, string type, int id)
        {
            var viewModel = new TasksFormViewModel()
            {
                TaskTypes = _context.TaskTypes.ToList(),
                TaskCategories = _context.TaskCategories.ToList(),
                Prices = _context.Prices.ToList(),
                Companies = _context.Companies.ToList(),
                TaskStatuses = _context.TaskStatuses.ToList(),
                Members = _context.Members.ToList(),
                //pre-fill based on Note
                CreatedByAction = type,
                AncestorTaskId = id,
                TaskName = taskName,
                TaskDescription = taskDesc,
                TaskStatusId = 1, //KLUDGE - NotStarted
                DateCreated = DateTime.Today
            };
            
            return View("TaskFormNew", viewModel);
        }
    }
}