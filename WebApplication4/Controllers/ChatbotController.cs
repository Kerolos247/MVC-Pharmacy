using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using OpenAI;

public class ChatbotController : Controller
{
    private readonly ChatClient _chatClient;

    public ChatbotController(IConfiguration config)
    {
        string apiKey = config["OpenAI:ApiKey"];
        _chatClient = new ChatClient(model: "gpt-3.5-turbo", apiKey: apiKey);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    public class QuestionRequest
    {
        public string Question { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Ask([FromBody] QuestionRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Question))
                return Json(new { answer = "من فضلك اكتب السؤال أولاً." });

            ChatCompletion completion = await _chatClient.CompleteChatAsync(
                new ChatMessage[]
                {
                new SystemChatMessage("انت AI متخصص في الأدوية."),
                new UserChatMessage(request.Question)
                }
            );

            string answer = completion.Content[0].Text.Trim();

            return Json(new { answer });
        }
        catch (Exception ex)
        {
            return Json(new { answer = "Error: " + ex.Message });
        }
    }

}
