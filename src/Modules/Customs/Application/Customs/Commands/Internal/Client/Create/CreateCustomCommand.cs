using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Create;

public sealed record CreateCustomCommand(
    string Name,
    string Description,
    bool ForDelivery,
    AccountId BuyerId
) : ICommand<CustomId>;
