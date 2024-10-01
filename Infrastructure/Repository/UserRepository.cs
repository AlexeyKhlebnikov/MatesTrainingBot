using Domain;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal sealed class UserRepository(TrainingDbContext dbContext) : IUserRepository
{
    public async Task<User> AddUser(User user, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Set<User>().AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<User?> GetUser(long id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetUser(string telegramId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.TelegramId == telegramId, cancellationToken);
    }
}