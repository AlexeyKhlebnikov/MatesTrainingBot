using Infrastructure;
using MatesTrainingService.Kafka;
using MatesTrainingService.Kafka.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IKafkaProvider<long, string>, TrainingTypesProvider>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<TrainingTypesKafkaService>();
builder.Services.AddLogging(x => x.ClearProviders().AddConsole());

builder.Logging.ClearProviders().AddConsole();

var host = builder.Build();
host.Run();