using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.IServices;

namespace WebApplication4.Pressention.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFeedBackService _feedBackService;
        public AdminController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        public async Task<IActionResult> FeedBack()
        {
            var res =await _feedBackService.GetAllAsync();
            return View(res.Data);
        }
    }
}
