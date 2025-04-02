using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetById;

public sealed record ClientGetCustomByIdQuery(
    CustomId Id,
    AccountId BuyerId
) : IQuery<ClientGetCustomByIdDto>;
