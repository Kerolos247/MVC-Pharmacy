using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.IServices;

namespace WebApplication4.Pressention.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory == null) return NotFound();
            return View(inventory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _inventoryService.DeleteAsync(id);
            if (!deleted)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the inventory.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return View(inventories);
        }
    }
}
