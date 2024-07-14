using BLL.Repositories.Issue;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IIssueRepository issueRepository;

		public HomeController(IIssueRepository issueRepository)
        {
			this.issueRepository = issueRepository;
        }

		[Authorize()]
		[HttpGet]
        public ActionResult Index()
		{
			var issuesStatuses = issueRepository.GetIssuesStatus();

			return View(issuesStatuses);
			
		}

		[Authorize]
		public ActionResult About()
		{
			ViewBag.Message = "Welcome back.";

			return View();
		}

		[Authorize]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}