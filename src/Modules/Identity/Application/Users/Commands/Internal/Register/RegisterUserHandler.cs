using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Register;

public class RegisterUserHandler(IUserService service, IRequestSender sender)
	: ICommandHandler<RegisterUserCommand>
{
	public async Task Handle(RegisterUserCommand req, CancellationToken ct)
	{
		AccountId accountId = await sender.SendCommandAsync(
			new CreateAccountCommand(
				Role: req.Role,
				Username: req.Username,
				Email: req.Email,
				FirstName: req.FirstName,
				LastName: req.LastName
			),
			ct
		).ConfigureAwait(false);

		await service.CreateAsync(
			user: User.Create(
				role: req.Role,
				username: req.Username,
				email: new(req.Email, IsVerified: false),
				accountId: accountId
			),
			password: req.Password
		).ConfigureAwait(false);
	}
}
