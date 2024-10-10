using MatesTrainingBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MatesTrainingBot.Processors;

internal class InitialMenuProcessor : IMessageProcessor
{
    public async Task ProcessUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        const string usage = "Welcome";

        await botClient.SendTextMessageAsync(update.Message.Chat,
            usage,
            parseMode: ParseMode.Html,
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken);
    }
}