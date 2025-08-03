using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ResetPassword;

public class ResetUserPasswordHandler(IUserReads reads, IUserWrites writes)
	: ICommandHandler<ResetUserPasswordCommand>
{
	public async Task Handle(ResetUserPasswordCommand req, CancellationToken ct)
	{
		User user = await reads.GetByEmailAsync(req.Email).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(User.Email), req.Email);

		bool succeess = await writes.ResetPasswordAsync(user.Username, req.Token, req.NewPassword).ConfigureAwait(false);
		if (!succeess)
		{
			throw CustomAuthorizationException<User>.Custom($"Failed to reset Account: {user.Username}'s password.");
		}
	}
}
