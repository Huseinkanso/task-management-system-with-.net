using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DB;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext _db;
        public EmployeeController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Login> employees = _db.Login.Where(u => u.Role == "Employee");
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Login obj)
        {
            if (ModelState.IsValid)
            {
                _db.Login.Add(obj);
                _db.SaveChanges();
                //TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var user = _db.Login.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Login obj)
        {
        
            if (ModelState.IsValid)
            {
                _db.Login.Update(obj);
                _db.SaveChanges();
                //TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int id)
        {
            var user = _db.Login.Find(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int id)
        {
            var user = _db.Login.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var tasks = _db.Tasks.Where(t => t.LoginId == user.Id).ToList(); // Materialize the query results into a list

            foreach (var task in tasks)
            {
                var comments = _db.Comments.Where(c => c.TaskId == task.Id).ToList(); // Materialize the query results into a list
                _db.Comments.RemoveRange(comments); // Remove the comments associated with the task
            }

            _db.Tasks.RemoveRange(tasks); // Remove the tasks associated with the login
            _db.Login.Remove(user); // Remove the login
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        
    }
}


/*
 * public IActionResult DeleteEmp(int id)
        {
            var user = _db.Login.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            _db.Comments.Where(c => c.LoginId == user.Id).ExecuteDelete();
            _db.Login.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }**/