using Telegram.Bot;
using Telegram.Bot.Types;

namespace MatesTrainingBot.Services;

public interface IMessageProcessorFactory
{
    IMessageProcessor GetProcessor(Update update);
}

public interface IMessageProcessor
{
    Task ProcessUpdate(ITelegramBotClient client, Update update, CancellationToken cancellationToken);   
} 