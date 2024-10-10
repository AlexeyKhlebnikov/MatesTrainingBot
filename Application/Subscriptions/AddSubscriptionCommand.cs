using MediatR;
using NetTopologySuite.Geometries;

namespace Application.Subscriptions;

public sealed class AddSubscriptionCommand : IRequest
{
    /// <summary>
    /// Автор подписки
    /// </summary>
    public long UserId { get; init; }

    /// <summary>
    /// Точка старта
    /// </summary>
    public Point StartPoint { get; init; } = null!;

    /// <summary>
    /// Радиус от точки старта
    /// </summary>
    public uint Radius { get; init; }

    /// <summary>
    /// Тип тренировки
    /// </summary>
    public int TrainingType { get; init; }

    /// <summary>
    /// Подтип тренировки
    /// </summary>
    public int? TrainingSubType { get; init; }
}