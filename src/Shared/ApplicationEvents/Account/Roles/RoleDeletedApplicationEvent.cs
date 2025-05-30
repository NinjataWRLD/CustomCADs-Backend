namespace CustomCADs.Shared.ApplicationEvents.Account.Roles;

public record RoleDeletedApplicationEvent(
	string Name
) : BaseApplicationEvent;
