namespace Domain;

/// <summary>
/// Подписка
/// </summary>
public class Subscription
{
    /// <summary>
    /// Идентификатор подписки
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Подписчик
    /// </summary>
    public User Subscriber { get; init; }

    /// <summary>
    /// Локация
    /// </summary>
    public Point Location { get; init; }

    /// <summary>
    /// Радиус в метрах, где искать тренировки
    /// </summary>
    public int Radius { get; init; }
}