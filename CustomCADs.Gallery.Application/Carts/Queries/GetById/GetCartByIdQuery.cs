using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdQuery(CartId Id) : IQuery<GetCartByIdDto>;
