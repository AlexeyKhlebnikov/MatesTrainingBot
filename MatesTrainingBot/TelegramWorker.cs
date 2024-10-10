using MatesTrainingBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MatesTrainingBot;

internal sealed class TelegramWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IMessageProcessorFactory _messageProcessorFactory;

    public TelegramWorker(ITelegramBotClient botClient, IMessageProcessorFactory messageProcessorFactory)
    {
        _botClient = botClient;
        _messageProcessorFactory = messageProcessorFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery, UpdateType.InlineQuery],
        };

        using var cts = new CancellationTokenSource();
        _botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
        await _botClient.SetMyCommandsAsync(Commands.AvailableCommands,cancellationToken: stoppingToken);
        var me = await _botClient.GetMeAsync(stoppingToken);

        Console.WriteLine($"{me.FirstName} запущен!");
    }

    private Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken cancellation)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        await _messageProcessorFactory
            .GetProcessor(update)
            .ProcessUpdate(client, update, cancellationToken);
    }
}