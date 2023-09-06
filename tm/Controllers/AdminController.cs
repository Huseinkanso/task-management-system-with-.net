using Microsoft.AspNetCore.Mvc;

namespace tm.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
