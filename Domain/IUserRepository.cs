using Domain.Model;

namespace Domain;

public interface IUserRepository
{
    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> AddUser(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пользователя по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<User?> GetUser(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пользователя по telegramId
    /// </summary>
    /// <param name="telegramId">Id в телеграм</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<User?> GetUser(string telegramId, CancellationToken cancellationToken = default);
}