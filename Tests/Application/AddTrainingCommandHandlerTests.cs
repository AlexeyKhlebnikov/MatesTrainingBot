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
            new AddTrainingCommandHandler(repoMock.Object, new Mock<ITrainingTypesQueryRepository>().Object, null);
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
            new AddTrainingCommandHandler(repoMock.Object, new Mock<ITrainingTypesQueryRepository>().Object, null);
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

        var commandHandler = new AddTrainingCommandHandler(repoMock.Object, trainingQueryRepoMock.Object, null);
        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);
        //assert
        await action.Should().ThrowAsync<InvalidTrainingSubTypeException>();
    }

    [Fact]
    public async Task Handle_CorrectCommand_Success()
    {
        //arrange
        var subTypeId = 1;
        var command = new AddTrainingCommand
        {
            Date = DateOnly.FromDateTime(DateTime.Today),
            Time = DateTimeOffset.Now.AddHours(1),
            TrainingSubTypeId = subTypeId,
            AuthorId = 1
        };
        var trainingRepository = new Mock<ITrainingRepository>();
        var trainingQueryRepoMock = new Mock<ITrainingTypesQueryRepository>();
        trainingQueryRepoMock
            .Setup(x => x.GetSubTypes(It.IsAny<int?>(), CancellationToken.None))
            .Returns(
                () => Task.FromResult<IReadOnlyCollection<TrainingSubType>>([new TrainingSubType {Id = subTypeId}]));
        
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetUser(command.AuthorId, CancellationToken.None))
            .Returns(Task.FromResult(new User())!);

        var commandHandler = new AddTrainingCommandHandler(trainingRepository.Object, trainingQueryRepoMock.Object, userRepository.Object);

        //act
        var action = () => commandHandler.Handle(command, CancellationToken.None);

        //assert
        await action.Should().NotThrowAsync();
        trainingRepository.Verify(x => x.Add(It.IsAny<Training>(), CancellationToken.None), Times.Once);
        trainingQueryRepoMock.Verify(x => x.GetSubTypes(null, CancellationToken.None), Times.Once);
    }
}