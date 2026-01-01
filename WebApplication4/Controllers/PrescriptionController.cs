using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Implementation;
using WebApplication4.Service_Layer.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<IActionResult> Index()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            return View(prescriptions);
        }

        public async Task<IActionResult> Details(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);
            if (prescription == null) return NotFound();
            return View(prescription);
        }

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
               
                ViewBag.Patients = new SelectList(await _patientService.GetAllPatientsAsync(), "PatientId", "FullName");
                return View(dto);
            }

           
            dto.PharmacistId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            await _prescriptionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int id)
        {
            var day= DateTime.Today;
            IPaymentStrategy payment = PaymentStrategyFactory.GetStrategy();
            var success = await _prescriptionService.PayAsync(id, payment);


            if (!success.operation)
            {
                TempData["ErrorMessage"] = "Payment failed. Check inventory or prescription status.";
                return RedirectToAction("index");
            }
            
            TempData["SuccessMessage"] = "Payment successful!";
            TempData["TotalCost"] = success.Cost.ToString("F2"); 
            return RedirectToAction(nameof(Index));
        }
       




    }
}
