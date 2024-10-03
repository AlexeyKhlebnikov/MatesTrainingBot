using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal sealed class TrainingTypeQueryRepository(TrainingDbContext dbContext) : ITrainingTypesQueryRepository
{
    private const int MaxItems = 100;

    public async Task<IReadOnlyCollection<TrainingType>> GetTypes(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TrainingType>()
            .AsNoTracking()
            .Include(x=> x.SubTypes)
            .Take(MaxItems)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TrainingSubType>> GetSubTypes(int? typeId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TrainingSubType>()
            .AsNoTracking()
            .Where(x => x.TrainingTypeId == typeId || typeId == null)
            .ToArrayAsync(cancellationToken);
    }
}