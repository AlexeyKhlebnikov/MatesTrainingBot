using MatesTrainingBot;
using MatesTrainingBot.Processors;
using MatesTrainingBot.Processors.Trainings;
using MatesTrainingBot.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<BotSettings>(builder.Configuration.GetSection("BotSettings"));
builder.Services.AddSingleton<IMessageProcessorFactory, MessageMessageProcessorFactory>();
builder.Services.AddTransient<CreateTrainingProcessor>();
builder.Services.AddTransient<InitialMenuProcessor>();

builder.Services.AddSingleton<ITelegramBotClient>((services) =>
{
    var options = services.GetRequiredService<IOptions<BotSettings>>();
    return new TelegramBotClient(options.Value.Token);
});
builder.Services.AddHostedService<TelegramWorker>();

var app = builder.Build();

app.Run();