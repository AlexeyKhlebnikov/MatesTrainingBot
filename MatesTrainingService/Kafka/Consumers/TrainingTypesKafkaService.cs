using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Domain;

namespace MatesTrainingService.Kafka.Consumers;

internal sealed class TrainingTypesKafkaService : BaseKafkaBackgroundService<long, string>
{
    protected override string TopicName => "TrainingTypesRequest";
    private const string ProduceTopicName = "TrainingTypesChanged";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    public TrainingTypesKafkaService(
        IServiceScopeFactory serviceScopeFactory,
        IKafkaProvider<long, string> kafkaProvider,
        ILogger<TrainingTypesKafkaService> logger)
        : base(serviceScopeFactory, kafkaProvider, logger)
    {
    }

    protected override async Task HandleAsync(ConsumeResult<long, string> message, CancellationToken cancellationToken)
    {
        await using var scope = ScopeFactory.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITrainingTypesQueryRepository>();
        var types = await repository.GetTypes(cancellationToken);
        var serializedTypes = JsonSerializer.Serialize(types, JsonOptions);
        var responseMessage = new Message<long, string>
        {
            Headers = message.Message.Headers,
            Value = serializedTypes,
            Key = DateTime.Now.ToFileTimeUtc()
        };

        await KafkaProvider.Producer.ProduceAsync(ProduceTopicName, responseMessage, cancellationToken);
    }
}