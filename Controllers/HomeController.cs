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
        Student estudiante = new Student();
            estudiante.Id = new Guid();
            estudiante.Name = "Ernesto";
            estudiante.LastName = "Ramirez";
            estudiante.Tetra= 5;
            estudiante.Cuota = 3000;

            this._context.Students.Add(estudiante);
            this._context.SaveChanges();

        return View();
    }

    public IActionResult Privacy()
    {

        //para insertar
        //Students estudiante = new Students();
        //estudiante.name = "Carlos";
        //estudiante.Id = new Guid();
        //estudiante.Tetra = 5;
        //estudiante.LastName = "Marin";

        //this._context.Students.Add(estudiante);
        //this._context.SaveChanges();
        
        //para Actualizar
        //Students estudianteActualiza = this._context.Students
        //.Where(c => c.Id==new Guid("2763454D-64AA-40C4-5647-08DC85007CF8"))
        //.First();
       // estudianteActualiza.name="Veronica";
       // estudianteActualiza.LastName="Torres";
        //this._context.Students.Update(estudianteActualiza);
       // this._context.SaveChanges();
          //borrar registro
        //Students estudianteBorrado = this._context.Students
        //.Where(c =>c.Id==new Guid("2763454D-64AA-40C4-5647-08DC85007CF8"))
        //.First();
        //this._context.Students.Remove(estudianteBorrado);
        //this._context.SaveChanges();
        
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

    public IActionResult StudentAdd()
    {
        //para insertar
        Student estudiante = new Student();
        estudiante.Name = "Carlos";
        estudiante.Id = new Guid();
        estudiante.Tetra = 5;
        estudiante.LastName = "Marin";
        estudiante.Cuota= 2000;

        this._context.Students.Add(estudiante);
        this._context.SaveChanges();

        return View();
    }

    public IActionResult StudentEdit()
    {
        //para Actualizar
        Student estudianteActualiza = this._context.Students
        .Where(c => c.Id==new Guid("2763454D-64AA-40C4-5647-08DC85007CF8"))
        .First();
       estudianteActualiza.Name="Veronica";
       estudianteActualiza.LastName="Torres";
       this._context.Students.Update(estudianteActualiza);
       this._context.SaveChanges();
        return View();
    }
    public IActionResult StudentDelete()
    {
        //borrar registro
        Student estudianteBorrado = this._context.Students
        .Where(c =>c.Id==new Guid("2763454D-64AA-40C4-5647-08DC85007CF8"))
        .First();
        this._context.Students.Remove(estudianteBorrado);
        this._context.SaveChanges();

        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
