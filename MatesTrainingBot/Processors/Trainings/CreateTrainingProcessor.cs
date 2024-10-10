using MatesTrainingBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MatesTrainingBot.Processors.Trainings;

internal sealed class CreateTrainingProcessor : IMessageProcessor
{
    
    public Task ProcessUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}