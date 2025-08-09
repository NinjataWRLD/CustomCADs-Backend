using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;

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
