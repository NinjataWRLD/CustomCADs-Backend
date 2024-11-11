using CustomCADs.Shared.Core.Common.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Shared.IntegrationEvents.Account.Users;

public record UserAccountCreatedIntegrationEvent(
    UserId Id,
    string Role,
    string Username,
    string Email
) : BaseIntegrationEvent;
