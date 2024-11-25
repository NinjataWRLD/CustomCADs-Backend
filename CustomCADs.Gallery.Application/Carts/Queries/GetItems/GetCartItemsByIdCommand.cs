using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdCommand(CartId Id) : IQuery<ICollection<GetCartItemsByIdDto>>;
