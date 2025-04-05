using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Register;

public class RegisterUserHandler(IUserManager manager, IRequestSender sender)
    : ICommandHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand req, CancellationToken ct)
    {
        CreateAccountCommand command = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            TimeZone: req.TimeZone,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        AccountId accountId = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        bool success = await manager.AddAsync(
            user: User.Create(
                role: req.Role,
                username: req.Username,
                email: new(req.Email, IsVerified: false),
                accountId: accountId
            ),
            password: req.Password
        ).ConfigureAwait(false);

        if (!success)
            throw new CustomException($"Couldn't create an account for: {req.Username}.");
    }
}
