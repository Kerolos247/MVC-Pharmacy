using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication4.Dto;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientService _patientService;

        public PrescriptionController(IPrescriptionService prescriptionService, IPatientService patientService)
        {
            _prescriptionService = prescriptionService;
            _patientService = patientService;
        }

        // ================= LIST =================
        public async Task<IActionResult> Index()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            return View(prescriptions);
        }

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);
            if (prescription == null) return NotFound();
            return View(prescription);
        }

        // ================= CREATE =================
        public async Task<IActionResult> Create()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(patients, "PatientId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreatePrescription dto)
        {
            if (!ModelState.IsValid)
            {
                // إعادة عرض الفورم
                ViewBag.Patients = new SelectList(await _patientService.GetAllPatientsAsync(), "PatientId", "FullName");
                return View(dto);
            }

            // ربط الصيدلي الحالي
            dto.PharmacistId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            await _prescriptionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        // ================= EDIT =================
        public async Task<IActionResult> Edit(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);
            if (prescription == null) return NotFound();

            var dto = new UpdatePrescriptionDto
            {
                Date = prescription.Date,
                Notes = prescription.Notes,
                PatientId = prescription.PatientId
            };
            var patients = await _patientService.GetAllPatientsAsync();
            ViewBag.Patients = new SelectList(patients, "PatientId", "FullName", dto.PatientId);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePrescriptionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var updated = await _prescriptionService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);
            if (prescription == null) return NotFound();
            return View(prescription);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _prescriptionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
