using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dto;
using WebApplication4.Service_Layer.Implementation;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Controllers
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

            await _medicineService.CreateAsync(dto);
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

            var updated = await _medicineService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

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
               
                await _medicineService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
               
                var medicine = await _medicineService.GetByIdAsync(id);
                if (medicine == null)
                    return NotFound();

               
                ViewBag.ErrorMessage = "This cannot be deleted because it contains records related to securities. New inventory must be deleted.";

               
                return View("Delete", medicine);
            }
        }

    }
}
