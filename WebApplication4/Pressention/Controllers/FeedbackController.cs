using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Application.Dto.SentimentModel;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication4.Pressention.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedBackService _feedBackService;
        private readonly ISentimentService _sentimentService;
        public FeedbackController(IFeedBackService feedBackService, ISentimentService sentimentService)
        {
            _feedBackService = feedBackService;
            _sentimentService = sentimentService;
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
            if (!ModelState.IsValid)
            {
                return View(feedbackDto);
            }

            // 1. تحليل المشاعر
            var analysisResult = await _sentimentService.AnalyzeAsync(feedbackDto.Notes);

            // 2. التحويل الذكي (استخدام ToUpper لمنع مشاكل الحروف الكبيرة والصغيرة)
            // ده أهم سطر عشان تضمن إن المقارنة تتم صح مهما كانت النتيجة راجعة إزاي
            string label = analysisResult.Label?.Trim().ToUpper() ?? "NEUTRAL";

            FeedbackSentiment sentiment;

            if (label == "POSITIVE")
            {
                TempData["FeedBackMessage"] = "رساله حلوه";
                sentiment = FeedbackSentiment.Positive;
            }
            else if (label == "NEGATIVE")
            {
                TempData["FeedBackMessage"] = "رساله وحشه";
                sentiment = FeedbackSentiment.Negative;
            }
            else if (label == "NEUTRAL")
            {
                TempData["FeedBackMessage"] = "رساله عاديه";
                sentiment = FeedbackSentiment.Neutral;
            }
            else
            {
                sentiment = FeedbackSentiment.Neutral; // لو جت قيمة غريبة → نخليها Neutral
            }

            // 3. تعيين القيمة وحفظ البيانات
            feedbackDto.feedbackSentiment = sentiment;
            var res = await _feedBackService.AddAsync(feedbackDto);

            if (res.IsSuccess)
            {
                /*TempData["FeedBackMessage"] = "تم استلام تقييمك بنجاح"*/
                return RedirectToAction("Index", "Home");
            }

            TempData["FeedBackErrorMessage"] = res.ErrorMessage;
            return RedirectToAction("Index", "Home");
        }


    }
}
