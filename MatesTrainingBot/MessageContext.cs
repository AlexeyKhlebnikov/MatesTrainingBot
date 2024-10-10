using Telegram.Bot;
using Telegram.Bot.Types;

namespace MatesTrainingBot;

public sealed class MessageContext : IMessageContext
{
    public MessageContext(ITelegramBotClient botClient, Update update)
    {
        BotClient = botClient;
        Update = update;
    }

    public ITelegramBotClient BotClient { get; }
    public Update Update { get; }
}