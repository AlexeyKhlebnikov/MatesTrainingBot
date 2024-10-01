using MediatR;

namespace Application.Users;

public sealed class AddTelegramUserCommand : IRequest<AddUserResult>
{
    public string Name { get; init; }
    public string TelegramId { get; init; } 
}