using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Delete;

public sealed record DeleteCustomCommand(
    CustomId Id,
    AccountId BuyerId
) : ICommand;
