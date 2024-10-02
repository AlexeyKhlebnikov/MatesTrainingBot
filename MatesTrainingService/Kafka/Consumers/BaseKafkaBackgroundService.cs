using Confluent.Kafka;

namespace MatesTrainingService.Kafka.Consumers;

public abstract class BaseKafkaBackgroundService<TKey, TValue> : BackgroundService
{
    protected readonly ILogger Logger;
    protected readonly IServiceScopeFactory ScopeFactory;
    protected readonly IKafkaProvider<TKey, TValue> KafkaProvider;

    protected abstract string TopicName { get; }

    protected BaseKafkaBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        IKafkaProvider<TKey, TValue> kafkaProvider,
        ILogger logger)
    {
        Logger = logger;
        ScopeFactory = serviceScopeFactory;
        KafkaProvider = kafkaProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        if (stoppingToken.IsCancellationRequested)
        {
            return;
        }

        KafkaProvider.Consumer.Subscribe(TopicName);

        Logger.LogInformation("Start consume topic {Topic}", TopicName);

        while (!stoppingToken.IsCancellationRequested)
        {
            await ConsumeAsync(stoppingToken);
        }

        KafkaProvider.Consumer.Unsubscribe();

        Logger.LogInformation("Stop consume topic {Topic}", TopicName);
    }

    private async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        ConsumeResult<TKey, TValue>? message = null;

        try
        {
            message = KafkaProvider.Consumer.Consume(TimeSpan.FromMilliseconds(100));

            if (message is null)
                return;

            await HandleAsync(message, cancellationToken);

            KafkaProvider.Consumer.Commit();
        }
        catch (Exception exc)
        {
            var key = message is not null ? message.Message.Key?.ToString() : "No key";
            var value = message is not null ? message.Message.Value?.ToString() : "No value";


            Logger.LogError(exc, "Error process message Key:{Key} Value:{Value}.{Topic}:{Partition}:{Offset}",
                key,
                value,
                message?.Topic,
                message?.Partition.Value.ToString(),
                message?.Offset.Value.ToString());
        }
    }

    protected abstract Task HandleAsync(ConsumeResult<TKey, TValue> message, CancellationToken cancellationToken);
}