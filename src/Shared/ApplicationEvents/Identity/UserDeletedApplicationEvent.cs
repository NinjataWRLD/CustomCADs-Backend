namespace CustomCADs.Shared.ApplicationEvents.Identity;

public record UserDeletedApplicationEvent(
	AccountId Id
) : BaseApplicationEvent;
