using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using ForTelegram.Infrastructure;
using ForTelegram.Services.UserRepo;
using ForTelegram.Models;
using Telegram.Bot.Types.Enums;

namespace ForTelegram.Services
{
    public class BotUpdateHand
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITelegramBotClient _botClient;
        public BotUpdateHand(IServiceScopeFactory serviceScopeFactory, ITelegramBotClient botClient)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _botClient = botClient;
        }
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            var message = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(update.Message, cancellationToken),
                _ => HandleMessageAsync(update.Message, cancellationToken),
            };

            try
            {
                await message;
            }
            catch
            {
                await message;
            }


        }
        private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepos>();

                    var user = new TeleUser()
                    {
                        Id = message.Chat.Id,
                        UserName = message.From.Username
                    };

                    await userRepository.AddUser(user);

                    await _botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"You said:\n<i>{message.Text}</i>",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
