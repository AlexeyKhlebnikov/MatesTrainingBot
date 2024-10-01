using Domain;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEntityFrameworkNpgsql()
            .AddEntityFrameworkNpgsqlNetTopologySuite();

        services.AddDbContext<TrainingDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TrainingService"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TrainingDbContext).Assembly.FullName);
                    sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", TrainingDbContext.DefaultSchema);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                    sqlOptions.UseNetTopologySuite();
                });
        });

        return services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ITrainingRepository, TrainingRepository>()
            .AddScoped<ITrainingTypesQueryRepository, TrainingTypeQueryRepository>();
    }
}