using AutoMapper;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.Controllers.SQL;
using System;
using System.Globalization;


namespace TaskManager.Controllers.Apis
{
    public class SubTaskDeferralController : ApiController
    {
        private ApplicationDbContext _context;

        public SubTaskDeferralController()
        {
            _context = new ApplicationDbContext();
        }

        // POST api/subtaskdeferral
        [HttpPost]
        [Route("api/subtaskdeferral/{subtaskId}")]

        public IHttpActionResult PostSubtaskDeferral(int subtaskId)
        {
            var assignedTo = HttpContext.Current.Request.Params["MemberId"];
            assignedTo = assignedTo.Substring(0, assignedTo.IndexOf(",")); //To Do: This is a workaround, somehow the <select> is returning 2 values
            var deferredTo = HttpContext.Current.Request.Params["DeferredTo"];
            var deferredFor = HttpContext.Current.Request.Params["DeferredFor"];                
            var reason = HttpContext.Current.Request.Params["Reason"];

            //Insert the deferral 
            var deferral = new SubTasksDeferralDetails();
            deferral.DeferredTo = DateTime.ParseExact(deferredTo, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            //deferredFor = Convert.ToString(DateTime.ParseExact(deferredFor, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture));
            //TimeSpan interval = TimeSpan.Parse(deferredFor);
            //deferral.DeferredFor = interval;
            deferral.MemberId = Convert.ToInt32(assignedTo);
            deferral.DeferredOn = DateTime.Today;
            deferral.DeferredReason = reason;
            deferral.SubTaskId = subtaskId;

            _context.SubTaskDeferralDetails.Add(deferral); 
            _context.SaveChanges();

            //Update the assignee
            var subtaskInDb = _context.SubTasksLevel1.SingleOrDefault(t => t.SubTaskId == subtaskId);
            if (subtaskInDb != null)
            {
                subtaskInDb.MemberId = Convert.ToInt32(assignedTo);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
