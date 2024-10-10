using Telegram.Bot;
using Telegram.Bot.Types;

namespace MatesTrainingBot;

public interface IMessageContext
{
    public ITelegramBotClient BotClient { get; }
    public Update Update { get; }
}

public interface IMessageContext<out T> : IMessageContext
{
    public T? State { get; }
}
