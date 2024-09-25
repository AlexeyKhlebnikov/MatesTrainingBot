namespace Domain;

public class Training
{
    /// <summary>
    /// Ид
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Организатор тренировки
    /// </summary>
    public User Author { get; init; }
    
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
    public TimeOnly Time { get; init; }

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
    public Point StartPoint { get; init; }
    
    /// <summary>
    /// Дистанция
    /// </summary>
    public Distance Distance { get; init; }
    
    /// <summary>
    /// Средняя скорость\темп
    /// </summary>
    public double AverageSpeed { get; init; }
}