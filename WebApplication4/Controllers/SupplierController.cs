using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

       
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return View(suppliers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreateSupplier dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _supplierService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null) return NotFound();

            var dto = new UpdateSupplierDto
            {
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateSupplierDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var updated = await _supplierService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }

     
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();
            return View(supplier);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _supplierService.DeleteAsync(id);
            if (!success) 
                return View("No_Delete");
            return RedirectToAction(nameof(Index));
        }
    }
}
