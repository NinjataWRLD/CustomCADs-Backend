namespace CustomCADs.Shared.ApplicationEvents.Identity;

public record UserEditedApplicationEvent(
    AccountId Id,
    string Username,
    string Email
) : BaseApplicationEvent;
