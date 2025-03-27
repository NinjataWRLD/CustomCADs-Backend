namespace CustomCADs.Shared.ApplicationEvents.Account.Roles;

public record RoleEditedApplicationEvent(
    string Name,
    string Description
) : BaseApplicationEvent;
