using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public record EmailVerificationRequestedApplicationEvent(
	string Email,
	string Endpoint
) : BaseApplicationEvent;
