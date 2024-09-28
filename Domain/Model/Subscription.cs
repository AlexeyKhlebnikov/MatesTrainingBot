using NetTopologySuite.Geometries;

namespace Domain.Model;

/// <summary>
/// Подписка
/// </summary>
public sealed class Subscription
{
    /// <summary>
    /// Идентификатор подписки
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Подписчик
    /// </summary>
    public User Subscriber { get; init; } = null!;

    /// <summary>
    /// Локация
    /// </summary>
    public Point Location { get; init; } = null!;

    /// <summary>
    /// Радиус в метрах, где искать тренировки
    /// </summary>
    public int Radius { get; init; }
    
    /// <summary>
    /// Тип тренировки
    /// </summary>
    public TrainingType?  Type { get; init; }
    
    /// <summary>
    /// Подтип
    /// </summary>
    public TrainingSubType? SubType { get; init; }
}