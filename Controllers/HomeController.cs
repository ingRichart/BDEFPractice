using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDProductCatalog.Models;
using CRUDProductCatalog.Entities;

namespace CRUDProductCatalog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {

        return View();
    }

    public IActionResult Privacy()
    {
        
         List<StudentModel> list = new List<StudentModel>();
         list = _context.Students.Select(s => new StudentModel()
         {
            Id = s.Id,
            Name=s.Name,
            LastName=s.LastName,
            Tetra=s.Tetra,
            Cuota=s.Cuota,
         }).ToList();
            
        return View(list);
    }

    [HttpGet]
    public IActionResult StudentAdd()
    {
        return View();
    }


    [HttpPost]
    public IActionResult StudentAdd(StudentModel model)
    {
        //para insertar
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Student s = new Student();
        s.Id = model.Id;
        s.Name = model.Name;
        s.LastName = model.LastName;
        s.Tetra = model.Tetra;
        s.Cuota = model.Cuota;

        this._context.Students.Add(s);
        this._context.SaveChanges();

        return RedirectToAction("Privacy", "Home");
    }

    [HttpGet]
    public IActionResult StudentEdit(Guid Id)
    {
        Student? estudiante = this._context.Students.Where(s => s.Id == Id).FirstOrDefault();

        if(estudiante == null)
        {
            return RedirectToAction ("Privacy","Home");
        } 

        StudentModel model = new StudentModel();
        model.Id = Id;
        model.Name = estudiante.Name;
        model.LastName = estudiante.LastName;
        model.Tetra = estudiante.Tetra;
        model.Cuota = estudiante.Cuota;

        return View(model);
    }

    [HttpPost]
    public IActionResult StudentEdit(StudentModel model)
    {

        Student estudianteActualiza = this._context.Students
        .Where(s => s.Id == model.Id).First();

        if (estudianteActualiza == null)
        {
            return RedirectToAction ("Privacy", "Home");
        }
        
        estudianteActualiza.Name = model.Name;
        estudianteActualiza.LastName = model.LastName;
        estudianteActualiza.Tetra = model.Tetra;
        estudianteActualiza.Cuota = model.Cuota;

        this._context.Students.Update(estudianteActualiza);
        this._context.SaveChanges();
        return RedirectToAction("Privacy", "Home");
    }

    [HttpGet]
    public IActionResult StudentDelete(Guid Id)
    {
        //borrar registro
        Student? estudianteborrado = this._context.Students.Where(s => s.Id == Id).FirstOrDefault();

        if (estudianteborrado == null)
        {
            return RedirectToAction("Privacy", "Home");
        }

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Privacy", "Home");
        }

        StudentModel model = new StudentModel();
        model.Id = Id;
        model.Name = estudianteborrado.Name;
        model.LastName = estudianteborrado.LastName;
        model.Tetra = estudianteborrado.Tetra;
        model.Cuota = estudianteborrado.Cuota;

        return View(model);
    }

    [HttpPost]
    public IActionResult StudentDelete(StudentModel model)
    {
        bool exists = this._context.Students.Any(s => s.Id == model.Id);

        if(!exists)
        {
            return RedirectToAction("Privacy", "Home");
        } 

        Student estudianteborrado = this._context.Students.Where(s => s.Id == model.Id).First();

        this._context.Students.Remove(estudianteborrado);
        this._context.SaveChanges();

        return RedirectToAction("Privacy", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
