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
            var res = await _inventoryService.DeleteAsync(id);
            if (!res.IsSuccess)
                TempData["DeleteMessage"] = res.ErrorMessage;
            else
                TempData["DeleteMessage"] = "Inventory deleted successfully!";
          

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return View(inventories);
        }
    }
}
