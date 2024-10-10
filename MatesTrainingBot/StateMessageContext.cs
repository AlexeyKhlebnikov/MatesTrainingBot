using Telegram.Bot;
using Telegram.Bot.Types;

namespace MatesTrainingBot;

public sealed class StateMessageContext<T> : IMessageContext<T>
{
    public StateMessageContext(ITelegramBotClient botClient, Update update, T? state)
    {
        BotClient = botClient;
        Update = update;
        State = state;
    }

    public ITelegramBotClient BotClient { get; }
    public Update Update { get; }
    public T? State { get; }
}