using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto.Medcine;
using WebApplication4.Application.IServices;

namespace WebApplication4.Pressention.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;

        public MedicineController(
            IMedicineService medicineService,
            ICategoryService categoryService,
            ISupplierService supplierService)
        {
            _medicineService = medicineService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetAllMedicinesAsync();
            return View(medicines);
        }

        public async Task<IActionResult> Details(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
            ViewBag.Suppliers = new SelectList(await _supplierService.GetAllSuppliersAsync(), "SupplierId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreateMedcine dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
                ViewBag.Suppliers = new SelectList(await _supplierService.GetAllSuppliersAsync(), "SupplierId", "Name");
                return View(dto);
            }

            var res =await _medicineService.CreateAsync(dto);
            if (!res.IsSuccess)
                TempData["CreatedMessage"] = res.ErrorMessage;
            else
                TempData["CreatedMessage"] = "Medicine created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            var inventory = medicine.Inventory;

            var dto = new UpdateMedcineDto
            {
                Name = medicine.Name,
                Description = medicine.Description,
                DosageForm = medicine.DosageForm,
                Strength = medicine.Strength,
                Price = medicine.Price,
                CategoryId = medicine.CategoryId,
                SupplierId = medicine.SupplierId,
                Quantity = inventory?.Quantity,
                ExpiryDate = inventory?.ExpiryDate
            };

            ViewBag.Categories = new SelectList(
                await _categoryService.GetAllCategoriesAsync(),
                "CategoryId",
                "Name",
                dto.CategoryId
            );

            ViewBag.Suppliers = new SelectList(
                await _supplierService.GetAllSuppliersAsync(),
                "SupplierId",
                "Name",
                dto.SupplierId
            );

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateMedcineDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name", dto.CategoryId);
                ViewBag.Suppliers = new SelectList(await _supplierService.GetAllSuppliersAsync(), "SupplierId", "Name", dto.SupplierId);
                return View(dto);
            }

            var res = await _medicineService.UpdateAsync(id, dto);
            if (!res.IsSuccess)
                TempData["UpdatedMessage"] = res.ErrorMessage;
            else
                TempData["UpdatedMessage"] = "Medicine updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var res = await _medicineService.DeleteAsync(id);
                if (!res.IsSuccess)
                    TempData["DeletedMessage"] = res.ErrorMessage;
                else
                    TempData["DeletedMessage"] = "Medicine deleted successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var medicine = await _medicineService.GetByIdAsync(id);
                if (medicine == null)
                    return NotFound();

                return NotFound();
            }
        }
    }
}
