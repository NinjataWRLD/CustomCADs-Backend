using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.Count;

public record ProductsCountQuery(UserId CreatorId) : IQuery<ProductsCountDto>;
