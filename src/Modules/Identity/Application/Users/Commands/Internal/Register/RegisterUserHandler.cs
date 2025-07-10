using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Register;

public class RegisterUserHandler(IUserWrites writes, IRequestSender sender)
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

		bool success = await writes.CreateAsync(
			user: User.Create(
				role: req.Role,
				username: req.Username,
				email: new(req.Email, IsVerified: false),
				accountId: accountId
			),
			password: req.Password
		).ConfigureAwait(false);

		if (!success)
		{
			throw new CustomException($"Couldn't create an account for: {req.Username}.");
		}
	}
}
