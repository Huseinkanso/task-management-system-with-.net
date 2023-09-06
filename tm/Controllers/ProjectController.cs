using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagement.DB;

using TaskManagement.Models;

namespace tm.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDBContext _db;
    public ProjectController(ApplicationDBContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Project> employees = _db.Projects;
        return View(employees);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Project obj)
    {
        if (ModelState.IsValid)
        {
            _db.Projects.Add(obj);
            _db.SaveChanges();
            //TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult ViewProjectDetails(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var project = _db.Projects.Include(p => p.Tasks!).ThenInclude(t => t.Login!).ThenInclude(t => t.Comments!).FirstOrDefault(p => p.Id == id);
        var project = _db.Projects
    .Include(p => p.Tasks!)
        .ThenInclude(t => t.Login)
    .Include(p => p.Tasks!)
        .ThenInclude(t => t.Comments!)
            .ThenInclude(c => c.Login)
    .FirstOrDefault(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }


        return View(project);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var project = _db.Projects.Find(id);
        //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Project obj)
    {

        if (ModelState.IsValid)
        {
            _db.Projects.Update(obj);
            _db.SaveChanges();
            //TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }


    public IActionResult Delete(int id)
    {
        var user = _db.Projects.Find(id);
        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteProject(int id)
    {
        var project = _db.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }

        var tasks = _db.Tasks.Where(c => c.ProjectId == project.Id).ToList(); // Materialize the query results into a list

        foreach (var task in tasks)
        {
            var comments = _db.Comments.Where(c => c.TaskId == task.Id).ToList(); // Materialize the query results into a list
            _db.Comments.RemoveRange(comments); // Remove the comments associated with the task
        }

        _db.Tasks.RemoveRange(tasks); // Remove the tasks associated with the project
        _db.Projects.Remove(project); // Remove the project
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    
}
/*
public IActionResult DeleteProject(int id)
{
    var project = _db.Projects.Find(id);
    if (project == null)
    {
        return NotFound();
    }
    var tasks = _db.Tasks.Where(c => c.ProjectId == project.Id);
    foreach (var task in tasks)
    {
        _db.Comments.Where(c => c.TaskId == task.Id).ExecuteDelete();
    }
    tasks.ExecuteDelete();
    //_db.Tasks.Where(c => c.ProjectId == project.Id).ExecuteDelete();
    _db.Projects.Remove(project);
    _db.SaveChanges();
    return RedirectToAction("Index");

}
*/