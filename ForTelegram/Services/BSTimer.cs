
using ForTelegram.Infrastructure;
using ForTelegram.Services.UserRepo;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ForTelegram.Services
{
    public class BSTimer : BackgroundService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IServiceScopeFactory _scopeFactory;
        public BSTimer(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
        {
            _botClient = botClient;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetService<IUserRepos>();
            var users = await repo.GetAllUsers();
            while(!stoppingToken.IsCancellationRequested)
            {
                foreach(var user in users)
                {
                    await _botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Qales",
                    cancellationToken: stoppingToken
                    
                    ); 
                }
                await Task.Delay(TimeSpan.FromSeconds(12), stoppingToken);
            }
        }
    }
}
