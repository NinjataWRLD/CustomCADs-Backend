using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Edit;

public sealed record EditCustomCommand(
    CustomId Id,
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand;
