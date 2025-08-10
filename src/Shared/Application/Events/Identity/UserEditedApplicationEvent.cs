namespace CustomCADs.Shared.Application.Events.Identity;

public record UserEditedApplicationEvent(
	AccountId Id,
	string? Username = null,
	bool? TrackViewedProducts = null
) : BaseApplicationEvent;
