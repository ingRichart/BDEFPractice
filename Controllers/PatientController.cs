using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDProductCatalog.Entities;
using CRUDProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> PatientList()
        {

            List<PatientModel> pacientes = await _context.Patients.Include(p => p.Specialist).Select(paciente => new PatientModel()
            {
                Id = paciente.Id,
                Name = paciente.Name,
                LastName = paciente.LastName,
                Email = paciente.Email,
                Birth = paciente.Birth,
                SpecialistName = paciente.Specialist.Major,
            }).ToListAsync();

            return View(pacientes);

        }

        [HttpGet]
        public async Task<IActionResult> PatientAdd()
        {
            PatientModel patient = new PatientModel();
            patient.SpecialistL = await _context.Specialists.Select(s => new SelectListItem(){ Value = s.Id.ToString(), Text = s.Major + s.Name}).ToListAsync();

            return View(patient);
        }


        [HttpPost]
        public async Task<IActionResult> PatientAdd(PatientModel model)
        {
            //para insertar
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Patient p = new Patient();
            p.Id = model.Id;
            p.Name = model.Name;
            p.LastName = model.LastName;
            p.Email = model.Email;
            p.Birth = model.Birth;
            p.SpecialistId = model.SpecialistId;

            this._context.Patients.Add(p);
            await this._context.SaveChangesAsync();

            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public async Task<IActionResult> PatientEdit(Guid Id)
        {
            Patient? paciente = await this._context.Patients.Where(p => p.Id == Id).FirstOrDefaultAsync();

            if (paciente == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = Id;
            model.Name = paciente.Name;
            model.LastName = paciente.LastName;
            model.Email = paciente.Email;
            model.Birth = paciente.Birth;
            model.SpecialistId = paciente.SpecialistId;

            model.SpecialistL =
            await this._context.Specialists.Select(s => new SelectListItem() {Value = s.Id.ToString(), Text = s.Major}).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PatientEdit(PatientModel model)
        {

            Patient pacienteactualiza = await this._context.Patients
            .Where(p => p.Id == model.Id).FirstAsync();

            if (pacienteactualiza == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            pacienteactualiza.Name = model.Name;
            pacienteactualiza.LastName = model.LastName;
            pacienteactualiza.Email = model.Email;
            pacienteactualiza.Birth = model.Birth;
            pacienteactualiza.SpecialistId = model.SpecialistId;

            this._context.Patients.Update(pacienteactualiza);
            await this._context.SaveChangesAsync();
            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public async Task<IActionResult> PatientDelete(Guid Id)
        {
            //borrar registro
            Patient? pacienteborrado = await this._context.Patients.Where(p => p.Id == Id).FirstOrDefaultAsync();

            if (pacienteborrado == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = Id;
            model.Name = pacienteborrado.Name;
            model.LastName = pacienteborrado.LastName;
            model.Email = pacienteborrado.Email;
            model.Birth = pacienteborrado.Birth;
            model.SpecialistId = pacienteborrado.SpecialistId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PatientDelete(PatientModel model)
        {
            bool exists = await this._context.Patients.AnyAsync(p => p.Id == model.Id);

            if (!exists)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            Patient pacienteborrado = await this._context.Patients.Where(p => p.Id == model.Id).FirstAsync();
            pacienteborrado.Id = model.Id;
            pacienteborrado.Name = model.Name;
            pacienteborrado.LastName = model.LastName;
            pacienteborrado.Email = model.Email;
            pacienteborrado.Birth = model.Birth;
            pacienteborrado.SpecialistId = model.SpecialistId;

            this._context.Patients.Remove(pacienteborrado);
            await this._context.SaveChangesAsync();

            return RedirectToAction("PatientList", "Patient");
        }
    }
}
