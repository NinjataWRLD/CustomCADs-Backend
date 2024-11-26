namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdQuery(CartId Id) : IQuery<GetCartByIdDto>;
