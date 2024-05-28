using Telegram.Bot.Polling;
using Telegram.Bot;

namespace ForTelegram.Services
{
    public class BS : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;
       // private readonly IUpdateHandler _updateHandler;

        public BS(ITelegramBotClient telegramBotClient)
        {
            _botClient = telegramBotClient;
            //_updateHandler = updateHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

           /* _botClient.StartReceiving(
                updateHandler: _updateHandler.HandleUpdateAsync,
                pollingErrorHandler: _updateHandler.HandlePollingErrorAsync,
                receiverOptions: new Telegram.Bot.Polling.ReceiverOptions()
                {

                    ThrowPendingUpdates = true
                },
                cancellationToken: stoppingToken
                );*/
        }
    }
}
