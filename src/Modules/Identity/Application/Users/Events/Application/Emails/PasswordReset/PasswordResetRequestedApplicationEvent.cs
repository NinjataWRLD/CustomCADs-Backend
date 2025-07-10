using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public record PasswordResetRequestedApplicationEvent(
	string Email,
	string Endpoint
) : BaseApplicationEvent;
