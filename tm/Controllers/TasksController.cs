using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using TaskManagement.DB;
using TaskManagement.Models;
using tm.Models;

namespace TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDBContext _db;
        public TasksController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index(TaskFilter? taskFilter)
        {
            IEnumerable<Login> employees = _db.Login.Where(u => u.Role == "Employee");

            IEnumerable<Tasks> tasks;

            if (taskFilter == null || (string.IsNullOrEmpty(taskFilter.status) && string.IsNullOrEmpty(taskFilter.employee)))
            {
                tasks = _db.Tasks.Include(t => t.Login).Include(p => p.Project);
            }
            else
            {
                tasks = _db.Tasks.Where(t => t.Status == taskFilter.status || t.Login!.Username == taskFilter.employee)
                    .Include(t => t.Login).Include(p => p.Project);
            }

            var viewModel = new TaskFilter
            {
                employees = employees,
                Tasks = tasks
            };

            return View(viewModel);
        }


        public IActionResult Create()
        {
            var employees = _db.Login.Where(u => u.Role != "admin");
            var projects = _db.Projects;

            var viewModel = new TaskInfo
            {
                Projects = projects,
                Employees = employees,
           
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskInfo obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj.Task);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var task = _db.Tasks.Find(id);
            var employees = _db.Login.Where(u => u.Role != "admin");
            var projects = _db.Projects;
            if (task == null)
            {
                return NotFound();
            }
            var viewModel = new TaskInfo
            {
                Projects = projects,
                Employees = employees,
                Task = task
            };

            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);



            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskInfo obj)
        {

            if (ModelState.IsValid)
            {
                _db.Tasks.Update(obj.Task);
                _db.SaveChanges();
                //TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
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
                //TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            

            return View(obj);
        }


        public IActionResult Delete(int id)
        {
            var task = _db.Tasks.Include(t => t.Login).Include(p => p.Project).FirstOrDefault(t => t.Id == id);
            return View(task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTask(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            _db.Comments.Where(c => c.TaskId == task.Id).ExecuteDelete();

            _db.Tasks.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
