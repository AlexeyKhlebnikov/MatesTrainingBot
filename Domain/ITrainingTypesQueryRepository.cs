using Domain.Model;

namespace Domain;

public interface ITrainingTypesQueryRepository
{
    /// <summary>
    /// Получить список типов тренировок
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TrainingType>> GetTypes(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить список подтипов тренировок
    /// </summary>
    /// <param name="typeId">ИД типа тренировки</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TrainingSubType>> GetSubTypes(int? typeId, CancellationToken cancellationToken = default);
}