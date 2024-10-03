namespace Domain.Model;

/// <summary>
/// Тип тренировки
/// </summary>
public sealed class TrainingType
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; } = null!;
    
    /// <summary>
    /// Виды тренировок
    /// </summary>
    public ICollection<TrainingSubType> SubTypes { get; init; } = new List<TrainingSubType>();
}