using LibManSysAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibManSysAssignment.Controllers
{
	public class HomeController : Controller
	{
        private readonly AppDbContext Context;

        public HomeController(AppDbContext _Context)
        {
            Context = _Context;
        }

        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}