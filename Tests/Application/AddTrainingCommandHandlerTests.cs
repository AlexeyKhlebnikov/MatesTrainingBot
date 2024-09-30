using Application.Trainings;
using Domain;
using Domain.Exceptions;
using Domain.Model;
using FluentAssertions;
using Moq;
using Xunit.Sdk;

namespace Tests.Application;

public class AddTrainingCommandHandlerTests : TestCollection
{
    [Fact]
    public async Task Handle_DateLessThanNow_DateInvalidException()
    {
        //arrange
        var command = new AddTrainingCommand
        {
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            Time = DateTimeOffset.Now.AddHours(-1)
        };
        var repoMock = new Mock<ITrainingRepository>();
        var commandHandler =
            new AddTrainingCommandHandler(repoMock.Object, new Mock<ITrainingTypesQueryRepository>().Object);
        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);
        //assert
        await action.Should().ThrowAsync<DateInvalidException>();
    }

    [Fact]
    public async Task Handle_TimeLessThanNow_DateInvalidException()
    {
        //arrange
        var command = new AddTrainingCommand
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Time = DateTimeOffset.Now.AddHours(-1)
        };
        var repoMock = new Mock<ITrainingRepository>();
        var commandHandler =
            new AddTrainingCommandHandler(repoMock.Object, new Mock<ITrainingTypesQueryRepository>().Object);
        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);
        //assert
        await action.Should().ThrowAsync<DateInvalidException>();
    }

    [Fact]
    public async Task Handle_IncorrectTrainingType_ThrowsInvalidTrainingTypeException()
    {
        //arrange
        var subTypeId = 1;
        var command = new AddTrainingCommand
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Time = DateTimeOffset.Now.AddHours(1),
            TrainingSubTypeId = subTypeId
        };
        var repoMock = new Mock<ITrainingRepository>();
        var trainingQueryRepoMock = new Mock<ITrainingTypesQueryRepository>();
        trainingQueryRepoMock
            .Setup(x => x.GetSubTypes(It.IsAny<int?>(), CancellationToken.None))
            .Returns(() => Task.FromResult<IReadOnlyCollection<TrainingSubType>>([]));

        var commandHandler = new AddTrainingCommandHandler(repoMock.Object, trainingQueryRepoMock.Object);
        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);
        //assert
        await action.Should().ThrowAsync<InvalidTrainingSubTypeException>();
    }

    [Fact]
    public async Task Handle_CorrectDate_Success()
    {
        //arrange
        var subTypeId = 1;
        var command = new AddTrainingCommand
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Time = DateTimeOffset.Now.AddHours(1),
            TrainingSubTypeId = subTypeId
        };
        var repoMock = new Mock<ITrainingRepository>();
        var trainingQueryRepoMock = new Mock<ITrainingTypesQueryRepository>();
        trainingQueryRepoMock
            .Setup(x => x.GetSubTypes(It.IsAny<int?>(), CancellationToken.None))
            .Returns(
                () => Task.FromResult<IReadOnlyCollection<TrainingSubType>>([new TrainingSubType {Id = subTypeId}]));

        var commandHandler = new AddTrainingCommandHandler(repoMock.Object, trainingQueryRepoMock.Object);

        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);

        //assert
        await action.Should().NotThrowAsync();
        repoMock.Verify(x => x.Add(It.IsAny<Training>(), CancellationToken.None), Times.Once);
        trainingQueryRepoMock.Verify(x=> x.GetSubTypes(null, CancellationToken.None), Times.Once);
    }
}