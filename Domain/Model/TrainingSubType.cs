namespace Domain.Model;

public sealed class TrainingSubType
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    
    /// <summary>
    /// Ид типа тренировки
    /// </summary>
    public int TrainingTypeId { get; init; }
    
    /// <summary>
    /// Тип тренировки 
    /// </summary>
    public TrainingType TrainingType { get; init; } = null!;
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; } = null!;
}