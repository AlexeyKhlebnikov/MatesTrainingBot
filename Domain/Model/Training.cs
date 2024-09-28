
using NetTopologySuite.Geometries;

namespace Domain.Model;

public sealed class Training
{
    /// <summary>
    /// Ид
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Организатор тренировки
    /// </summary>
    public User Author { get; init; } = null!;
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; init; }
    
    /// <summary>
    /// Дата проведения тренировки
    /// </summary>
    public DateOnly Date { get; init; }
    
    /// <summary>
    /// Время проведения тренировки
    /// </summary>
    public DateTimeOffset Time { get; init; }

    /// <summary>
    /// Тип тренировки
    /// </summary>
    public TrainingSubType TrainingType { get; init; } = null!;
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; init; } = null!;
    
    /// <summary>
    /// Точка старта
    /// </summary>
    public Point StartPoint { get; init; } = null!;
    
    /// <summary>
    /// Дистанция
    /// </summary>
    public uint Distance { get; init; }
    
    /// <summary>
    /// Средняя скорость\темп
    /// </summary>
    public double AverageSpeed { get; init; }
}