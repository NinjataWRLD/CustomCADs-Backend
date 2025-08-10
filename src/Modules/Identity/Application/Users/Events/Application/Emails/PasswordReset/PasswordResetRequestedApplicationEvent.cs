using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public record PasswordResetRequestedApplicationEvent(
	string Email,
	string Endpoint
) : BaseApplicationEvent;
