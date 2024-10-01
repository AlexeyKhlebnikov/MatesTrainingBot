using Domain;
using Domain.Exceptions;
using Domain.Model;
using MediatR;

namespace Application.Users;

public sealed class AddTelegramUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddTelegramUserCommand, AddUserResult>
{
    public async Task<AddUserResult> Handle(AddTelegramUserCommand request, CancellationToken cancellationToken)
    {
        await Verify(request, cancellationToken);

        var newUser = new User {Name = request.Name, TelegramId = request.TelegramId};
        newUser = await userRepository.AddUser(newUser, cancellationToken);

        return new AddUserResult(newUser.Id);
    }

    private async Task Verify(AddTelegramUserCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.TelegramId))
            throw new ArgumentNullException(nameof(request.TelegramId), "Telegram ID is required");
        
        var user = await userRepository.GetUser(request.TelegramId, cancellationToken);
        if (user != null)
            throw new UserAlreadyExistsException();
    }
}