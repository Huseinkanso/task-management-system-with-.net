using Microsoft.AspNetCore.Mvc;
using TaskManagement.DB;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor context;
        private readonly ApplicationDBContext _db;
        public LoginController(ApplicationDBContext db,IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            context = httpContextAccessor;
        }
        public IActionResult Index()
        {
           return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string Username, string Password)
        {
            var user = _db.Login.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            if (user != null)
            {
                if(user.Role=="admin")
                {
                    context.HttpContext.Session.SetString("type", "admin");
                    context.HttpContext.Session.SetInt32("id",user.Id );
                    return RedirectToAction("Index", "Admin");

                }else
                {
                    context.HttpContext.Session.SetInt32("id", user.Id);
                    context.HttpContext.Session.SetString("type", "employee");
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                // Login failed
                // You can display an error message or perform other actions
                ModelState.AddModelError("Password", "Invalid username or password");
                return Index();
            }

        }

        
    }
}
