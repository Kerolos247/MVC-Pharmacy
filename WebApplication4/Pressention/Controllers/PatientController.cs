using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Application.IServices;

namespace WebApplication4.Pressention.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreatePatient dto)
        {
            
            if (!ModelState.IsValid)
                return View(dto);

            var res =await _patientService.CreateAsync(dto);
            if(!res.IsSuccess)
                TempData["CreatedMessage"] = res.ErrorMessage;
            else
                TempData["CreatedMessage"] = "Patient created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound();


            var dto = new UpdatePatientDto
            {
                FullName = patient.FullName,
                Phone = patient.Phone,
                Address = patient.Address
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePatientDto dto)
        {
            if (!ModelState.IsValid) 
                return View(dto);

            var res = await _patientService.UpdateAsync(id, dto);
            if (!res.IsSuccess)
                TempData["UpdateMessage"] = res.ErrorMessage;
            else
                TempData["UpdateMessage"] = "Patient updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) 
                return NotFound();

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res=await _patientService.DeleteAsync(id);
            if (!res.IsSuccess)
                TempData["DeleteMessage"] = res.ErrorMessage;
            else
                TempData["DeleteMessage"] = "Patient deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
