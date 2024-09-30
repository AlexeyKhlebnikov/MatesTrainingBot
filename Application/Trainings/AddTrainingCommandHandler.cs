using Domain;
using Domain.Exceptions;
using Domain.Model;
using MediatR;

namespace Application.Trainings;

public sealed class AddTrainingCommandHandler(
    ITrainingRepository trainingRepository,
    ITrainingTypesQueryRepository trainingTypesQueryRepository)
    : IRequestHandler<AddTrainingCommand>
{
    public async Task Handle(AddTrainingCommand request, CancellationToken cancellationToken)
    {
        await Verify(request, cancellationToken);

        var training = new Training
        {
            CreatedAt = DateTime.UtcNow,
            Author = request.Author,
            Date = request.Date,
            Time = request.Time,
            Name = request.Name,
            Description = request.Description,
            TrainingTypeId = request.TrainingSubTypeId,
            StartPoint = request.StartPoint,
            Distance = request.Distance,
            AverageSpeed = request.AverageSpeed
        };

        await trainingRepository.Add(training, cancellationToken);
    }

    private async Task Verify(AddTrainingCommand command, CancellationToken cancellationToken)
    {
        if (command.Date < DateOnly.FromDateTime(DateTime.Now) ||
            command.Time.ToUniversalTime() < DateTime.UtcNow.ToUniversalTime())
            throw new DateInvalidException();

        var subTypes = await trainingTypesQueryRepository.GetSubTypes(null, cancellationToken);
        if (subTypes.All(x => x.Id != command.TrainingSubTypeId))
            throw new InvalidTrainingSubTypeException();

        //TODO: check author
    }
}