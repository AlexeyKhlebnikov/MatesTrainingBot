namespace Domain.Model;

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
}