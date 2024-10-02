using Confluent.Kafka;

namespace MatesTrainingService.Kafka.Consumers;

public interface IKafkaProvider<TKey, TValue> 
{
    IConsumer<TKey, TValue> Consumer { get; }
    IProducer<TKey, TValue> Producer { get; }
}
