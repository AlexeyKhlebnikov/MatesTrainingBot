using Telegram.Bot.Types;

namespace MatesTrainingBot;

static class Commands
{
     public const string Start = "/start";
     
     public const string AddTraining = "/addtraining";
     
     public const string AddSubscription = "/addsubscription";

     public const string ListTrainings = "/listtrainings";
    
     public const string ListSubscriptions = "/listsubscriptions";
    
    static readonly BotCommand StartCommand = new() {Command = Start, Description = "Starts bot"};

    static readonly BotCommand AddTrainingCommand = new()
        {Command = AddTraining, Description = "Добавить тренировку"};

    static readonly BotCommand AddSubscriptionCommand = new()
        {Command = AddSubscription, Description = "Добавить подписку"};

    static readonly BotCommand ListTrainingsCommand = new()
        {Command = ListTrainings, Description = "Список тренировок"};

    static readonly BotCommand ListSubscriptionsCommand = new()
        {Command = ListSubscriptions, Description = "Список подписок"};

    internal static readonly IReadOnlyCollection<BotCommand> AvailableCommands =
        [StartCommand, AddTrainingCommand, AddSubscriptionCommand, ListTrainingsCommand, ListSubscriptionsCommand];
}