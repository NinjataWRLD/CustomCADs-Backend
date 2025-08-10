namespace CustomCADs.Shared.Application.Events.Account.Roles;

public record RoleDeletedApplicationEvent(
	string Name
) : BaseApplicationEvent;
