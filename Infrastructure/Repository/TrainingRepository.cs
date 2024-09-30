using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal sealed class TrainingRepository(TrainingDbContext dbContext) : ITrainingRepository
{
    public async Task Add(Training training, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Training>()
            .AddAsync(training, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Training training, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Training>()
            .Where(x => x.Id == training.Id && x.Author.Id == training.Author.Id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}