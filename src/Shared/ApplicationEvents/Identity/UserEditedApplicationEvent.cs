namespace CustomCADs.Shared.ApplicationEvents.Identity;

public record UserEditedApplicationEvent(
	AccountId Id,
	string? Username = null,
	bool? TrackViewedProducts = null
) : BaseApplicationEvent;
