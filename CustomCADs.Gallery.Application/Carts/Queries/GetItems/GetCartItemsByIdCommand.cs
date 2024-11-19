using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdCommand(CartId Id) : IQuery<ICollection<GetCartItemsByIdDto>>;
