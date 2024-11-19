using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdQuery(CartId Id) : IQuery<GetCartByIdDto>;
