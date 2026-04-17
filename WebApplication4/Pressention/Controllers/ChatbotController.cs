using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.ChatAi;
using WebApplication4.Application.IServices;

public class ChatbotController : Controller
{
    private readonly IChatAiService _chatAiService;

    public ChatbotController(IChatAiService chatAiService)
    {
        _chatAiService = chatAiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(request?.Question))
        {
            return BadRequest(new { answer = "الرجاء إدخال سؤال." });
        }

        var answer = await _chatAiService.Ask(request.Question);

        return Json(new { answer });
    }
}