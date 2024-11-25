using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.IsCreator;

public record IsProductCreatorQuery(ProductId Id, UserId CreatorId) : IQuery<bool>;
