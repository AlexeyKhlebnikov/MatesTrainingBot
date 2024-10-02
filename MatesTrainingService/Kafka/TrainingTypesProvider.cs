using Confluent.Kafka;
using MatesTrainingService.Kafka.Consumers;

namespace MatesTrainingService.Kafka;

internal sealed class TrainingTypesProvider : IKafkaProvider<long, string>
{
    private const string KafkaBrokers = "KAFKA_BROKERS";

    public IConsumer<long, string> Consumer { get; }
    public IProducer<long, string> Producer { get; }

    public TrainingTypesProvider(ILoggerFactory loggerFactory)
    {
        var bootstrapServers = Environment.GetEnvironmentVariable(KafkaBrokers);
        var logger = loggerFactory.CreateLogger<TrainingTypesProvider>();
        var consumerConfig = new ConsumerConfig
        {
            GroupId = "training-service-group",
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false, 
            ClientId = nameof(TrainingTypesKafkaService),
            AllowAutoCreateTopics = false
        };

        var consumerBuilder = new ConsumerBuilder<long, string>(consumerConfig);

        Consumer = consumerBuilder
            .SetErrorHandler((_, error) => logger.LogError(error.Reason))
            .SetLogHandler((_, message) => logger.LogInformation(message.Message))
            .Build();

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            Partitioner = Partitioner.Consistent
        };

        var producerBuilder = new ProducerBuilder<long, string>(producerConfig);

        Producer = producerBuilder
            .SetErrorHandler((_, error) => logger.LogError(error.Reason))
            .SetLogHandler((_, message) => logger.LogInformation(message.Message))
            .Build();
    }
}