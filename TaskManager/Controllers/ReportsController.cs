using System.Linq;
using System.Web.Mvc;
using DoddleReport.Web;
using DoddleReport;
using System.Threading.Tasks;
using TaskManager.ViewModels.DoddleReportEnumerables;

namespace TaskManager.Controllers
{
    public class ReportsController : TaskManagerBaseController
    {
        // GET: Reports
        [Route("Reports/IndexAlt")]
        public async Task<ActionResult> IndexAlt()
        {
            await AuthorizeUserInIdentity();
            return View("Index");
        }

        // HTTP GET /productreport.pdf - will serve a PDF report
        [Route("Reports/TaskListByMemberReport.pdf")]
        public ReportResult TaskListByMemberReport()
        {
            var viewModel = _context.Database.SqlQuery<TaskListByMembers>(@"
                            SELECT 
                            B.LastName + ', ' + B.FirstName As FullName
                            , A.TaskName As MainTask
                            , A.DateCreated
                            , A.CreatedByAction
                            , C.SubTaskName
                            , C.DateCreated
                            , C.Hours
                            , C.TimeWorked
                            , C1.TaskStatusName As CurrentStatus
                            FROM Tasks A
                            JOIN Members B ON A.MemberId = B.MemberId
                            JOIN SubTasksLevel1 C ON A.TaskId = C.TaskId	
	                            JOIN TaskStatuses C1 ON C.TaskStatusId = C1.TaskStatusId
                            ORDER By
                            B.MemberId
                            , A.IsCompleted 
                            , A.TaskId
                            ").ToList();

            // Get the data for the report (any IEnumerable will work)
            var query = viewModel;

            // Create the report and turn our query into a ReportSource
            var report = new Report(query.ToReportSource());

            // Customize the Text Fields
            report.TextFields.Title = "Task List by Member Report";
            report.RenderHints.BooleanCheckboxes = true;
            report.RenderHints.Orientation = ReportOrientation.Landscape;

            //report.DataFields["Id"].Hidden = true;
            //report.DataFields["Price"].DataFormatString = "{0:c}";
            //report.DataFields["LastPurchase"].DataFormatString = "{0:d}";

            // Return the ReportResult
            // the type of report that is rendered will be determined by the extension in the URL (.pdf, .xls, .html, etc)
            return new ReportResult(report);
        }
    }
}