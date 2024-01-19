using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using Microsoft.Extensions.Options;


namespace ChatGPT_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : Controller
    {

        private readonly AppSettings _appSettings;

        public OpenAIController(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

        [HttpGet]
        [Route("UseChatGPT")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            var openAIKey = _appSettings.OpenAIKey;
            var openai = new OpenAIAPI(openAIKey);
            ChatRequest chatRequest = new ChatRequest();
            chatRequest.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
            chatRequest.Messages = new ChatMessage[]{new ChatMessage(ChatMessageRole.User, query)} ;
            chatRequest.MaxTokens = 550;

            var anws = await openai.Chat.CreateChatCompletionAsync(chatRequest);

            return Ok(Json(anws));

        }
    }
}