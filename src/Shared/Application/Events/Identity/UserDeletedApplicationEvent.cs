namespace CustomCADs.Shared.Application.Events.Identity;

public record UserDeletedApplicationEvent(
	AccountId Id
) : BaseApplicationEvent;
