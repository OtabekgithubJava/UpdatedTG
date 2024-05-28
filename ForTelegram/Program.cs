using Telegram.Bot.Polling;
using Telegram.Bot;
using ForTelegram.Services;
using ForTelegram.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ForTelegram.Services.UserRepo;
using ForTelegram.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<BS>();
builder.Services.AddScoped<BotUpdateHand>();
builder.Services.AddScoped<IUserRepos, UserRepos>();
builder.Services.Configure<HostOptions>(options =>
{
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
builder.Services.AddDbContext<AppDbContext>(option =>
option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var botConfig = builder.Configuration.GetSection("BotConfiguration").Get<BotConfig>();
builder.Services.AddHttpClient("webhook")
    .AddTypedClient<ITelegramBotClient>(httpClient
        => new TelegramBotClient(botConfig.Token, httpClient));

builder.Services.AddHostedService<ConfigureWebHook>();
builder.Services.AddHostedService<BSTimer>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors(ops =>
{
    ops.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    var token = botConfig.Token;

    endpoints.MapControllerRoute(
        name: "webhook",
        pattern: $"bot/{token}",
        new { controller = "WebHookConnect", action = "Connector" });

    endpoints.MapControllers();
});
app.Run();
