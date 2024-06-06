using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDProductCatalog.Entities;
using CRUDProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDProductCatalog.Controllers
{
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _context;
        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult PatientList()
        {
            Patient paciete = new Patient();
            paciete.Id = new Guid();
            paciete.Name = "Ernesto";
            paciete.LastName = "Ramirez";
            paciete.Email = "carlos_opr18@hotmail.com";
            paciete.Birth = new DateTime(1995, 04,26);

            this._context.Patients.Add(paciete);
            this._context.SaveChanges();


            List<PatientModel> list = new List<PatientModel>();
         list = _context.Patients.Select(s => new PatientModel()
         {
            Id = s.Id,
            Name=s.Name,
            LastName=s.LastName,
            Email=s.Email,
            Birth=s.Birth,
         }).ToList();
            
        return View(list);

        }
    }
}