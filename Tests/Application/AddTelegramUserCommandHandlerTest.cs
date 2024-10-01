using Application.Users;
using Domain;
using Domain.Exceptions;
using Domain.Model;
using FluentAssertions;
using Moq;
using Xunit.Sdk;

namespace Tests.Application;

public class AddTelegramUserCommandHandlerTest : TestCollection
{
    [Fact]
    public void Handle_TelegramIdEmpty_ThrowsArgumentException()
    {
        //arrange
        var command = new AddTelegramUserCommand();
        var handler = new AddTelegramUserCommandHandler(null);
        //act
        var action = () => handler.Handle(command, CancellationToken.None);
        //assert
        action.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public void Handle_UserExists_ThrowUserAlreadyExistsException()
    {
        //arrange
        var userRepository = new Mock<IUserRepository>();
        var telegramId = Guid.NewGuid().ToString();
        var command = new AddTelegramUserCommand {TelegramId = telegramId};
        var handler = new AddTelegramUserCommandHandler(userRepository.Object);
        
        userRepository
            .Setup(x => x.GetUser(telegramId, CancellationToken.None))
            .Returns(Task.FromResult(new User())!);
        
        //act
        var action = () => handler.Handle(command, CancellationToken.None);
        //assert
        action.Should().ThrowAsync<UserAlreadyExistsException>();
    }
    
    [Fact]
    public async Task Handle_Success_ThrowUserAlreadyExistsException()
    {
        //arrange
        var userRepository = new Mock<IUserRepository>();
        var telegramId = Guid.NewGuid().ToString();
        var command = new AddTelegramUserCommand {TelegramId = telegramId};
        var handler = new AddTelegramUserCommandHandler(userRepository.Object);
        
        userRepository
            .Setup(x => x.GetUser(telegramId, CancellationToken.None))
            .Returns(Task.FromResult<User>(null));
        
        userRepository.Setup(x => x.AddUser(It.IsAny<User>(), CancellationToken.None))
            .Returns(Task.FromResult(new User {Id = 1, TelegramId = telegramId}));
        
        //act
        var createdUser = await handler.Handle(command, CancellationToken.None);
        //assert
        createdUser.Should().NotBeNull();
        createdUser.UserId.Should().BeGreaterThan(0);
        userRepository.Verify(x=> x.GetUser(telegramId, CancellationToken.None), Times.Once);
        userRepository.Verify(x=> x.AddUser(It.IsAny<User>(), CancellationToken.None), Times.Once);
    }
}