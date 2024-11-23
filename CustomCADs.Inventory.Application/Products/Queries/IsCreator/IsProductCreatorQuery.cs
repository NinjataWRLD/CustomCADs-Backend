using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.IsCreator;

public record IsProductCreatorQuery(ProductId Id, UserId CreatorId) : IQuery<bool>;
