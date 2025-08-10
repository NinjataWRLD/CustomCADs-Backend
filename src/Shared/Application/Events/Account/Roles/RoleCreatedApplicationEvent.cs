namespace CustomCADs.Shared.Application.Events.Account.Roles;

public record RoleCreatedApplicationEvent(
	string Name,
	string Description
) : BaseApplicationEvent;
