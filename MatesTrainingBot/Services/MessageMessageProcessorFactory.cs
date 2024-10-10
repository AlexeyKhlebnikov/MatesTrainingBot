using MatesTrainingBot.Processors;
using MatesTrainingBot.Processors.Trainings;
using Telegram.Bot.Types;

namespace MatesTrainingBot.Services;

internal sealed class MessageMessageProcessorFactory(IServiceProvider serviceProvider) : IMessageProcessorFactory
{
    public IMessageProcessor GetProcessor(Update update)
    {
        return update switch
        {
            {Message: { } message} => CreateByMessage(message),
            {InlineQuery : { } inlineQuery} => CreateByInlineResult(inlineQuery),
            _ => serviceProvider.GetRequiredService<InitialMenuProcessor>()
        };
    }

    private IMessageProcessor CreateByInlineResult(InlineQuery inlineQuery)
    {
        throw new NotImplementedException();
    }

    private IMessageProcessor CreateByMessage(Message message)
    {
        return message switch
        {
            {Text : Commands.AddTraining} => serviceProvider.GetRequiredService<CreateTrainingProcessor>(),
            {Text: Commands.Start} => serviceProvider.GetRequiredService<InitialMenuProcessor>(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}