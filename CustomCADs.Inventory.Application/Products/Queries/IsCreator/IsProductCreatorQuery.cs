using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.IsCreator;

public record IsProductCreatorQuery(ProductId Id, UserId CreatorId) : IQuery<bool>;
