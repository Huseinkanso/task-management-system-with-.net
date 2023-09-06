using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using TaskManagement.Models;

namespace TaskManagement.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpContextAccessor context;
		private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contxt)
        {
            _logger = logger;
			context = contxt;
          
        }


		public IActionResult Index()
		{
			return View();
           
        }

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