using Domain.Model;

namespace Domain;

public interface ITrainingRepository
{
    /// <summary>
    /// Добавить тренировку
    /// </summary>
    /// <param name="training"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Add(Training training, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Удалить тренировку
    /// </summary>
    /// <param name="training"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(Training training, CancellationToken cancellationToken = default);
}