namespace CustomCADs.Shared.ApplicationEvents.Account.Roles;

public record RoleCreatedApplicationEvent(
    string Name,
    string Description
) : BaseApplicationEvent;
