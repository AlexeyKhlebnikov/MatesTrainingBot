namespace Domain.Model;

public sealed class User
{
    /// <summary>
    /// Ид пользователя
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; init; } = null!;
    
    /// <summary>
    /// Ид в телеграм
    /// </summary>
    public string? TelegramId { get; init; }
}