using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Application.IServices;

namespace WebApplication4.Pressention.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedBackService _feedBackService;
        public FeedbackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        [HttpGet]
        public IActionResult CreateFeedback()
        {
            var FeedBackForm = new PatientFeedbackDto();
            return View(FeedBackForm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(PatientFeedbackDto feedbackDto)
        { 
            if(!ModelState.IsValid)
            {
                return View(feedbackDto);
            }
            var res=await _feedBackService.AddAsync(feedbackDto);
            if(res.IsSuccess)
            {
                TempData["FeedBackMessage"] = " تم استلام تقييمك بنجاح وسنحرص دائمًا على تقديم أفضل خدمة لك ";
                return RedirectToAction("Index", "Home");
            }
            TempData["FeedBackMessage"] = res.ErrorMessage;
            return RedirectToAction("Index", "Home");
        } 

    }
}
