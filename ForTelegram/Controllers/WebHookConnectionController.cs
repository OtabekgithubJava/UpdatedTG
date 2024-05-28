using ForTelegram.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ForTelegram.Controllers
{
    public class WebHookConnectionController : ControllerBase
    {
        private readonly BotUpdateHand _handler;

        public WebHookConnectionController(BotUpdateHand handler, ITelegramBotClient client)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Connector([FromBody] Update update, CancellationToken cancellation)
        {
            await _handler.HandleUpdateAsync(update, cancellation);

            return Ok();
        }
    }
}
