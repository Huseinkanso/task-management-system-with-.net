using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DB;
using TaskManagement.Models;
using tm.Models;

namespace tm.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor context;
        private readonly ApplicationDBContext _db;
        public UserController(ApplicationDBContext db,IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            context = httpContextAccessor;
        }
        public IActionResult Index()
        {
            int id = context.HttpContext.Session.GetInt32("id") ?? -1; // Provide a default value if session value is null

            if (id == -1)
            {
                return RedirectToAction("Index", "Login"); // Redirect to login page
            }

            IEnumerable<Project> projects = _db.Projects.Where(p => p.Tasks!.Any(t => t.LoginId == id));
            return View(projects);

        }
        public IActionResult ViewProjectDetails(int? id)
        {
            if (id == null || id == -1)
            {
                return NotFound();
            }
            //var project = _db.Projects.Include(p => p.Tasks!).ThenInclude(t => t.Login!).ThenInclude(t => t.Comments!).FirstOrDefault(p => p.Id == id);
            var project = _db.Projects.Include(p => p.Tasks!).ThenInclude(t => t.Login)
                .Include(p => p.Tasks!).ThenInclude(t => t.Comments!).ThenInclude(c => c.Login)
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }


            return View(project);
        }

        public IActionResult CreateComment(int id)
        {
            CommentInfo t = new()
            {
                Id = id
            };
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(CommentInfo obj)
        {
            obj.Comment.CreationDateTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                _db.Comments.Add(obj.Comment);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }


            return View(obj);
        }

        public IActionResult UserTasks()
        {
            int id = context.HttpContext.Session.GetInt32("id") ?? -1; // Provide a default value if session value is null

            if (id == -1)
            {
                return RedirectToAction("Index", "Login"); // Redirect to login page
            }
            IEnumerable<Tasks> tasks = _db.Tasks.Where(t=>t.LoginId==id).Include(t => t.Login).Include(p => p.Project);
            return View(tasks);

        }

        public IActionResult EditStatus(int id)
        {
            var model = new TaskEdit
            {
                Id = id
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStatus(TaskEdit obj)
        {
            var task = _db.Tasks.Find(obj.Id);
            if(task!=null)
            {
                task.Status = obj.Status;
                _db.SaveChanges();
                return RedirectToAction("UserTasks");
            }
            return RedirectToAction("index");
        }
    }
}
