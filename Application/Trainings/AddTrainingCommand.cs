using Domain.Model;
using MediatR;
using NetTopologySuite.Geometries;

namespace Application.Trainings;

public sealed class AddTrainingCommand : IRequest
{
    /// <summary>
    /// Организатор тренировки
    /// </summary>
    public long AuthorId { get; init; }

    /// <summary>
    /// Дата проведения тренировки
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Время проведения тренировки
    /// </summary>
    public DateTimeOffset Time { get; init; }

    /// <summary>
    /// Ид подтипа тренировки
    /// </summary>
    public int TrainingSubTypeId { get; init; }

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