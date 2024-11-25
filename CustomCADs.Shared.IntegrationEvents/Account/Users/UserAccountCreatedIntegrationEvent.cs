using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserAccountCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email
) : BaseIntegrationEvent;
