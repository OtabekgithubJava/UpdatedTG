
using ForTelegram.Models;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ForTelegram.Services
{
    public class ConfigureWebHook(IConfiguration configuration, ITelegramBotClient botClient) : BackgroundService
    {
        private readonly ITelegramBotClient _botClient = botClient;
        private readonly BotConfig _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfig>()!;
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var webhookAddress = $"{_botConfig.HostAddress}/bot/{_botConfig.Token}";

            await _botClient.SendTextMessageAsync(
                chatId: _botConfig.MyChatId,
                text: "Start weebhook");

            await _botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: stoppingToken);
        }
    }
}
